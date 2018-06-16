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
            var systemObj = Instantiate( baseSystemObj );
            DontDestroyOnLoad( systemObj );
        }
        
        #endregion

    }

}
