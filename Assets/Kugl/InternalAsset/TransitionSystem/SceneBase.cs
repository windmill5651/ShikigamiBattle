﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Kuglトランジションのシステム名前空間です。
/// </summary>
namespace Kugl.Transition
{

    /// <summary>
    ///  SceneBase
    ///  シーンのベース
    ///  
    /// Author:Windmill
    /// </summary>
    public abstract class SceneBase : MonoBehaviour
    {

        #region メソッド

        /// <summary>
        /// シーンをロードします。
        /// </summary>
        public IEnumerator LoadScene( SceneParameterBase param )
        {
            yield return OnLoadScene( param );
        }

        /// <summary>
        /// シーンをオープンします。
        /// </summary>
        public IEnumerator OpenScene( SceneParameterBase param )
        {
            yield return OnOpenScene( param );
        }

        /// <summary>
        /// シーンをクローズします。
        /// </summary>
        public IEnumerator CloseScene()
        {
            yield return OnCloseScene();
        }

        /// <summary>
        /// シーンをロードするときの処理です。
        /// </summary>
        public abstract IEnumerator OnLoadScene( SceneParameterBase param );

        /// <summary>
        /// シーンを開くときの処理です。
        /// </summary>
        public abstract IEnumerator OnOpenScene( SceneParameterBase param );

        /// <summary>
        /// シーンをクローズします。
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator OnCloseScene();

        #endregion

    }

}
