
using UnityEngine;
/// <summary>
/// 式神のキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    ///  BattleCharacterParameter
    ///  バトルキャラクターのパラメータです
    ///  
    /// Author:Windmill
    /// </summary>
    public class BattleCharacterParameter
    {

        #region フィールド/プロパティ

        /// <summary>
        /// キャラクターのID
        /// </summary>
        public int characterId;

        /// <summary>
        /// キャラクターのオーナーです
        /// </summary>
        public CharacterOwnerType ownerType;

        /// <summary>
        /// オーナー番号です
        /// </summary>
        public int ownerNo = -1;

        /// <summary>
        /// ベースとなる方向
        /// </summary>
        public Transform baseDir;

        #endregion

    }

}