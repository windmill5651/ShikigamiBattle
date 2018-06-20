using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シキガミのデバッグ名前空間です。
/// </summary>
namespace Shikigami.Debug
{

    /// <summary>
    /// 　DebugSetting
    /// 　デバッグ用の設定です。
    /// 　
    /// Author:Windmill
    /// </summary>
    public static class DebugSetting
    {
        #region フィールド/プロパティ

        /// <summary>
        /// デフォルトのスタートシーン名です。
        /// </summary>
        public const string DEFAULT_START_SCENE = "TitleScene";


        /// <summary>
        /// デバッグ時の開始シーン名です。
        /// </summary>
        public static string StartSceneDebug { get; set; }


        #endregion
    }

}
