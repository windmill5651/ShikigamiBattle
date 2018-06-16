using Kugl.Transition.Scene;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Resources.UnloadUnusedAssets();

            yield break;
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

            yield break;
        }
    }

}
