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

        #region インナークラス

        public class StateParameter
        {
            /// <summary>
            /// 最大速度です。
            /// </summary>
            public float maxSpeed = 0.0f;

            /// <summary>
            /// 現在の移動ベクトルです。
            /// </summary>
            public Vector3 CurrentMove { get; set; }

            /// <summary>
            /// 現在のインプットステートです。
            /// </summary>
            public InputableState CurrentInputState { get; set; }

            /// <summary>
            /// 移動速度倍率です。
            /// </summary>
            public float moveSqrMag { get; set; }

        }

        #endregion

        #region フィールド/プロパティ

        /// <summary>
        /// ステートのパラメータです。
        /// </summary>
        protected StateParameter stateParam;

        /// <summary>
        /// ステート変更時処理です。
        /// </summary>
        private Action< CharacterState > onChangeState;

        #endregion


        #region メソッド

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="parameter">ステートのパラメータです。</param>
        /// <param name="onChange">ステート変更時の処理です。</param>
        public CharacterStateBase( StateParameter parameter, Action< CharacterState > onChange )
        {
            stateParam = parameter;
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

        #endregion

        #region 抽象メソッド

        /// <summary>
        /// キャラクターの移動入力です。
        /// </summary>
        /// <param name="inputVec">入力ベクトル</param>
        public abstract void InputMove( Vector3 inputVec );

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
