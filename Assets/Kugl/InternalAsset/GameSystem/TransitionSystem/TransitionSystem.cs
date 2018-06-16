
using Kugl.Transition.Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Extensions;

/// <summary>
/// Kuglのトランジションシステム名前空間です。
/// </summary>
namespace Kugl.Transition
{

    /// <summary>
    ///  TransitionSystem
    ///  トランジションシステム
    ///   外部から呼び出すインターフェース部分です。
    ///  
    /// Author:Windmill
    /// </summary>
    public partial class TransitionSystem : SingletonMonoBehaviourBase< TransitionSystem >
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
    
        /// <summary>
        /// スクリーンの履歴です。
        /// </summary>
        private Dictionary< string, Stack< string > > screenHistoryStack;

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
                .Continue( () =>
                {
                    return WaitForSceneActivate( sceneName );
                } )
                .Continue( () =>
                {
                    return OpenSceneAsync( param );
                } )
                .OnComplete( ( c ) =>
                {
                    if ( c.Exception != null )
                    {
                        // 例外処理
                    }

                });

            yield return transition;
        }



        #endregion

    }

}
