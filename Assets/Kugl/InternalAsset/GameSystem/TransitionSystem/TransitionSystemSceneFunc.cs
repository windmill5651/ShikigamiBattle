using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Extensions;
using Kugl.Transition.Scene;
using Kugl.Transition.Screen;

/// <summary>
/// Kuglのトランジションシステム名前空間です。
/// </summary>
namespace Kugl.Transition
{

    /// <summary>
    ///  TransitionSystem
    ///  トランジションシステム
    ///  シーンの機能をまとめてあります。
    /// 
    /// Author:Windmill
    /// </summary>
    public partial class TransitionSystem
    {
        #region フィールド/プロパティ

        /// <summary>
        /// 現在のシーン
        /// </summary>
        private SceneBase currentScene = null;

        #endregion


        #region メソッド

        /// <summary>
        /// シーン機能の初期化です。
        /// </summary>
        private void InitializeSceneFunction()
        {
        }

        /// <summary>
        /// シーンを非同期でアンロードします。
        /// </summary>
        private IEnumerator UnloadSceneAsync()
        {
            var sceneObject = FindObjectOfType< SceneBase >();

            // シーンをクローズします。
            if ( sceneObject != null )
            {
                yield return sceneObject.CloseScene();
            }

            // 未使用のアセットをアンロードします。
            var unloadRequest = Resources.UnloadUnusedAssets();
            while ( !unloadRequest.isDone ) { yield return null; }
        }

        /// <summary>
        /// 前のシーンをパージします。
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private IEnumerator PargeSceneAsync( SceneParameterBase param )
        {
            // 空のシーン読み込みがある場合はまず空のシーンをロードする
            if ( !string.IsNullOrEmpty( emptySceneName ) )
            {
                var emptyLoadTask = SceneManager.LoadSceneAsync( emptySceneName, LoadSceneMode.Single );

                while ( emptyLoadTask.isDone )
                {
                    yield return null;
                }
            }

            // GCを呼び出す設定であればGC呼び出し
            if ( param.isCallGC )
            {
                System.GC.Collect();
            }

            yield break;
        }

        /// <summary>
        /// シーンをロードします。
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        private IEnumerator LoadSceneAsync( string sceneName )
        {
            var loadTask = SceneManager.LoadSceneAsync( sceneName, LoadSceneMode.Single );

            while ( loadTask.isDone )
            {
                yield return null;
            }

        }

        /// <summary>
        /// シーンがActive化するまで待ちます。
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        private IEnumerator WaitForSceneActivate( string sceneName )
        {
            var scene = SceneManager.GetActiveScene();

            while ( scene.name != sceneName )
            {
                scene = SceneManager.GetActiveScene();
                yield return null;
            }
        }

        /// <summary>
        /// シーンをオープンします。
        /// </summary>
        /// <param name="param">シーンのパラメータ</param>
        private IEnumerator OpenSceneAsync( SceneParameterBase param )
        {
            var sceneObject = FindObjectOfType< SceneBase >();
            
            if ( sceneObject != null )
            {
                yield return sceneObject.LoadScene( param );

                yield return sceneObject.OpenScene( param );

            }

            // 現在のシーンを保持
            currentScene = sceneObject;
            yield break;
        }

        /// <summary>
        /// シーンロード時の初期スクリーンを開始します。
        /// </summary>
        /// <param name="param">シーンのパラメータ</param>
        private IEnumerator StartScreenAsync( SceneParameterBase param )
        {
            var screenName = "";

            // パラメータに設定されていた場合はそちらを優先
            if ( param.nextScreenType != null )
            {
                screenName = param.nextScreenType.Name;
            }
            else
            {
                screenName = currentScene.Setting.defaultStartScreenName;
            }

            // シーン名が取得できなければ終了
            if( string.IsNullOrEmpty( screenName ) )
            {
                yield break;
            }

            yield return TransitionScreenAsync( screenName, param.nextScreenParam );
        }

        #endregion
    }

}
