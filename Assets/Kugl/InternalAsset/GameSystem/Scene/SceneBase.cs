using System.Collections;
using UnityEngine;

/// <summary>
/// Kuglトランジションのシステム名前空間です。
/// </summary>
namespace Kugl.Transition.Scene
{

    /// <summary>
    ///  SceneBase
    ///  シーンのベース
    ///  
    /// Author:Windmill
    /// </summary>
    public abstract class SceneBase : MonoBehaviour
    {

        #region インスペクター設定フィールド

        /// <summary>
        /// シーンの設定です。
        /// </summary>
        [ SerializeField ]
        private SceneSetting setting;

        #endregion

        
        #region フィールド/プロパティ

        /// <summary>
        /// シーンの設定です。
        /// </summary>
        public SceneSetting Setting
        {
            get { return setting; }
        }

        #endregion


        #region メソッド

        /// <summary>
        /// シーンをロードします。
        /// </summary>
        public IEnumerator LoadScene( SceneParameterBase param )
        {
            yield return OnLoadScene( param );
        }

        /// <summary>
        /// シーンをオープンします。
        /// </summary>
        public IEnumerator OpenScene( SceneParameterBase param )
        {
            yield return OnOpenScene( param );
        }

        /// <summary>
        /// シーンをクローズします。
        /// </summary>
        public IEnumerator CloseScene()
        {
            yield return OnCloseScene();
        }

        /// <summary>
        /// シーンをロードするときの処理です。
        /// </summary>
        protected abstract IEnumerator OnLoadScene( SceneParameterBase param );

        /// <summary>
        /// シーンを開くときの処理です。
        /// </summary>
        protected abstract IEnumerator OnOpenScene( SceneParameterBase param );

        /// <summary>
        /// シーンをクローズします。
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator OnCloseScene();

        #endregion

    }

}
