using UnityEngine;
using System;
using Shikigami.Game.InputUtil;

/// <summary>
/// 式神のゲームキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{
    /// <summary>
    ///  CharacterStateBase
    ///  キャラクターステートのベースクラスです。
    ///  
    /// Author:Windmill
    /// </summary>
    public abstract class CharacterStateBase
    {

        #region フィールド/プロパティ

        /// <summary>
        /// ステートのパラメータです。
        /// </summary>
        protected CharacterStateSharedValues values;
   
        /// <summary>
        /// アニメーションのコントロール
        /// </summary>
        protected CharacterAnimationControl animationControl;

        /// <summary>
        /// 移動の計算を行います。
        /// </summary>
        protected MoveCaliclator calc = null;

        /// <summary>
        /// ステート変更時処理です。
        /// </summary>
        private Action< CharacterState > onChangeState;

        /// <summary>
        /// キャラクターのRigidBody
        /// </summary>
        private Rigidbody rigid;

        #endregion


        #region メソッド


        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="valuesObj">ステートの共有値です。</param>
        /// <param name="animControl">アニメーションのコントローラです。</param>
        /// <param name="rigidBody">キャラクターの剛体</param>
        /// <param name="onChange">ステート変更時の処理です。</param>
        public CharacterStateBase( CharacterStateSharedValues valuesObj,
                                   CharacterAnimationControl animControl,
                                   Action< CharacterState > onChange )
        {
            values = valuesObj;
            animationControl = animControl;
            onChangeState = onChange;

            rigid = valuesObj.rigidBody;

            calc = new MoveCaliclator();
        }

        /// <summary>
        /// ステートを変更します。
        /// </summary>
        /// <param name="state">ステート変更時処理</param>
        protected void ChangeState( CharacterState state )
        {
            if( onChangeState != null )
            {
                onChangeState( state );
            }
        }

        /// <summary>
        /// キャラクターを移動させます。
        /// </summary>
        /// <param name="moveVector"></param>
        protected void Move( Vector3 moveVector )
        {
            rigid.velocity = moveVector;
        }

        /// <summary>
        /// キャラクターを指定の方向に向かせます
        /// </summary>
        /// <param name="lookDir"></param>
        protected void LookAt( Vector3 lookDir )
        {
            rigid.rotation = Quaternion.LookRotation( lookDir );
        }

        public virtual void OnAnimationStateEnter(){}

        public virtual void OnAnimationStateExit(){}

        #endregion


        #region 抽象メソッド

        /// <summary>
        /// ステートが変更された時の処理です。
        /// </summary>
        public abstract void OnChangedState();

        /// <summary>
        /// 更新処理です。
        /// </summary>
        /// <param name="rigid">RigidBody</param>
        /// <returns>遷移先ステート</returns>
        public abstract void OnUpdate();

        #endregion
    }

}
