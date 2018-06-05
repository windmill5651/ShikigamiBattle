using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// エディタ拡張機能の名前空間です。
/// </summary>
namespace UnityEditor.Extensions
{

    /// <summary>
    ///  HierarchyExtensionSetting
    ///  ヒエラルキー拡張の設定です。
    ///  
    /// Author:Windmill
    /// </summary>
    public class HierarchyExtensionSetting : ScriptableObject
    {

        #region フィールド/プロパティ

        /// <summary>
        /// スクリプトのリストです。
        /// </summary>
        [ SerializeField ]
        private List< MonoScript > scripts = null;

        /// <summary>
        /// 拡張機能のリストです。
        ///  リフレクションでインスタンス化する為、Updateのような
        ///  毎フレーム呼び出すような場所では呼び出さない事。
        /// </summary>
        public List< IHierarchyExtension > ExtensionList
        {
            get
            {
                return null;
            }
        }

        #endregion


        #region メソッド



        #endregion




    }

}
