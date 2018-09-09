using UnityEngine;
using System;

/// <summary>
/// 式神のキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    ///  AttackState
    ///  攻撃ステートです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class AttackState : CharacterStateBase
    {

        /// <summary>
        /// 攻撃ステートコンストラクタです。
        /// </summary>
        /// <param name="parameter">パラメータ</param>
        /// <param name="animControl">アニメーションコントローラ</param>
        /// <param name="onChangeState">ステート変更通知</param>
        public AttackState( StateParameter parameter, CharacterAnimationControl animControl, Action< CharacterState > onChangeState ) : base( parameter, animControl, onChangeState )
        {

        }

        /// <summary>
        /// 攻撃入力時の処理です。
        /// </summary>
        public override void InputAttack()
        {
            animationControl.SetAttackTrigger();
        }

        /// <summary>
        /// ジャンプ入力時の処理です。
        /// </summary>
        /// <param name="isInput">入力状況</param>
        public override void InputJump( bool isInput )
        {
        }

        /// <summary>
        /// 定期更新処理です
        /// </summary>
        /// <param name="rigid">キャラクターの剛体</param>
        public override void OnUpdate( Rigidbody rigid )
        {
        }

        /// <summary>
        /// アニメーション終了時処理
        /// </summary>
        public override void OnAnimationStateExit()
        {

            // ステート終了時に攻撃入力が成立していない場合は立ちステートへ
            if ( !animationControl.IsAttacking )
            {
                ChangeState( CharacterState.Idole );
            }
        }

    }

}

