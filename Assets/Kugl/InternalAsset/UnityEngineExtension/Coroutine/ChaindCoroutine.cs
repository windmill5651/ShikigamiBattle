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
        /// コルーチンのキューです。
        /// </summary>
        private Queue< SafeCoroutine > coroutineQueue = null;

        /// <summary>
        /// 現在回っているイテレータです。
        /// </summary>
        private IEnumerator current = null;

        /// <summary>
        /// 完了時処理です。
        /// </summary>
        private Action onComplete = null;

        /// <summary>
        /// 実行中に発生したエラーです。
        /// </summary>
        private CombinedException exceptions = null;

        #endregion


        #region メソッド

        /// <summary>
        /// 空のコルーチンチェーンを返します。
        /// </summary>
        /// <returns>コルーチンチェーン</returns>
        public static ChaindCoroutine Empty()
        {
            return new ChaindCoroutine();
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        private ChaindCoroutine()
        {
            coroutineQueue = new Queue< SafeCoroutine >();
            exceptions = new CombinedException();
        }

        /// <summary>
        /// コルーチンを繋げます。
        /// </summary>
        /// <param name="task">コルーチン生成関数</param>
        /// <returns>指定したコルーチンをつなげた自身</returns>
        public ChaindCoroutine Continue( Func< IEnumerator > task )
        {
            coroutineQueue.Enqueue( new SafeCoroutine( task ) );
            return this;
        }

        /// <summary>
        /// 実行中の例外発生時の処理を繋げます。
        /// </summary>
        /// <param name="onException">例外発生時の処理</param>
        /// <returns>指定した処理をつなげた自身</returns>
        public ChaindCoroutine OnException( Action< Exception > onException )
        {
            OnComplete( () =>
            {
                if ( exceptions.ExceptionCount != 0 )
                {
                    onException( exceptions );
                }

            } );
            return this;
        }

        /// <summary>
        /// 実行完了時処理です。
        /// </summary>
        /// <param name="onComplete">コルーチンが完了した時の処理</param>
        /// <returns>処理をつなげた自身</returns>
        public ChaindCoroutine OnComplete( Action onComplete )
        {
            this.onComplete += onComplete;
            return this;
        }

        /// <summary>
        /// イテレータを次へ進めます
        /// </summary>
        /// <returns>次があるかどうか</returns>
        public bool MoveNext()
        {
            bool isNext = false;

            // 現在のイテレータがなければキューから取得
            if( current == null )
            {
                current = coroutineQueue.Dequeue();
            }

            // イテレーション
            try
            {
                isNext = current.MoveNext();
            }
            catch( Exception e )
            {
                exceptions.AddException( e );
            }

            if ( !isNext )
            {
                if ( coroutineQueue.Count == 0 )
                {
                    // キューのカウントがなくなっていたら完了
                    if ( onComplete != null )
                    {
                        onComplete();
                    }
                }
                else
                {
                    // まだキューが残っているなら次へ
                    current = coroutineQueue.Dequeue();
                    isNext = true;
                }
            }

            return isNext;
        }

        public void Reset()
        {
        }

        #endregion

    }

}
