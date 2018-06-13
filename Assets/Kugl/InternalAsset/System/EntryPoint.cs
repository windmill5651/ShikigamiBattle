using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Kuglのシステム名前空間です。
/// </summary>
namespace Kugl.BaseSystem
{
    /// <summary>
    ///  EntryPoint
    ///  ゲームのエントリポイントを定義します。
    ///  
    /// Author:Windmill
    /// </summary>
    public class EntryPoint : MonoBehaviour
    {

        #region フィールド/プロパティ

        /// <summary>
        /// ベースシステムのオブジェクトです。
        /// </summary>
        [ SerializeField ]
        private GameObject baseSystemObj = null;


        #endregion


        #region メソッド

        /// <summary>
        /// 開始時に呼び出されます。
        /// </summary>
        void Start()
        {
            #if GAME_DEBUG

            DebugSetting();

            #elif GAME_PRODUCT

            ReleaseSetting();

            #endif

            var systemObj = Instantiate( baseSystemObj );
            DontDestroyOnLoad( systemObj );
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
