using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シキガミのデバッグ名前空間です。
/// </summary>
namespace Shikigami.Debug
{
    /// <summary>
    /// 
    /// </summary>
    public class DebugMenuScene : MonoBehaviour
    {

        #region インスペクタ-設定フィールド

        /// <summary>
        /// エントリーポイントのシーンです。
        /// </summary>
        private const string ENTRY_POINT_SCENE_NAME = "EntryPointScene";
            
        #endregion


        #region フィールド/プロパティ

        /// <summary>
        /// エントリーポイントの次のシーンです。
        /// </summary>
        private string nextScene = "GameScene";

        #endregion


        #region メソッド


        /// <summary>
        /// ゲームスタートボタンを押した時の処理です。
        /// </summary>
        public void OnPushStartGame()
        {
            DebugSetting.StartSceneDebug = nextScene;

            SceneManager.LoadScene( ENTRY_POINT_SCENE_NAME );
        }

        #endregion


    }

}
