using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 式神のゲームキャラクター名前空間です
/// </summary>
namespace Shikigami.Game.Character
{
    /// <summary>
    ///  StateParameter
    ///  ステートのパラメータです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class StateParameter
    {

        /// <summary>
        /// 接地しているか?
        /// </summary>
        public bool isGround = false;

        /// <summary>
        /// 攻撃入力をしているか
        /// </summary>
        public bool isInputAttack = false;

        /// <summary>
        /// ジャンプ入力をしているか
        /// </summary>
        public bool isInputJump = false;

        /// <summary>
        /// 現在の速度倍率
        /// </summary>
        public float currentSpeedMag;

    }

}