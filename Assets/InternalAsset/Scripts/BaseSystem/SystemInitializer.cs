using Kugl.Transition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if GAME_DEBUG
using Shikigami.GameDebug;
#endif

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

        #region 定数

        /// <summary>
        /// デフォルトで設定される開始シーンです。
        /// </summary>
        private const string DEFAULT_START_SCENE = "TitleScene";

        #endregion

        #region フィールド/プロパティ

        /// <summary>
        /// 開始シーン名です。
        /// </summary>
        private string startSceneName = "";

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
            #if GAME_DEBUG
            SetDebugSetting();
            #else
            SetReleaseSetting();
            #endif

            // トランジションシステムの初期化
            TransitionSystem.Instance.Initialize();

            // 最初のシーンへ
            yield return TransitionSystem.Instance.TransitionSceneAsync( startSceneName, null );
        }

        #if GAME_DEBUG
        /// <summary>
        /// デバッグ用の設定です。
        /// </summary>
        private void SetDebugSetting()
        {
            Debug.SetLogEnabled( true );
            startSceneName = DebugSetting.StartSceneDebug;
        }

        #else

        /// <summary>
        /// リリース用の設定です。
        /// </summary>
        private void SetReleaseSetting()
        {
            Debug.SetLogEnabled( false );
            startSceneName = DEFAULT_START_SCENE;
        }

        #endif

        #endregion

    }

}
