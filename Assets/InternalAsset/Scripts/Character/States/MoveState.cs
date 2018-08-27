using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public MoveState( StateParameter parameter, CharacterAnimationControl animControl,  Action< CharacterState > onChange ) : base( parameter, animControl, onChange )
        {
        }

        /// <summary>
        /// 攻撃入力
        /// </summary>
        /// <returns>遷移先ステート</returns>
        public override void InputAttack()
        {
        }

        /// <summary>
        /// ジャンプ入力
        /// </summary>
        /// <param name="isInput">入力方向</param>
        /// <returns>遷移先ステート</returns>
        public override void InputJump( bool isInput )
        {
        }

        /// <summary>
        /// 定期更新処理
        /// </summary>
        /// <param name="rigid">キャラクターの物理挙動</param>
        /// <returns>遷移先ステート</returns>
        public override void OnUpdate( Rigidbody rigid )
        {
            var moveVector = ( stateParam.CurrentInputVec * stateParam.maxSpeed * Time.fixedDeltaTime );
            stateParam.CurrentMove = moveVector;
            rigid.velocity = moveVector;

            var moveMag = ( moveVector.sqrMagnitude / ( stateParam.maxSpeed * stateParam.maxSpeed ) );

            animationControl.SetMoveSpeed( moveMag );

            // 移動中だけ方向転換をする
            if ( moveVector.sqrMagnitude > 0.0f )
            {
                var lookDir = moveVector.normalized;
                lookDir.y = 0;
                rigid.rotation = Quaternion.LookRotation( lookDir );
            }
            else
            {
                // 移動していなかったら立ち状態に戻す
                ChangeState( CharacterState.Idole );
            }
        }

        #endregion
    }

}
