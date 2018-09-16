using UnityEngine;

/// <summary>
/// 式神のキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    ///  MoveCaliculator
    ///  移動に関する計算です。
    ///
    /// Author:Windmill
    /// </summary>
    public class MoveCaliclator {

        #region フィールド/プロパティ

        /// <summary>
        /// 現在の速度倍率
        /// </summary>
        private float currentSpeedMag = 0;

        /// <summary>
        /// 速度倍率
        /// </summary>
        public float SpeedMag
        {
            get
            {
                return currentSpeedMag;
            }
        }

        #endregion


        #region メソッド

        /// <summary>
        /// 現在の速度入力割合を取得します
        /// </summary>
        /// <param name="inputVector">入力ベクトル</param>
        /// <returns></returns>
        public float GetSpeed( Vector3 inputVec )
        {
            // 入力がされていたら速度を徐々に上げる
            if ( inputVec.sqrMagnitude > 0 )
            {
                currentSpeedMag += Time.fixedDeltaTime * 5;
            }
            // 入力されていなかったら速度を徐々に下げる
            else
            {
                currentSpeedMag -= Time.fixedDeltaTime * 5;
            }

            // 入力の程度によって最大を制限する
            if ( inputVec.sqrMagnitude > currentSpeedMag * currentSpeedMag )
            {
                currentSpeedMag = inputVec.magnitude;
            }

            if ( currentSpeedMag > 1.0f )
            {
                currentSpeedMag = 1.0f;
            }
            else if ( currentSpeedMag < 0 )
            {
                currentSpeedMag = 0;
            }

            return currentSpeedMag;
        }

        #endregion
    }

}
