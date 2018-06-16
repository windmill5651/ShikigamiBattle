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
        public IEnumerator LoadScreen()
        {
            yield return OnLoadScreen();
        }

        /// <summary>
        /// スクリーンを開きます。
        /// </summary>
        public IEnumerator OpenScreen()
        {
            yield return OnOpenScreen();
        }

        /// <summary>
        /// スクリーンを閉じます。
        /// </summary>
        /// <returns></returns>
        public IEnumerator CloseScreen()
        {
            yield return OnCloseScreen();
        }

        /// <summary>
        /// スクリーンがロードされる時の処理です。
        /// </summary>
        protected abstract IEnumerator OnLoadScreen();

        /// <summary>
        /// スクリーンが開かれる時の処理です。
        /// </summary>
        protected abstract IEnumerator OnOpenScreen();

        /// <summary>
        /// スクリーンが閉じられる時の処理です。
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator OnCloseScreen();
       
        #endregion

    }

}