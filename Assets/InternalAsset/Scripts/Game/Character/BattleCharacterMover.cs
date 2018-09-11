using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shikigami.Game.Character
{

    /// <summary>
    ///  BattleCharacterMover
    ///  バトルキャラクターの移動処理クラスです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class BattleCharacterMover
    {
        #region フィールド/プロパティ

        /// <summary>
        /// キャラクターの剛体です。
        /// </summary>
        private Rigidbody characterBody = null;

        /// <summary>
        /// バトルキャラクターのステータスです。
        /// </summary>
        private MasterBattleCharacterStatus status;

        #endregion


        #region メソッド

        /// <summary>
        /// バトルキャラクターの移動処理の生成
        /// </summary>
        /// <param name="characterBody">キャラクターの剛体です。</param>
        /// <param name="status">キャラクターのステータス</param>
        public BattleCharacterMover( Rigidbody characterBody, MasterBattleCharacterStatus status )
        {
            this.characterBody = characterBody;
            this.status = status;
        }

        public void Move()
        {

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

            if ( currentSpeedMag > 1.0f )
            {
                currentSpeedMag = 1.0f;
            }
            else if ( currentSpeedMag < 0 )
            {
                currentSpeedMag = 0;
            }

            var speed = ( currentSpeedMag * status.moveSpeed );
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

            if ( isInputJump )
            {

            }

        }
        #endregion

    }

}

