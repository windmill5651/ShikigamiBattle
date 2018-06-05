using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Unityの拡張機能の名前空間です。
/// </summary>
namespace UnityEngine.Extensions
{

    /// <summary>
    ///  CombinedException
    ///  複数の例外を内包することの出来る例外です。
    ///  
    /// Author:Windill
    /// </summary>
    public class CombinedException : Exception {

        #region フィールド/プロパティ

        /// <summary>
        /// 発生した例外リストです。
        /// </summary>
        private List<Exception> exceptionList;

        /// <summary>
        /// エラーのメッセージです。
        /// どういった例外が起きたかを取得できます。
        /// </summary>
        public override string Message
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append( "Exception in:" );

                foreach( var exception in exceptionList )
                {
                    builder.Append( exception.GetType().Name );
                    builder.Append( "," );
                }

                return builder.ToString(); ;
            }
        }

        /// <summary>
        /// 内包されているExceptionのリストのカウントを返します。
        /// </summary>
        public int ExceptionCount
        {
            get { return exceptionList.Count; }
        }

        /// <summary>
        /// 例外のリストです。
        /// </summary>
        public List< Exception > Exceptions
        {
            get { return exceptionList; }
        }

        #endregion


        #region メソッド

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public CombinedException()
        {
            exceptionList = new List<Exception>();
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="message">エラーメッセージ</param>
        public CombinedException( string message ) : this()
        {
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="message">エラーメッセージ</param>
        /// <param name="inner">内部例外</param>
        public CombinedException( string message, Exception inner ) : this( message )
        {
        }

        /// <summary>
        /// 例外を追加します。
        /// </summary>
        /// <param name="e">例外です。</param>
        public void AddException( Exception e )
        {
            exceptionList.Add( e );            
        }

        /// <summary>
        /// Exceptionを取得します。
        /// </summary>
        /// <typeparam name="T">Exceptionのタイプ</typeparam>
        /// <returns> 指定したタイプのException </returns>
        public T GetException< T >() where T : Exception
        {
            var exception = exceptionList.Find( ( e ) => typeof( T ) == e.GetType() );
            return ( T )exception;
        }

        #endregion

    }

}
