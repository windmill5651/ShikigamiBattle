using UnityEngine;
using System;

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
        /// ステート変更時処理です。
        /// </summary>
        private Action< CharacterState > onChangeState;

        #endregion


        #region メソッド


        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="valuesObj">ステートの共有値です。</param>
        /// <param name="animControl">アニメーションのコントローラです。</param>
        /// <param name="onChange">ステート変更時の処理です。</param>
        public CharacterStateBase( CharacterStateSharedValues valuesObj, CharacterAnimationControl animControl, Action< CharacterState > onChange )
        {
            values = valuesObj;
            animationControl = animControl;
            onChangeState = onChange;
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
        /// 移動入力をします。
        /// </summary>
        /// <param name="inputVec">移動入力値です</param>
        public void InputMove( Vector3 inputVec )
        {
            var inputVector = new Vector3();
            // ベクトルのスカラ値が1.0を超えていたらノーマライズ
            if ( inputVec.sqrMagnitude <= 1.0f )
            {
                inputVector = inputVec;
            }
            else
            {
                inputVector = inputVec.normalized;
            }

            values.CurrentInputVec = inputVector;
        }

        public virtual void OnAnimationStateEnter(){}

        public virtual void OnAnimationStateExit(){}

        #endregion


        #region 抽象メソッド

        /// <summary>
        /// 攻撃入力です。
        /// </summary>
        public abstract void InputAttack();

        /// <summary>
        /// ジャンプ入力です。
        /// </summary>
        /// <param name="isInput">入力状態</param>
        public abstract void InputJump( bool isInput );

        /// <summary>
        /// 更新処理です。
        /// </summary>
        /// <param name="rigid">RigidBody</param>
        /// <returns>遷移先ステート</returns>
        public abstract void OnUpdate( Rigidbody rigid );

        #endregion
    }

}
