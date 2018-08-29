using UnityEngine;
using Shikigami.Game.Character;

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
    public class InputAdapter : MonoBehaviour
    {
        #region フィールド/プロパティ

        /// <summary>
        /// キャラクターをコントロールするクラスです。
        /// </summary>
        private ShikigamiBattleCharacter character;
    
        #endregion


        #region メソッド

        /// <summary>
        /// セットアップを行います
        /// </summary>
        /// <param name="character">操作するキャラクター</param>
        public void Setup( ShikigamiBattleCharacter character )
        {
            this.character = character;
        }

        /// <summary>
        /// キャラクター制御入力です
        /// </summary>
        /// <param name="input">入力ベクトル</param>
        public void InputCharacterControl( Vector3 input )
        {
            character.Move( input );
        }
    
        /// <summary>
        /// カメラ制御入力です。
        /// </summary>
        /// <param name="input"></param>
        public void InputCameraControl( Vector3 input )
        {
            
        }

        /// <summary>
        /// 攻撃入力です。
        /// </summary>
        public void InputAttackControl()
        {
            character.Attack();
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