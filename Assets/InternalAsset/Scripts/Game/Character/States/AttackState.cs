using UnityEngine;
using System;
using Shikigami.Game.InputUtil;


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

        #region フィールド/プロパティ

        private bool isAttackInput = false;

        #endregion


        #region メソッド

        /// <summary>
        /// 攻撃ステートコンストラクタです。
        /// </summary>
        /// <param name="parameter">パラメータ</param>
        /// <param name="animControl">アニメーションコントローラ</param>
        /// <param name="onChangeState">ステート変更通知</param>
        public AttackState( CharacterStateSharedValues parameter, CharacterAnimationControl animControl, Action< CharacterState > onChangeState ) : base( parameter, animControl, onChangeState )
        {

        }

        /// <summary>
        /// ステート変更時処理です。
        /// </summary>
        public override void OnChangedState()
        {
            isAttackInput = true;
            // 入ってきた時に攻撃トリガーをセット
            animationControl.SetAttackTrigger();
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

        /// <summary>
        /// 定期更新処理です。
        /// </summary>
        public override void OnUpdate()
        {
            var input = values.input;
            // 攻撃入力がされていたら攻撃トリガーを引き続ける
            if( input.isAttack )
            {
                if ( !isAttackInput )
                {
                    animationControl.SetAttackTrigger();
                    isAttackInput = true;
                }
            }
            else
            {
                isAttackInput = false;
            }
        }

        #endregion

    }

}

