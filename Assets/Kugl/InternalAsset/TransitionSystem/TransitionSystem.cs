using UnityEngine.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// Kuglのトランジションシステム名前空間です。
/// </summary>
namespace Kugl.Transition
{

    /// <summary>
    ///  TransitionSystem
    ///  トランジションシステム
    ///  
    /// Author:Windmill
    /// </summary>
    public class TransitionSystem : SingletonMonoBehaviourBase< TransitionSystem >
    {
        #region インスペクター設定フィールド

        /// <summary>
        /// シーン読み込みの間に挟む空のシーンです。
        /// 名前を空にしておくとこのシーンは使いません。
        /// </summary>
        [ SerializeField ]
        private string emptySceneName = "";

        #endregion


        #region フィールド/プロパティ

        /// <summary>
        /// 現在のシーン名です。
        /// </summary>
        private string currentSceneName = "";

        /// <summary>
        /// シーンの履歴です。
        /// </summary>
        private Stack< string > sceneHistroyStack;

        #endregion


        #region メソッド

        /// <summary>
        /// 初期化メソッドです。
        /// </summary>
        public void Initialize()
        {
            InitializeSceneFunction();
        }

        /// <summary>
        /// シーン機能の初期化です。
        /// </summary>
        private void InitializeSceneFunction()
        {
            sceneHistroyStack = new Stack< string >();
        }


        /// <summary>
        /// シーンを遷移させます。
        /// </summary>
        /// <typeparam name="Scene">シーンの型です。</typeparam>
        /// <param name="param">シーンのパラメータです。</param>
        public void TransitionScene< Scene >( SceneParameterBase param = null ) where Scene : SceneBase
        {
            TransitionScene( typeof( Scene ).Name, param );
        }

        /// <summary>
        /// 非同期でシーンを読み込みます。
        /// </summary>
        /// <typeparam name="Scene">シーンの型です</typeparam>
        /// <param name="param">シーンのパラメータです。</param>
        public IEnumerator TransitionSceneAsync< Scene >( SceneParameterBase param = null ) where Scene : SceneBase
        {
            yield return TransitionSceneAsync( typeof( Scene ).Name, param );
        }

        /// <summary>
        /// シーンを変更します。
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        public void TransitionScene( string sceneName, SceneParameterBase param = null )
        {
            StartCoroutine( TransitionSceneAsync( sceneName, param ) );
        }

        /// <summary>
        /// シーンを変更します。
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        public IEnumerator TransitionSceneAsync( string sceneName, SceneParameterBase param = null )
        {
            // パラメータがnullならデフォルトパラメータで遷移
            if ( param == null )
            {
                param = new SceneParameterBase();
            }

            var transition = ChaindCoroutine.Empty()
                .Continue( () =>
                {
                    return UnloadSceneAsync();
                } )
                .Continue( () =>
                {
                    return PargeSceneAsync( param );
                } )
                .Continue( () =>
                {
                    return LoadSceneAsync( sceneName );
                } )
                .Continue( ()=>
                {
                    return WaitForSceneActivate( sceneName );
                } )
                .Continue( ()=>
                {
                    return OpenSceneAsync( param );
                } );
            
            yield return transition;
        }

        /// <summary>
        /// シーンを非同期でアンロードします。
        /// </summary>
        private IEnumerator UnloadSceneAsync()
        {
            var sceneObject = FindObjectOfType< SceneBase >();

            // シーンをクローズします。
            if( sceneObject != null )
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

            while( loadTask.isDone )
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

            if( sceneObject != null )
            {
                yield return sceneObject.LoadScene( param );

                yield return sceneObject.OpenScene( param );
            }

            yield break;
        }

        #endregion

    }

}
