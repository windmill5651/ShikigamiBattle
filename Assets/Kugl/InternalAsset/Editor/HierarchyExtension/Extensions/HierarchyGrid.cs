using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// エディタ拡張の名前空間です。
/// </summary>
namespace UnityEditor.Extensions
{
    /// <summary>
    /// 　HierarchyGrid
    /// 　ヒエラルキーにグリッドを表示します。
    ///  
    /// Author:Windmill
    /// </summary>
    public class HierarchyGrid : IHierarchyExtension
    {
        /// <summary>
        /// 色データです。
        /// </summary>
        [ Serializable ]
        private class ColorData
        {
            /// <summary>
            /// 奇数行の色です。
            /// </summary>
            public Color odd;

            /// <summary>
            /// 偶数行の色です。
            /// </summary>
            public Color even;
        }


        #region メソッド


        public void OnInitialize()
        {


        }

        public void OnHierarchyGUI( int instanceId, Rect selectionRect )
        {
            var gameObj = EditorUtility.InstanceIDToObject( instanceId ) as GameObject;

            // オブジェクトがnullの場合はScene表示なので終了
            if( gameObj == null )
            {
                return;
            }

            var index = ( selectionRect.y / selectionRect.height );

            var odd = Color.white;
            var even = Color.black;

            odd.a = 0.2f;
            even.a = 0.2f;

            var currentColor = ( ( ( index % 2 ) == 0 ) ? even : odd );

            var rect = new Rect( selectionRect );

            rect.x = 0;
            rect.xMax = selectionRect.xMax;

            var defaultColor = GUI.color;
            GUI.color = currentColor;
            GUI.Box( rect, string.Empty );
            GUI.color = defaultColor;
        }

        public void OnSave()
        {
        }

        public void OnSettingWindowGUI()
        {
        }

        #endregion



    }

}
