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

        /// <summary>
        /// ステートのパラメータです。
        /// </summary>
        public class StateParameter
        {
            /// <summary>
            /// 最大速度です。
            /// </summary>
            public float maxSpeed = 0.0f;


            /// <summary>
            /// 接地しているか?
            /// </summary>
            public bool isGround = false;

            /// <summary>
            /// 現在の入力ベクトルです。
            /// </summary>
            public Vector3 CurrentInputVec { get; set; }

            /// <summary>
            /// 現在の移動ベクトルです。
            /// </summary>
            public Vector3 CurrentMove { get; private set; }


            /// <summary>
            /// 移動入力がされているか
            /// </summary>
            public bool IsInputMove
            {
                get { return ( CurrentInputVec.sqrMagnitude > 0 ); }
            }

            /// <summary>
            /// 平行移動量をセットします
            /// </summary>
            /// <param name="moveVec">平行移動量</param>
            public void SetMove( Vector3 moveVec )
            {
                var temp = CurrentMove;
                temp.x = moveVec.x;
                temp.z = moveVec.z;
                CurrentMove = temp;
            }

            /// <summary>
            /// Y軸移動量をセットします
            /// </summary>
            /// <param name="jumpPower">ジャンプ力</param>
            public void SetYMovement( float jumpPower )
            {
                var temp = CurrentMove;
                temp.y = jumpPower;

                CurrentMove = temp;
            }

            /// <summary>
            /// Yの移動量を加算します
            /// </summary>
            /// <param name="movement">加算する移動量</param>
            public void AddYMovement( float movement )            
            {
                var temp = CurrentMove;
                temp.y += movement;

                CurrentMove = temp;
            }
        }

        #endregion


        #region フィールド/プロパティ

        /// <summary>
        /// ステートのパラメータです。
        /// </summary>
        protected StateParameter stateParam;
   
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

        public static CharacterStateBase[] CreateStateMachine( StateParameter parameter, CharacterAnimationControl control, Action< CharacterState > onChange )
        {
            return new CharacterStateBase[]
            {
                new IdolState( parameter, control, onChange ),
                new MoveState( parameter, control, onChange ),
                new AttackState( parameter, control, onChange ),
                new JumpState( parameter, control, onChange ),
            };
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="parameter">ステートのパラメータです。</param>
        /// <param name="animControl">アニメーションのコントローラです。</param>
        /// <param name="onChange">ステート変更時の処理です。</param>
        public CharacterStateBase( StateParameter parameter, CharacterAnimationControl animControl, Action< CharacterState > onChange )
        {
            stateParam = parameter;
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

            stateParam.CurrentInputVec = inputVector;
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
