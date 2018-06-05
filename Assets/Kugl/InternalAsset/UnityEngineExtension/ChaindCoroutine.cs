using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Unity拡張機能の名前空間です。
/// </summary>
namespace UnityEngine.Extensions
{

    /// <summary>
    ///  ChainedCoroutine
    ///  コルーチンをチェーン化するクラスです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class ChaindCoroutine : IEnumerator
    {
        #region フィールド/プロパティ

        public object Current
        {
            get { return null; }
        }

        private Queue< SafeCoroutine > coroutineQueue = null;

        private IEnumerator current = null;

        #endregion


        #region

        /// <summary>
        /// 空のコルーチンチェーンを返します。
        /// </summary>
        /// <returns>コルーチンチェーン</returns>
        public static ChaindCoroutine Empty()
        {
            return new ChaindCoroutine();
        }

        private ChaindCoroutine()
        {
            coroutineQueue = new Queue< SafeCoroutine >();
        }


        public void Continue( Func< IEnumerator > task )
        {
            coroutineQueue.Enqueue( new SafeCoroutine( task ) );
        }

        public void ContinueWithSafeCoroutine( SafeCoroutine task )
        {
            coroutineQueue.Enqueue( task );
        }


        public bool MoveNext()
        {
            return false;
        }

        public void Reset()
        {
        }

        #endregion

    }

}
