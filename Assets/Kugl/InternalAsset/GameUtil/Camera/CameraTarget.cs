using System;
using UnityEngine;
using UnityENgine.Extensions;

/// <summary>
/// カメラ関連の名前空間
/// </summary>
namespace Game.Library.Camera
{
    /// <summary>
    /// 　CameraTarget
    /// 　ターゲットカメラ用のターゲットです。
    /// 　
    /// Author:Windmill
    /// </summary>
    public class CameraTarget : MonoBehaviour
    {

        #region 定数

        /// <summary>
        /// 最大角度です
        /// </summary>
        private const float DEGREE_MAX = 360.0f;

        #endregion


        #region フィールド/プロパティ

        /// <summary>
        /// 直行座標上の位置です。
        /// </summary>
        private Vector3 position = Vector3.zero;

        /// <summary>
        /// 球面座標上の位置です
        /// </summary>
        private SphericalVector3 spherical = new SphericalVector3();

        /// <summary>
        /// カメラターゲットのパラメータです
        /// </summary>
        private CameraTargetParameter parameter = null;

        /// <summary>
        /// 回転中心です
        /// </summary>
        private Transform center = null;

        #endregion


        #region メソッド

        /// <summary>
        /// 初期化を行います。
        /// </summary>
        /// <param name="parameter">パラメータ</param>
        public void Setup( CameraTargetParameter parameter )
        {
            // パラメータがnullの場合セットアップ出来ないので終了
            if( parameter == null )
            {
                return;
            }
            this.parameter = parameter;

            center = parameter.center;
            spherical.radius = parameter.distance;

            SetPos( center.forward * parameter.distance );
            UpdatePos();
        }

        /// <summary>
        /// 指定したベクトル方向で球面座標上に移動させます
        /// </summary>
        /// <param name="vector">移動ベクトル</param>
        public void Move( Vector3 vector )
        {
            spherical.theta += ( ( vector.x * parameter.speed ) * Mathf.Deg2Rad );
            spherical.phi += ( ( vector.y * parameter.speed ) * Mathf.Deg2Rad );

            UpdatePos();
        }

        /// <summary>
        /// 指定した位置の球面座標上に移動させます。
        /// </summary>
        /// <param name="position">位置</param>
        public void SetPos( Vector3 position )
        {
            var centerPos = center.position;
            centerPos.y += parameter.heightOffset; 
            var cartesianPos = position - centerPos;

            var sphericalTemp = SphericalCoord.GetSphericalPositionFromCartesianCoord( cartesianPos );

            spherical.phi = sphericalTemp.phi;
            spherical.theta = sphericalTemp.theta;

            UpdatePos();
        }

        /// <summary>
        /// 現在の球面座標に従って位置を更新します。
        /// </summary>
        private void UpdatePos()
        {
            // 中心位置セット
            var centerPos = center.position;
            centerPos.y += parameter.heightOffset;

            // 仰角制限を適用したうえで球面座標から直交座標の位置に変換
            spherical.phi = Mathf.Clamp( spherical.phi, -parameter.thetaRestrictionDegree * Mathf.Deg2Rad, parameter.thetaRestrictionDegree * Mathf.Deg2Rad );
            position = SphericalCoord.GetPositionFromSphericalCoord( spherical );

            // ターゲットの距離と中心の位置を適用
            position = position.normalized * parameter.distance;
            position += centerPos;
            transform.position = position;
        }

        #endregion
    }


    /// <summary>
    ///  CameraTargetParameter
    ///  カメラターゲットのパラメータです
    /// </summary>
    [ Serializable ]
    public class CameraTargetParameter
    {
        /// <summary>
        /// 回転速度です。
        /// </summary>
        public float speed = 1.0f;

        /// <summary>
        /// 中心からの距離です。
        /// </summary>
        public float distance = 2.0f;

        /// <summary>
        /// 回転中心からの高さです。
        /// </summary>
        public float heightOffset = 1.0f;

        /// <summary>
        /// 回転中心です
        /// </summary>
        public Transform center = null;

        /// <summary>
        /// 仰角の制限角度です。
        /// </summary>
        public float thetaRestrictionDegree = 50;

    }

}
