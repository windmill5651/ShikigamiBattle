using Kugl.Transition.Screen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// トランジションシステムの名前空間です。
/// </summary>
namespace Kugl.Transition
{

    /// <summary>
    ///  TransitionSystem
    ///  スクリーンの機能をまとめています。
    ///  
    /// Author:Windmill
    /// </summary>
    public partial class TransitionSystem
    {
        #region フィールド/プロパティ

        /// <summary>
        /// 現在開いているスクリーンです。
        /// </summary>
        private ScreenBase currentScreen;
        
        /// <summary>
        /// スクリーンの履歴のリストです。
        /// </summary>
        private List< ScreenBase > screenHistoryList;

        #endregion


        #region メソッド

        /// <summary>
        /// 画面機能を初期化します。
        /// </summary>
        private void InitializeScreenFunc()
        {
            screenHistoryList = new List< ScreenBase >();
            currentScreen = null;
        }

        /// <summary>
        /// 画面をアンロードします。
        /// </summary>
        /// <param name="param">パラメータ</param>
        private IEnumerator UnloadScreenAsync( ScreenParameterBase param )
        {
            // スクリーンがなければ終了
            if( currentScreen == null )
            {
                yield break;
            }

            // スクリーンのクローズ処理
            yield return currentScreen.CloseScreen( param );

            // スクリーンを非アクティブ
            currentScreen.gameObject.SetActive( false );

            // 未使用アセットをアンロード
            var unloadRequest = Resources.UnloadUnusedAssets();
            while( !unloadRequest.isDone ) { yield return null; }
        }

        /// <summary>
        /// スクリーンをロードします。
        /// </summary>
        /// <param name="param">パラメータです。</param>
        /// <param name="screenName">画面名です</param>
        private IEnumerator LoadScreenAsync( string screenName, ScreenParameterBase param = null )
        {
            ScreenBase screen = null;
            var cachedScreenObj = GameObject.Find( screenName );

            if ( cachedScreenObj != null )
            {
                screen = cachedScreenObj.GetComponent<ScreenBase>();
            }
            else
            {
                // スクリーンのロードリクエストをします。
                var sceneName = currentScene.GetType().Name;
                var screenLoadRequest = Resources.LoadAsync<GameObject>( currentScene.Setting.resourceRootPath + "/" + screenName );

                while ( !screenLoadRequest.isDone ) { yield return null; }

                // リソースからスクリーンを生成
                var screenResource = screenLoadRequest.asset as GameObject;
                var screenObject = Instantiate( screenResource, currentScene.Setting.screenRoot );

                screen = screenObject.GetComponent<ScreenBase>();

            }

            yield return screen.LoadScreen( param );

            currentScreen = screen;
            screenHistoryList.Add( currentScreen );

            yield break;
        }

        /// <summary>
        /// 画面を開きます。
        /// </summary>
        /// <param name="param">パラメータ</param>
        private IEnumerator OpenScreenAsync( ScreenParameterBase param )
        {
            currentScreen.gameObject.SetActive( true );

            yield return currentScreen.OpenScreen( param );
        }

        #endregion
    }

}