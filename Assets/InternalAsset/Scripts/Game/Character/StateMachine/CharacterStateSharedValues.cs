using Shikigami.Game.InputUtil;
using UnityEngine;

/// <summary>
/// 式神のキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    /// ステートのパラメータです。
    /// </summary>
    public class CharacterStateSharedValues
    {

        /// <summary>
        /// 接地しているか?
        /// </summary>
        public bool isGround = false;

        /// <summary>
        /// キャラクターの剛体
        /// </summary>
        public Rigidbody rigidBody = null;

        /// <summary>
        /// 現在の入力情報
        /// </summary>
        public CurrentInput input = null;

        /// <summary>
        /// 現在キャラクターが向いている方向
        /// </summary>
        public Vector3 currentDir = Vector3.zero;

    }

}
