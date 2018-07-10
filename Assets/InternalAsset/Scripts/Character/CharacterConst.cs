using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shikigami.Game.Character
{

    /// <summary>
    ///  CharacterConst
    ///  キャラクターに関する定数値を持ちます。
    /// </summary>
    public class CharacterConst : MonoBehaviour
    {
        /// <summary>
        /// デフォルトの速度倍率です。
        /// </summary>
        public const float DEFAULT_SPEEDMAG = 1.0f;

    }

    /// <summary>
    ///  CharacterState
    ///  キャラクターのステートです。
    ///  
    /// Author:Windmill
    /// </summary>
    public enum CharacterState
    {
        /// <summary>
        /// 立ち
        /// </summary>
        Idole = 0,

        /// <summary>
        /// 移動
        /// </summary>
        Move,

        /// <summary>
        /// 攻撃
        /// </summary>
        Attack,

        /// <summary>
        /// ダッシュ
        /// </summary>
        Dash,

        /// <summary>
        /// ジャンプ
        /// </summary>
        Jump,

        /// <summary>
        /// 落下
        /// </summary>
        Fall,

    }

    /// <summary>
    /// InputableState
    /// 入力可能ステートです。
    /// 
    /// Author:Windmill
    /// </summary>
    public enum InputableState
    {
        /// <summary>
        /// 入力可能
        /// </summary>
        Enable,

        /// <summary>
        /// 入力不可能
        /// </summary>
        Disable,

    }

}