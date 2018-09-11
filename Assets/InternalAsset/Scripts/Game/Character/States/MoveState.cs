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

        private float currentSpeedMag = 0;

        private Vector3 currentDir = new Vector3();

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
            if( isInput )
            {
                ChangeState( CharacterState.Jump );
            }
        }

        /// <summary>
        /// 定期更新処理
        /// </summary>
        /// <param name="rigid">キャラクターの物理挙動</param>
        /// <returns>遷移先ステート</returns>
        public override void OnUpdate()
        {
            /*
            var inputVec = stateParam.CurrentInputVec;

            // 入力がされていたら速度を徐々に上げる
            if ( inputVec.sqrMagnitude > 0 )
            {
                currentDir = inputVec.normalized;
                currentSpeedMag += Time.fixedDeltaTime * 5;
            }
            // 入力されていなかったラ速度を徐々に下げる
            else
            {
                currentSpeedMag -= Time.fixedDeltaTime * 5;
            }

            // 入力の程度によって最大を制限する
            if ( inputVec.sqrMagnitude > currentSpeedMag * currentSpeedMag )
            {
                currentSpeedMag = inputVec.magnitude;
            }

            if( currentSpeedMag > 1.0f )
            {
                currentSpeedMag = 1.0f;
            }
            else if( currentSpeedMag < 0 )
            {
                currentSpeedMag = 0;
            }

            var speed = ( currentSpeedMag * stateParam.maxSpeed );
            var moveVec = currentDir * ( speed * Time.fixedDeltaTime );

            animationControl.SetMoveSpeed( currentSpeedMag );
            // 移動中だけ方向転換をする
            if ( currentSpeedMag > 0.0f )
            {
                var lookDir = currentDir.normalized;
                lookDir.y = 0;
                rigid.rotation = Quaternion.LookRotation( lookDir );
            }
            else
            {
                // 移動していなかったら立ち状態に戻す
                ChangeState( CharacterState.Idole );
            }

            stateParam.SetMove( moveVec );
            */
        }

        #endregion
    }

}
