
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        #endregion


        #region メソッド

        /// <summary>
        /// 初期化メソッドです。
        /// </summary>
        public void Initialize()
        {
            InitializeSceneFunction();
            InitializeScreenFunc();
        }

        /// <summary>
        /// シーンを遷移させます。
        /// </summary>
        /// <typeparam name="Scene">シーンの型です。</typeparam>
        /// <param name="param">シーンのパラメータです。</param>
        public void TransitionScene< Scene >( SceneParameterBase param = null ) where Scene : SceneBase
        {
            StartCoroutine( TransitionSceneAsync< Scene >( param ) );
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
                .Continue( ()=>
                {
                    return StartScreenAsync( param );
                } )
                .OnComplete( ( c ) =>
                {
                    if ( c.Exception != null )
                    {
                        Debug.Log( c.Exception.Message );
                    }

                } );

            yield return transition;
        }

        /// <summary>
        /// スクリーンを遷移させます。
        /// </summary>
        /// <param name="screenName">スクリーン名</param>
        /// <param name="pram">スクリーンパラメータ</param>
        public void TransitionScreen( string screenName, ScreenParameterBase param = null )
        {
            StartCoroutine( TransitionScreenAsync( screenName, param ) );
        }

        /// <summary>
        /// スクリーンを遷移させます。
        /// </summary>
        /// <typeparam name="Screen">スクリーンの型</typeparam>
        /// <param name="param">パラメータ</param>
        public void TransitionScreen< Screen >( ScreenParameterBase param = null )
        {
            StartCoroutine( TransitionScreenAsync< Screen >( param ) );
        }

        /// <summary>
        /// スクリーンを非同期で遷移します。
        /// </summary>
        /// <typeparam name="Screen">スクリーンの型</typeparam>
        /// <param name="param">スクリーン遷移パラメータ</param>
        public IEnumerator TransitionScreenAsync< Screen >( ScreenParameterBase param = null )
        {
            yield return TransitionScreenAsync( typeof( Screen ).Name, param );
        }

        /// <summary>
        /// 非同期でスクリーンを遷移させます。
        /// </summary>
        /// <param name="screenName">スクリーンの名前です</param>
        /// <param name="param">パラメータ</param>
        public IEnumerator TransitionScreenAsync( string screenName, ScreenParameterBase param = null )
        {
            if( param == null )
            {
                param = new ScreenParameterBase();
            }

            var transition = ChaindCoroutine.Empty()
                .Continue( () =>
                {
                    return UnloadScreenAsync( param );
                } )
                .Continue( ()=>
                {
                    return LoadScreenAsync( screenName, param );
                } )
                .Continue( ()=>
                {
                    return OpenScreenAsync( param );
                } )
                .OnComplete( ( c )=>
                {
                    if ( c.Exception != null )
                    { 
                        Debug.Log( c.Exception.Message );
                    }
                } );

            yield return transition;
        }


        #endregion

    }

}
