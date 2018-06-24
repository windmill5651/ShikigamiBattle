using UnityEngine;

/// <summary>
/// Unity機能の拡張の名前空間です。
/// </summary>
namespace UnityENgine.Extensions
{
    /// <summary>
    ///  SpericalVector3
    ///  球面座標系上の座標を表します。
    ///  
    /// Author:Windomill
    /// </summary>
    public struct SphericalVector3
    {
        /// <summary>
        /// 仰角
        /// </summary>
        public float phi;

        /// <summary>
        /// 方位角
        /// </summary>
        public float theta;

        /// <summary>
        /// 半径
        /// </summary>
        public float radius;

        /// <summary>
        /// 文字列として取得します。
        /// </summary>
        /// <returns>このクラスの文字列</returns>
        public override string ToString()
        {
            return string.Format( "(phi:{0} , theta:{1}, radius{2})", phi, theta, radius );
        }
    }

    /// <summary>
    ///  SphericalCoord
    ///  球面座標系の変換などを行うクラスです。
    ///  
    /// Author:Windmill
    /// </summary>
    public static class SphericalCoord 
    {

        /// <summary>
        /// 球面座標上の位置から直交座標上の位置を求めます。
        /// </summary>
        /// <param name="phi">方位角</param>
        /// <param name="theta">仰角</param>
        /// <param name="radius">半径</param>
        /// <returns>直交座標上の位置</returns>
        public static Vector3 GetPositionFromSphericalCoord( SphericalVector3 sperical )
        {
            var pos = Vector3.zero;

            var radius = sperical.radius;
            var phi = sperical.phi;
            var theta = sperical.theta;

            // 球面座標系変換
            pos.x = radius * Mathf.Cos( phi ) * Mathf.Sin( theta );
            pos.y = radius * Mathf.Sin( phi );
            pos.z = radius * Mathf.Cos( phi ) * Mathf.Cos( theta );

            return pos;
        }

        /// <summary>
        /// 直交座標上の位置から球面座標上の位置を求めます。
        /// </summary>
        /// <param name="cartesian">直交座標上の位置</param>
        /// <returns>球面座標上の位置</returns>
        public static SphericalVector3 GetSphericalPositionFromCartesianCoord( Vector3 cartesian )
        {
            var radius = Mathf.Sqrt( ( cartesian.x * cartesian.x ) + ( cartesian.y * cartesian.y ) + ( cartesian.z * cartesian.z ) );
            var sinPhi = Mathf.Clamp( cartesian.y, -1, 1 );

            var phi = Mathf.Asin( cartesian.y / radius );
            var theta = Mathf.Atan2( cartesian.x, cartesian.z );

            var spherical = new SphericalVector3();

            spherical.phi = phi;
            spherical.theta = theta;
            spherical.radius = radius;

            return spherical;
        }


    }

}

