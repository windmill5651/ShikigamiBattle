using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Kuglのトランジション機能の名前空間です。
/// </summary>
namespace Kugl.Transition.Screen
{

    /// <summary>
    ///  ScreenBase
    ///  スクリーンのベースです。
    ///  
    /// Author:Windmill
    /// </summary>
    public abstract class ScreenBase : MonoBehaviour
    {

        #region メソッド

        /// <summary>
        /// スクリーンをロードします。
        /// </summary>
        public IEnumerator LoadScreen(  ScreenParameterBase param )
        {
            yield return OnLoadScreen( param );
        }

        /// <summary>
        /// スクリーンを開きます。
        /// </summary>
        public IEnumerator OpenScreen( ScreenParameterBase param )
        {
            yield return OnOpenScreen( param );
        }

        /// <summary>
        /// スクリーンを閉じます。
        /// </summary>
        public IEnumerator CloseScreen( ScreenParameterBase param )
        {
            yield return OnCloseScreen( param );
        }

        /// <summary>
        /// スクリーンがロードされる時の処理です。
        /// </summary>
        protected abstract IEnumerator OnLoadScreen( ScreenParameterBase param );

        /// <summary>
        /// スクリーンが開かれる時の処理です。
        /// </summary>
        protected abstract IEnumerator OnOpenScreen( ScreenParameterBase param );

        /// <summary>
        /// スクリーンが閉じられる時の処理です。
        /// </summary>
        protected abstract IEnumerator OnCloseScreen( ScreenParameterBase param );
       
        #endregion

    }

}