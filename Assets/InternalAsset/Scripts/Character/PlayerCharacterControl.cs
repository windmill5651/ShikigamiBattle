using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シキガミの名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    ///  PlayerCharacterControl
    ///  プレイヤーキャラクターのコントロールです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class PlayerCharacterControl : CharacterControlBase
    {
        #region フィールド/プロパティ

        /// <summary>
        /// ベースの方向
        /// </summary>
        [ SerializeField ]
        private Transform baseDir = null;

        /// <summary>
        /// 移動方向です。
        /// </summary>
        private Vector3 moveDirVec = new Vector3();
    
        #endregion


        #region メソッド

        // Use this for initialization
        protected new void Start()
        {
            base.Start();
        }

        /// <summary>
        /// プレイヤーを操作します。
        /// </summary>
        /// <param name="input">入力ベクトル</param>
        public void MovePlayer( Vector3 input )
        {
            var forward = baseDir.forward * input.z;
            var side = baseDir.right * input.x;
            var moveDir = forward + side;

            moveDir.y = 0;

            // 移動ベクトルのスカラ値を取得
            //this.moveSqrMag = moveDir.sqrMagnitude;

            // 移動ベクトル
            if ( moveDir.sqrMagnitude > 1.0f )
            {
                moveDir.Normalize();
            }

            Move( moveDir );
        }

        // Update is called once per frame
        void Update()
        {
            if( Input.GetButtonDown( "Fire1" ) )
            {
                Attack();
            }

            InputJump( Input.GetButton( "Jump" ) );
        }

        #endregion

    }

}