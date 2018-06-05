using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エディタ拡張の名前空間です。
/// </summary>
namespace UnityEditor.Extensions
{
    
    /// <summary>
    ///  JsonEditorPres
    ///  JsonでEditorPrefsに書き込みます
    ///  
    /// Author:Windmill
    /// </summary>
    public static class JsonEditorPrefs {


        /// <summary>
        /// オブジェクトをEditorPrefs
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="obj"></param>
        public static void SetObj( string key, object obj )
        {
            var jsonText = JsonUtility.ToJson( obj );
            EditorPrefs.SetString( key, jsonText );
        }



    }

}
