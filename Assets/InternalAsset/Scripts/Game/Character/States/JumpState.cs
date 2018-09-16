using System;
using UnityEngine;
using Shikigami.Game.InputUtil;

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
        private const float MAX_JUMP_POW = 4.0f;

        private const float JUMP_UP_TIME = 0.5f;

        private const float JUMP_MAX_COUNT = 2;

        #endregion


        #region フィールド/プロパティ

        private float currentJumpPow = 0;

        private bool canJump = false;

        private int jumpCount = 0;

        private float jumpUpTime = 0;

        private bool isCount = false;

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

        public override void OnChangedState()
        {
            currentJumpPow = MAX_JUMP_POW;
            canJump = true;
            jumpUpTime = JUMP_UP_TIME;
            jumpCount = 0;
            isCount = false;
            OnUpdate();
        }

        /// <summary>
        /// 定期更新処理です。
        /// </summary>
        /// <param name="input">入力状態</param>
        public override void OnUpdate()
        {
            var input = values.input;
            var currentDir = values.currentDir;

            // 入力がされていたら速度を徐々に上げる
            if ( input.inputMoveVec.sqrMagnitude > 0 )
            {
                currentDir = input.inputMoveVec.normalized;
                values.currentDir = currentDir;
            }
            var speed = calc.GetSpeed( input.inputMoveVec );
            var moveVec = currentDir * ( speed * Time.fixedDeltaTime );

            // 移動中だけ方向転換をする
            if ( calc.SpeedMag > 0.0f )
            {
                var lookDir = currentDir;
                lookDir.y = 0;
                LookAt( lookDir );
            }

            if ( input.isJump )
            {
                isCount = true;
                if ( jumpUpTime > 0 )
                {
                    currentJumpPow = MAX_JUMP_POW;
                    jumpUpTime -= Time.fixedDeltaTime;
                }
                else
                {
                    currentJumpPow -= 10 * Time.fixedDeltaTime;
                }
            }
            else
            {

                if ( isCount )
                {
                    jumpCount++;
                    isCount = false;
                }

                if ( jumpCount < JUMP_MAX_COUNT )
                {
                    Debug.Log( "JumpUp" );
                    jumpUpTime = JUMP_UP_TIME;
                }
                else
                {
                    jumpUpTime = 0;
                }
                currentJumpPow -= 10 * Time.fixedDeltaTime;
            }

            moveVec.y = currentJumpPow;

            animationControl.SetJumpSpeed( currentJumpPow / MAX_JUMP_POW );
            Move( moveVec );
            

            if( values.isGround )
            {
                if ( currentJumpPow <= 0 )
                {
                    animationControl.SetIsGround();
                }
                ChangeState( CharacterState.Idole );
            }
        }

        #endregion
    }

}
