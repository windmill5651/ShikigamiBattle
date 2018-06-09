using System;
using System.Collections;

/// <summary>
/// Unityの拡張機能の名前空間です。
/// </summary>
namespace UnityEngine.Extensions
{

    /// <summary>
    ///  SafeCoroutine
    ///  例外が発生しても進められるコルーチンです
    ///   
    /// Author:Windmill
    /// </summary>
    public class SafeCoroutine : IEnumerator
    {

        #region フィールド/プロパティ

        /// <summary>
        /// 現在のイテレータです。
        /// </summary>
        public object Current
        {
            get
            {
                if( current != null )
                {
                    return current.Current;
                }
                return null;
            }
        }

        /// <summary>
        /// コルーチンを生成する関数です。
        /// </summary>
        public Func< IEnumerator > sequenceCreateFunc = null;

        /// <summary>
        /// 現在のコルーチン
        /// </summary>
        private IEnumerator current = null;

        /// <summary>
        /// コルーチン実行中に発生したExceptionです
        /// </summary>
        private Exception exception;


        #endregion


        #region メソッド

        /// <summary>
        /// コルーチンシーケンスを生成します。
        /// </summary>
        /// <param name="func">内包するタスクを生成する関数</param>
        public SafeCoroutine( Func< IEnumerator > func )
        {
            sequenceCreateFunc = func;
        }

        /// <summary>
        /// コルーチンを次へ進めます
        /// </summary>
        /// <returns>次のシーケンスがあるかどうか</returns>
        public bool MoveNext()
        {
            var isNext = false;

            if ( current != null )
            {
                try
                {
                    isNext = current.MoveNext();
                }
                catch ( Exception e )
                {
                    exception = e;
                }
            }
            else
            {
                try
                {
                    current = sequenceCreateFunc();
                    isNext = current.MoveNext();
                }
                catch ( Exception e )
                {
                    exception = e;
                }
            }

            // 終了後、Exceptionが発生していたらThrow
            if( !isNext && exception != null )
            {
                throw exception;
            }

            return isNext;
        }

        /// <summary>
        /// リセットします。
        /// </summary>
        public void Reset()
        {
            exception = null;
            current.Reset();
        }

        #endregion

    }

}
