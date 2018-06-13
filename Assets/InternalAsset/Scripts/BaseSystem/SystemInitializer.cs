using Kugl.Transition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームのシステム根幹の名前空間です。
/// </summary>
namespace Game.System
{

    /// <summary>
    ///  SystemInitializer
    ///  システムの初期化を行います。
    ///  
    /// Author:Windmill
    /// </summary>
    public class SystemInitializer : MonoBehaviour
    {
        #region インスペクター設定フィールド

        /// <summary>
        /// リリース時のスタートシーンです。
        /// </summary>
        [ SerializeField ]
        private string startSceneProduct = "";

        /// <summary>
        /// デバッグ時のスタートシーンです。
        /// </summary>
        [ SerializeField ]
        private string startSceneDebug = "";

        #endregion


        #region メソッド

        /// <summary>
        /// 初期化です。
        /// </summary>
        void Start()
        {
            StartCoroutine( InitSystem() );
        }

        /// <summary>
        /// システムを初期化します。
        /// </summary>
        private IEnumerator InitSystem()
        {

            var startSceneName = "";
            startSceneName = startSceneDebug;

            // トランジションシステムの初期化
            TransitionSystem.Instance.Initialize();

            // 最初のシーンへ
            yield return TransitionSystem.Instance.TransitionSceneAsync( startSceneName );
        }

        #endregion

    }

}
