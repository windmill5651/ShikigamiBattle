using System.Linq;

/// <summary>
/// Unityの拡張機能の名前空間です。
/// </summary>
namespace UnityEngine.Extensions
{

    /// <summary>
    ///  SingletonMonoBehaviourBase
    ///  MonoBehaviourのシングルトンを提供します。
    ///  
    /// Author:Windmill
    /// </summary>
    public class SingletonMonoBehaviourBase< T > : MonoBehaviour where T : class
    {
        #region フィールド/プロパティ

        /// <summary>
        /// インスタンスへのアクセサです。
        /// </summary>
        public static T Instance
        {
            get
            {
                if( instance == null )
                {
                    instance = GetInstance();

                    // インスタンスが見つからなかった場合はエラーログ
                    if ( instance == null)
                    {
                        Debug.LogError( "Singleton " + typeof( T ).Name + " is Null" );
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// インスタンス本体です。
        /// </summary>
        private static T instance = null;


        #endregion


        #region メソッド
        
        /// <summary>
        /// インスタンスが存在しているかを取得します。
        /// </summary>
        /// <returns>インスタンスが既に存在しているか</returns>
        public static bool IsExist()
        {
            var instance = GetInstance();

            return ( instance != null );
        }


        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        /// <returns>インスタンス</returns>
        private static T GetInstance()
        {
            var componentList = FindObjectsOfType( typeof( T ) );
            var component = componentList.FirstOrDefault() as T;

            if( componentList.Length > 1 )
            {
                Debug.LogWarning( "Singleton MonoBehaviour: There are Multiple Objects " + typeof( T ).Name );
            }

            return component;
        }

        private void OnDestroy()
        {
            instance = null;
            OnCallDestroy();
        }


        protected virtual void OnCallDestroy() { }

        #endregion

    }

}
