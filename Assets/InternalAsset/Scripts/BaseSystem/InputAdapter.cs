using UnityEngine;
using Shikigami.Game.Character;
using Shikigam.Game.Camera;
using Game.Library.PlayerInput;

/// <summary>
/// シキガミのゲーム名前空間です。
/// </summary>
namespace Shikigami.Game
{

    /// <summary>
    ///  InputAdapter
    ///  入力制御クラスです
    ///  
    /// Author:Windmill
    /// </summary>
    public class InputAdapter : IInputAdapter
    {
        #region フィールド/プロパティ

        /// <summary>
        /// キャラクターをコントロールするクラスです。
        /// </summary>
        private ShikigamiBattleCharacter character = null;

        /// <summary>
        /// カメラのルートです
        /// </summary>
        private CameraRoot cameraRoot = null;

        #endregion


        #region メソッド

        /// <summary>
        /// セットアップを行います
        /// </summary>
        /// <param name="character">操作するキャラクター</param>
        /// <param name="cameraRoot">カメラの操作です</param>
        public void Setup( ShikigamiBattleCharacter character, CameraRoot cameraRoot )
        {
            this.character = character;
            this.cameraRoot = cameraRoot;
        }

        /// <summary>
        /// キャラクター移動制御入力です
        /// </summary>
        /// <param name="input">入力ベクトル</param>
        public void InputCharacterMoveControl( Vector3 input )
        {
            character.Move( input );
        }
    
        /// <summary>
        /// カメラ制御入力です。
        /// </summary>
        /// <param name="input"></param>
        public void InputCameraControl( Vector3 input )
        {
            cameraRoot.MoveCamera( input );
        }

        /// <summary>
        /// 攻撃入力です。
        /// </summary>
        /// <param name="isInput">入力されたか</param>
        public void InputAttackControl( bool isInput )
        {
            if ( isInput )
            {
                character.Attack();
            }
        }

        /// <summary>
        /// ジャンプ入力です。
        /// </summary>
        /// <param name="isOn">入力がOnかどうか</param>
        public void InputJumpControl( bool isOn )
        {
            character.InputJump( isOn );
        }

        #endregion

    }

}