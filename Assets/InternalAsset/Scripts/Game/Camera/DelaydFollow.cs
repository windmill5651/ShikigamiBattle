using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームのユーティティ名前空間です。
/// </summary>
namespace Game.Utility
{

    /// <summary>
    ///  DelaydFollow
    ///  特定のオブジェクトに遅れて追従させるものです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class DelaydFollow : MonoBehaviour
    {

        #region インスペクター設定フィールド

        /// <summary>
        /// 固定フレームレート更新を使用するかどうか
        /// </summary>
        [ SerializeField ]
        private bool isUseFixedUpdate = false;

        /// <summary>
        /// 追従するオブジェクト
        /// </summary>
        [ SerializeField ]
        private Transform followObject =  null;

        /// <summary>
        /// 追従するオブジェクトからのオフセット
        /// </summary>
        [ SerializeField ]
        private Vector3 followOffset = Vector3.zero;

        /// <summary>
        /// 追従速度
        /// </summary>
        [ SerializeField ]
        private float followSpeed = 1.0f;

        #endregion


        #region メソッド

        /// <summary>
        /// このオブジェクトが追いかけるターゲットを設定します。
        /// </summary>
        /// <param name="targetTransform">ターゲットのTransform</param>
        public void SetFollowTarget( Transform targetTransform )
        {
            followObject = targetTransform;
        }

        /// <summary>
        /// 固定フレームレートの定期更新処理です。
        /// </summary>
        private void FixedUpdate()
        {
            if( isUseFixedUpdate )
            {
                UpdatePos();
            }
        }

        /// <summary>
        /// 定期更新処理です。
        /// </summary>
        private void Update()
        {
            if( !isUseFixedUpdate )
            {
                UpdatePos();
            }
        }

        /// <summary>
        /// 位置を更新します
        /// </summary>
        private void UpdatePos()
        {
            if( followObject == null )
            {
                return;
            }
            // ターゲットの位置を算出し、線形補完で移動
            var targetPos = ( followObject.position + followOffset );
            transform.position = Vector3.Lerp( transform.position, followObject.position, followSpeed * Time.deltaTime );
        }

        #endregion

    }

}
