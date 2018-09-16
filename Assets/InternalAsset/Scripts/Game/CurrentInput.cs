using UnityEngine;

/// <summary>
/// 式神の入力Utilityです。
/// </summary>
namespace Shikigami.Game.InputUtil
{
    /// <summary>
    ///  CurrentInput
    ///  現在の入力状況
    /// 
    /// Author:Windmill
    /// </summary>
    public class CurrentInput
    {
        /// <summary>
        /// 移動入力ベクトル
        /// </summary>
        public Vector3 inputMoveVec = new Vector3();

        /// <summary>
        /// ジャンプ入力
        /// </summary>
        public bool isJump = false;

        /// <summary>
        /// 攻撃入力
        /// </summary>
        public bool isAttack = false;

    }

}
