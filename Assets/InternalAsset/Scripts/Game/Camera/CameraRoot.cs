using Game.Library.Camera;
using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 式神のカメラ名前空間です
/// </summary>
namespace Shikigam.Game.Camera
{

    /// <summary>
    ///  CameraRoot
    ///  カメラ制御のルートです
    ///  
    /// Author:Windmill
    /// </summary>
    public class CameraRoot : MonoBehaviour {

        /// <summary>
        /// カメラの移動ターゲットです
        /// </summary>
        [ SerializeField ]
        private CameraTarget target = null;

        [ SerializeField ]
        private DelaydFollow follow = null;

        [ SerializeField ]
        private Transform cameraTransform;        

        public Transform CameraTransform
        {
            get
            {
                return cameraTransform;
            }
        }

        /// <summary>
        /// セットアップを行います
        /// </summary>
        /// <param name="parameter">カメラのパラメータ</param>
        /// <param name="followTarget">カメラの追跡ターゲット</param>
        public void Setup( CameraTargetParameter parameter, Transform followTarget )
        {
            target.Setup( parameter );
            follow.SetFollowTarget( followTarget );
        }

        /// <summary>
        /// カメラを移動させます
        /// </summary>
        /// <param name="moveDir">移動ベクトルです</param>
        public void MoveCamera( Vector3 moveDir )
        {
            target.Move( moveDir );
        }

    }

}
