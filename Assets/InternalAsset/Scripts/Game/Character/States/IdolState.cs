using System;
using Shikigami.Game.InputUtil;
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
        public IdolState( CharacterStateSharedValues parameter, CharacterAnimationControl animControl, Action<CharacterState> onChange ) : base( parameter, animControl, onChange ) { }

        /// <summary>
        /// このステートに入ってきた時の処理です。
        /// </summary>
        /// <param name="input"></param>
        public override void OnChangedState()
        {
            OnUpdate();
        }

        /// <summary>
        /// 定期更新処理です。
        /// </summary>
        /// <param name="rigid">キャラクターのRigidBodyです。</param>
        /// <returns>遷移後ステートです。</returns>
        public override void OnUpdate()
        {
            var input = values.input;

            if( input.inputMoveVec.sqrMagnitude > 0 )
            {
                ChangeState( CharacterState.Move );
                return;
            }

            if( input.isJump )
            {
                ChangeState( CharacterState.Jump );
                return;
            }

            if( input.isAttack )
            {
                ChangeState( CharacterState.Attack );
            }
        }

        #endregion
    }

}
