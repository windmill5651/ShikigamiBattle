using System;
using UnityEngine;

/// <summary>
/// Kuglのトランジション機能の名前空間です。
/// </summary>
namespace Kugl.Transition.Scene
{
    /// <summary>
    /// 　SceneSetting
    /// 　シーンの設定です.
    /// 
    /// Autor:Windmill
    /// </summary>
    [ Serializable ]
    public struct SceneSetting
    {
        /// <summary>
        /// スクリーンのぶら下げ先です。
        /// </summary>
        public Transform screenRoot;

        /// <summary>
        /// ウィンドウのぶら下げ先です。
        /// </summary>
        public Transform windowRoot;

        /// <summary>
        /// デフォルトの開始スクリーン名です。
        /// </summary>
        public string defaultStartScreenName;

        /// <summary>
        /// リソースのパスです。
        /// </summary>
        public string resourceRootPath;

    }

}
