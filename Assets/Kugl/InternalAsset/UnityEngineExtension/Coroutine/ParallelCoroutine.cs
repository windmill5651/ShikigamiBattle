using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity拡張機能の名前空間です。
/// </summary>
namespace UnityEngine.Extensions
{

    /// <summary>
    ///  ParallelCoroutine
    ///  並列実行が出来るコルーチンです。
    ///  
    /// Author:Ryoya Sobajima
    /// </summary>
    public class ParallelCoroutine : IEnumerator {

        #region フィールド/プロパティ
        
        /// <summary>
        /// 現在のイテレータ状態です。
        /// </summary>
        public object Current
        {
            get
            {
                if ( mainEnumerator != null )
                {
                    return mainEnumerator.Current;
                }

                return null;
            }
        }

        /// <summary>
        /// メインとなるイテレータです。
        /// </summary>
        private IEnumerator mainEnumerator = null;

        /// <summary>
        /// イテレータのリストです。
        /// </summary>
        private List< IEnumerator > enumeratorList = null;

        #endregion 

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        private ParallelCoroutine()
        {
            enumeratorList = new List<IEnumerator>();
        }


        /// <summary>
        /// コルーチンをまとめ、ParallelCoroutineとして返します。
        /// </summary>
        /// <param name="iteratorList">イテレータ</param>
        public static ParallelCoroutine WhenAll( params IEnumerator[] iteratorList )
        {
            var coroutine = new ParallelCoroutine();
            coroutine.SetEnumeratorList( new List< IEnumerator >( iteratorList ) );
            return coroutine;
        }

        /// <summary>
        /// コルーチンをまとめる内部処理です。
        /// </summary>
        /// <param name="iteratorList">イテレータのリスト</param>
        private void SetEnumeratorList( List< IEnumerator > iteratorList )
        {
            enumeratorList = iteratorList;
        }

        /// <summary>
        /// メインとなるイテレータを生成します。
        /// </summary>
        private IEnumerator MainEnumerator()
        {

            foreach( var enumerator in enumeratorList )
            {
                yield return enumerator;
            }

            yield break;
        }

        /// <summary>
        /// イテレータを次に進めます。
        /// </summary>
        /// <returns>次があるかどうか</returns>
        public bool MoveNext()
        {
            if( mainEnumerator == null )
            {
                mainEnumerator = MainEnumerator();
            }

            return mainEnumerator.MoveNext();
        }

        public void Reset()
        {
        }

    }

}
