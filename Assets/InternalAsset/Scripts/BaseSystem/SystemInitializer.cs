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
            DebugSetting();
            #else
            ReleaseSetting();
            #endif

            // トランジションシステムの初期化
            TransitionSystem.Instance.Initialize();

            // 最初のシーンへ
            yield return TransitionSystem.Instance.TransitionSceneAsync( startSceneName, null );
        }

        /// <summary>
        /// デバッグ用の設定です。
        /// </summary>
        private void DebugSetting()
        {
            Debug.SetLogEnabled( true );
        }

        /// <summary>
        /// リリース用の設定です。
        /// </summary>
        private void ReleaseSetting()
        {
            Debug.SetLogEnabled( false );
        }

        #endregion

    }

}
