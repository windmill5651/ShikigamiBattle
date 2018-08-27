using UnityEngine;
using Shikigami.Game.Character;

/// <summary>
/// シキガミのゲーム名前空間です。
/// </summary>
namespace Shikigami.Game
{

    /// <summary>
    ///  PlayerCharacterControl
    ///  プレイヤーキャラクターのコントロールです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class PlayerCharacterControl : MonoBehaviour
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

        /// <summary>
        /// キャラクターをコントロールするクラスです。
        /// </summary>
        private ShikigamiCharacterController character;
    
        #endregion


        #region メソッド

        /// <summary>
        /// セットアップを行います
        /// </summary>
        /// <param name="character">操作するキャラクター</param>
        public void Setup( ShikigamiCharacterController character )
        {
            this.character = character;
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

            character.Move( moveDir );
        }

        // Update is called once per frame
        void Update()
        {
            if( Input.GetButtonDown( "Fire1" ) )
            {
                character.Attack();
            }

            character.InputJump( Input.GetButton( "Jump" ) );
        }

        #endregion

    }

}