using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 汎用ゲームシステム名前空間です。
/// </summary>
namespace Game.Library.PlayerInput
{
    
    /// <summary>
    ///  IInputAdapter
    ///  入力アダプタインターフェースです
    /// 
    /// Author:Windmill
    /// </summary>
    public interface IInputAdapter
    {
        
        /// <summary>
        /// キャラクターの移動制御です
        /// </summary>
        /// <param name="input">入力ベクトル</param>
        void InputCharacterMoveControl( Vector3 input );

        /// <summary>
        /// カメラ制御です。
        /// </summary>
        /// <param name="input">入力ベクトル</param>
        void InputCameraControl( Vector3 input );

        /// <summary>
        /// 攻撃入力です。
        /// </summary>
        /// <param name="isInput">入力されたか</param>
        void InputAttackControl( bool isInput );

        /// <summary>
        /// ジャンプ入力です
        /// </summary>
        /// <param name="isJump">入力されたか</param>
        void InputJumpControl( bool isJump );


    }

}

