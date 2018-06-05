using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エディタ拡張の名前空間です。
/// </summary>
namespace UnityEditor.Extensions
{

    /// <summary>
    ///  IHierarchyExtension
    ///  ヒエラルキー拡張のインターフェースです。
    ///  
    /// Author:Windmill
    /// </summary>
    public interface IHierarchyExtension
    {
        
        /// <summary>
        /// 初期化です。
        /// </summary>
        void OnInitialize();

        /// <summary>
        /// ヒエラルキーのGUI表示です。
        /// </summary>
        /// <param name="instanceId">インスタンスIDです。</param>
        /// <param name="selectionRect">選択されている矩形です。</param>
        void OnHierarchyGUI( int instanceId, Rect selectionRect );

        /// <summary>
        /// データ保存時の処理です。
        /// </summary>
        void OnSave();

        /// <summary>
        /// 設定ウィンドウのGUI表示です。
        /// </summary>
        void OnSettingWindowGUI();

    }

}
