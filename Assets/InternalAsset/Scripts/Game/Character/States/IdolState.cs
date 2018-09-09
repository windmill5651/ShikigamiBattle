using System;
using UnityEngine;

/// <summary>
/// 式神のゲームキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    ///  IdolState
    ///  キャラクターの立ちステートです。
    /// </summary>
    public class IdolState : CharacterStateBase
    {

        #region メソッド

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="parameter">ステートのパラメータです。</param>
        /// <param name="animControl">アニメーションのコントローラです</param>
        /// <param name="onChange">ステート変更時の処理です。</param>
        public IdolState( StateParameter parameter, CharacterAnimationControl animControl, Action< CharacterState > onChange ) : base( parameter, animControl, onChange ) { }

        /// <summary>
        /// 攻撃入力です。
        /// </summary>
        /// <returns>遷移後ステート</returns>
        public override void InputAttack()
        {
            animationControl.SetAttackTrigger();
            ChangeState( CharacterState.Attack );
        }

        /// <summary>
        /// ジャンプ入力です。
        /// </summary>
        /// <param name="isInput">ジャンプ入力状態</param>
        /// <returns>遷移後ステート</returns>
        public override void InputJump( bool isInput )
        {
            if ( isInput )
            {
                ChangeState( CharacterState.Jump );
            }
        }

        /// <summary>
        /// 定期更新処理です。
        /// </summary>
        /// <param name="rigid">キャラクターのRigidBodyです。</param>
        /// <returns>遷移後ステートです。</returns>
        public override void OnUpdate( Rigidbody rigid )
        {
            if( stateParam.IsInputMove )
            {
                ChangeState( CharacterState.Move );
            }
        }

        #endregion
    }

}
