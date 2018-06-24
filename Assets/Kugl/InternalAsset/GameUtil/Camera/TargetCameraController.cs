using UnityEngine;

/// <summary>
/// カメラ用名前空間
/// </summary>
namespace Game.Library.Camera
{

    /// <summary>
    /// 　TargetCameraController
    /// 　キャラクター位置、ターゲット位置を元に
    /// 　カメラ位置を決めるコントローラ
    /// 　
    /// Author:Windmill
    /// </summary>
    public class TargetCameraController : MonoBehaviour
    {
        #region インスペクタフィールド

        /// <summary>
        /// FixedUpdateで行進するか。
        ///  rigidbodyを使用してキャラクタを動かす場合等は
        ///  これをtrueにする
        /// </summary>
        [ SerializeField ]
        private bool isFixedUpdate = false;

        /// <summary>
        /// カメラ注視点
        /// </summary>
        [ SerializeField ]
        private Transform cameraLookTarget = null;

        /// <summary>
        /// カメラが追いかけるキャラクター
        /// </summary>
        [SerializeField]
        private Transform cameraFollowTarget = null;

        /// <summary>
        /// カメラの高さオフセット
        /// </summary>
        [SerializeField ]
        private float heightOffset = 1.0f;

        /// <summary>
        /// カメラ距離
        /// </summary>
        [ SerializeField ]
        private float distance = 1.0f;

        /// <summary>
        /// 上下の角度制限です
        /// </summary>
        [ SerializeField ]
        private float verticalRestrictDegree = 50.0f;

        /// <summary>
        /// カメラの回転感度です
        /// </summary>
        [ SerializeField ]
        private float sensitivity = 5.0f;

        #endregion


        #region フィールド/プロパティ

        /// <summary>
        /// カメラ位置
        /// </summary>
        private Vector3 cameraPos = new Vector3();

        #endregion


        #region メソッド

        /// <summary>
        /// 初期化です。
        /// </summary>
        void Start()
        {
        }

        /// <summary>
        /// カメラの更新を行います。
        /// </summary>
        private void UpdateCamera()
        {
            var targetPos = cameraFollowTarget.position;
            targetPos.y += heightOffset;

            var cameraVec = targetPos - cameraLookTarget.transform.position;

            cameraPos = ( targetPos + ( cameraVec.normalized * distance ) );
            transform.position = cameraPos;
            transform.LookAt( cameraLookTarget.transform );
        }

        /// <summary>
        /// 一定の間隔で呼び出されます
        /// </summary>
        private void FixedUpdate()
        {
            if( isFixedUpdate )
            {
                UpdateCamera();
            }
        }

        /// <summary>
        /// Update後に呼び出される定期更新処理です
        /// </summary>
        private void LateUpdate()
        {
            if( !isFixedUpdate )
            {
                UpdateCamera();
            }
        }

        #endregion
    }

}