using System;
using UnityEngine;
using Shikigami.Game.InputUtil;

/// <summary>
/// 式神のキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    ///  MoveState
    ///  移動ステートです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class MoveState : CharacterStateBase
    {
        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parameter">ステートのパラメータです。</param>
        /// <param name="animControl">アニメーションのコントローラです</param>
        /// <param name="onChange">ステート変更時の処理です。</param>
        public MoveState( CharacterStateSharedValues parameter, CharacterAnimationControl animControl,  Action< CharacterState > onChange ) : base( parameter, animControl, onChange )
        {
        }

        /// <summary>
        /// このステートに入ってきた時の処理です。
        /// </summary>
        /// <param name="input">現在の入力状態</param>
        public override void OnChangedState()
        {
            OnUpdate();
        }

        /// <summary>
        /// 定期更新処理
        /// </summary>
        /// <param name="rigid">キャラクターの物理挙動</param>
        /// <returns>遷移先ステート</returns>
        public override void OnUpdate()
        {
            var input = values.input;
            var currentDir = values.currentDir;

            // 方向をセット
            if ( input.inputMoveVec.sqrMagnitude > 0 )
            {
                currentDir = input.inputMoveVec.normalized;
                values.currentDir = currentDir;
            }

            var speed = calc.GetSpeed( input.inputMoveVec ) * 100;
            var moveVec = currentDir * ( speed * Time.fixedDeltaTime );

            animationControl.SetMoveSpeed( calc.SpeedMag );
            // 移動中だけ方向転換をする
            if ( calc.SpeedMag > 0.0f )
            {
                var lookDir = currentDir;
                lookDir.y = 0;
                LookAt( lookDir );
            }
            else
            {
                // 移動していなかったら立ち状態に戻す
                ChangeState( CharacterState.Idole );
            }

            animationControl.SetMoveSpeed( calc.SpeedMag );
            Move( moveVec );

            if( input.isJump )
            {
                animationControl.SetMoveSpeed( 0 );
                ChangeState( CharacterState.Jump );
            }
        }

        #endregion
    }

}
