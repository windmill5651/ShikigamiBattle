using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Shikigami.Game.Character
{


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
    }

}

