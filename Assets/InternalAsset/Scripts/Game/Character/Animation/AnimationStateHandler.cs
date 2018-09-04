using UnityEngine;
using System;

/// <summary>
/// アニメーションユーティリティ名前空間です
/// </summary>
namespace Game.Util.Animation
{
    /// <summary>
    ///  AnimationStateHandler
    ///  アニメーションのステートのハンドリングを行います
    /// 
    /// Author:Windmill
    /// </summary>
    public class AnimationStateHandler : StateMachineBehaviour
    {
        #region フィールド/プロパティ

        /// <summary>
        /// ステート開始時処理です
        /// </summary>
        private Action< AnimatorStateInfo > onStateEnter;

        /// <summary>
        /// ステート終了時処理です
        /// </summary>
        private Action< AnimatorStateInfo > onStateExit;

        #endregion

        #region メソッド

        /// <summary>
        /// アニメーションのハンドラをセットします
        /// </summary>
        /// <param name="onEnter">ステート開始時処理</param>
        /// <param name="onExit">ステート終了時処理</param>
        public void SetHandler( Action< AnimatorStateInfo > onEnter, Action< AnimatorStateInfo > onExit )
        {
            onStateEnter = onEnter;
            onStateExit = onExit;
        }

        /// <summary>
        /// ステートに入った時の処理です
        /// </summary>
        /// <param name="animator">アニメータオブジェクト</param>
        /// <param name="stateInfo">ステート情報</param>
        /// <param name="layerIndex">レイヤー番号</param>
        public override void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
        {
            base.OnStateEnter( animator, stateInfo, layerIndex );

            if( onStateEnter != null )
            {
                onStateEnter( stateInfo );
            }

        }

        /// <summary>
        /// ステート終了時処理です
        /// </summary>
        /// <param name="animator">アニメータオブジェクト</param>
        /// <param name="stateInfo">ステート情報</param>
        /// <param name="layerIndex">レイヤー番号</param>
        public override void OnStateExit( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
        {
            base.OnStateExit( animator, stateInfo, layerIndex );

            if( onStateExit != null )
            {
                onStateExit( stateInfo );
            }

        }

        #endregion
    }

}