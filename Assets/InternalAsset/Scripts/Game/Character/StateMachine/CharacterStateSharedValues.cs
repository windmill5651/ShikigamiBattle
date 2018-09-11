using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 式神のキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    /// ステートのパラメータです。
    /// </summary>
    public class CharacterStateSharedValues
    {
        /// <summary>
        /// 接地しているか?
        /// </summary>
        public bool isGround = false;

        /// <summary>
        /// 現在の入力ベクトルです。
        /// </summary>
        public Vector3 CurrentInputVec { get; set; }

        /// <summary>
        /// 現在の移動ベクトルです。
        /// </summary>
        public Vector3 CurrentMove { get; private set; }


        /// <summary>
        /// 移動入力がされているか
        /// </summary>
        public bool IsInputMove
        {
            get { return ( CurrentInputVec.sqrMagnitude > 0 ); }
        }

        /// <summary>
        /// 平行移動量をセットします
        /// </summary>
        /// <param name="moveVec">平行移動量</param>
        public void SetMove( Vector3 moveVec )
        {
            var temp = CurrentMove;
            temp.x = moveVec.x;
            temp.z = moveVec.z;
            CurrentMove = temp;
        }

        /// <summary>
        /// Y軸移動量をセットします
        /// </summary>
        /// <param name="jumpPower">ジャンプ力</param>
        public void SetYMovement( float jumpPower )
        {
            var temp = CurrentMove;
            temp.y = jumpPower;

            CurrentMove = temp;
        }

        /// <summary>
        /// Yの移動量を加算します
        /// </summary>
        /// <param name="movement">加算する移動量</param>
        public void AddYMovement( float movement )
        {
            var temp = CurrentMove;
            temp.y += movement;

            CurrentMove = temp;
        }
    }

}
