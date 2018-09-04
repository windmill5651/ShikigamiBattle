using UnityEngine;
using System;

/// <summary>
/// アニメーションのユーティリティ名前空間です
/// </summary>
namespace Game.Util.Animation
{

    public class AnimationEventHandler {

        #region フィールド/プロパティ

        /// <summary>
        /// ステートに入った時の処理です
        /// </summary>
        private Action< AnimatorStateInfo > onStateEnter = null;

        /// <summary>
        /// ステートから出た時の処理です
        /// </summary>
        private Action< AnimatorStateInfo > onStateExit = null;

        #endregion


        #region メソッド

        /// <summary>
        /// セットアップを行います
        /// </summary>
        /// <param name="anim">アニメーション</param>
        /// <param name="onStateEnter">ステートに入った時の処理</param>
        /// <param name="onStateExit">ステートから出た時の処理</param>
        public void Setup( Animator anim, Action< AnimatorStateInfo > onStateEnter, Action< AnimatorStateInfo > onStateExit )
        {
            // アニメーションのステートハンドラを取得
            var handler = anim.GetBehaviour< AnimationStateHandler >();
            handler.SetHandler( OnStateEnter, OnStateExit );

            this.onStateEnter = onStateEnter;
            this.onStateExit = onStateExit;
        }

        /// <summary>
        /// ステートに入ってきた時の処理です
        /// </summary>
        /// <param name="info">アニメーション情報</param>
        public void OnStateEnter( AnimatorStateInfo info )
        {
            if( onStateEnter != null )
            {
                onStateEnter( info );
            }
        }

        /// <summary>
        /// ステートから出る時の処理です
        /// </summary>
        /// <param name="info">アニメーション情報</param>
        public void OnStateExit( AnimatorStateInfo info )
        {
            if( onStateExit != null )
            {
                onStateExit( info );
            }

        }

        #endregion

    }

}

