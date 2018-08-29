using Shikigami.Game;
using UnityEngine;
using Game.Library.PlayerInput;
using System;

/// <summary>
/// 式神のシステム名前空間です
/// </summary>
namespace Shikigami.System
{
    /// <summary>
    /// PlayerInput
    /// プレイヤーの入力情報です
    ///  
    /// Author:Windmill
    /// </summary>
    public class ShikigamiPlayerInput : InputOwnerBase
    {

        #region フィールド/プロパティ


        #endregion


        #region メソッド

        /// <summary>
        /// 初期化処理です
        /// </summary>
        protected override void OnInitialize()
        {
            // キーコンフィグなんかを設定する
        }

        /// <summary>
        /// 定期更新処理です
        /// </summary>
        public override void OnUpdate()
        {
            var inputVector = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
            var isInputAttack = Input.GetButtonDown( "Fire1" );
            var isInputJump = Input.GetButton( "Jump" );
            var mouseVector = new Vector3( Input.GetAxis( "Mouse X" ), Input.GetAxis( "Mouse Y" ), 0 );

            // キャラクターコントロールです
            inputAdapter.InputCharacterMoveControl( inputVector );

            // 攻撃入力です
            inputAdapter.InputAttackControl( isInputAttack );

            // ジャンプ入力です
            inputAdapter.InputJumpControl( isInputJump );

            // カメラコントロールです
            inputAdapter.InputCameraControl( mouseVector );
        }

        #endregion

    }

}
