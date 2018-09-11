using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 式神のゲームキャラクターの名前空間です
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    ///  JumpState
    ///  ジャンプステートです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class JumpState : CharacterStateBase
    {

        #region 固定値

        /// <summary>
        /// ジャンプの力
        /// </summary>
        private const float JUMP_POW = 10;

        #endregion

        #region フィールド/プロパティ
        
        /// <summary>
        /// ジャンプ入力されているか?
        /// </summary>
        private bool isInputJump = false;

        private Vector3 currentDir = new Vector3();

        private float currentSpeedMag = 0;

        #endregion


        #region メソッド

        /// <summary>
        /// コンストラクタです
        /// </summary>
        /// <param name="parameter">パラメータ</param>
        /// <param name="animControl">アニメーションコントロール</param>
        /// <param name="onChange">変更時処理</param>
        public JumpState( CharacterStateSharedValues parameter, CharacterAnimationControl animControl, Action<CharacterState> onChange ) : base( parameter, animControl, onChange )
        {
        }

        public override void InputAttack()
        {
        }

        public override void InputJump( bool isInput )
        {
            isInputJump = isInput;
        }

        public override void OnUpdate( Rigidbody rigid )
        {
            var inputVec = values.CurrentInputVec;

            // 入力がされていたら速度を徐々に上げる
            if ( inputVec.sqrMagnitude > 0 )
            {
                currentDir = inputVec.normalized;
                currentSpeedMag += Time.fixedDeltaTime * 5;
            }
            // 入力されていなかったら速度を徐々に下げる
            else
            {
                currentSpeedMag -= Time.fixedDeltaTime * 5;
            }

            // 入力の程度によって最大を制限する
            if ( inputVec.sqrMagnitude > currentSpeedMag * currentSpeedMag )
            {
                currentSpeedMag = inputVec.magnitude;
            }

            if ( currentSpeedMag > 1.0f )
            {
                currentSpeedMag = 1.0f;
            }
            else if ( currentSpeedMag < 0 )
            {
                currentSpeedMag = 0;
            }

            var speed = ( currentSpeedMag * 100 );
            var moveVec = currentDir * ( speed * Time.fixedDeltaTime );

            //animationControl.SetMoveSpeed( currentSpeedMag );
            // 移動中だけ方向転換をする
            if ( currentSpeedMag > 0.0f )
            {
                var lookDir = currentDir.normalized;
                lookDir.y = 0;
                rigid.rotation = Quaternion.LookRotation( lookDir );
            }

            values.SetMove( moveVec );
            if ( isInputJump )
            {
                values.SetYMovement( JUMP_POW );

            }
            
            if( values.isGround )
            {
                ChangeState( CharacterState.Idole );
            }

        }

        #endregion
    }

}
