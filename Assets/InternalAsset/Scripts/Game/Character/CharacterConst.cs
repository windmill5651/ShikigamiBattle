
/// <summary>
/// 式神のキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    ///  CharacterConst
    ///  キャラクターに関する定数値を持ちます。
    /// </summary>
    public static class CharacterConst
    {

        #region メソッド


        /// <summary>
        /// デフォルトの速度倍率です
        /// </summary>
        /// <returns>デフォルトの速度倍率</returns>
        public static float GetDefaultSpeedMag()
        {
            return 1.0f;
        }

        /// <summary>
        /// キャラクターユニットのリソース名を取得します。
        /// </summary>
        /// <returns>キャラクタの制御ユニットリソース名</returns>
        public static string GetCharacterUnitResName()
        {
            return "CharacterUnit";
        }

        /// <summary>
        /// キャラクターのモデル名を取得します
        /// </summary>
        /// <param name="characterId">キャラクターID</param>
        /// <returns>キャラクタモデル名</returns>
        public static string GetCharacterModelName( int characterId )
        {
            return string.Format( "Battle_Character_{0:d7}", characterId );
        }

        #endregion

    }

    /// <summary>
    ///  CharacterOwner
    ///  キャラクターの操作をする主です
    ///  
    /// Author:Windmill
    /// </summary>
    public enum CharacterOwnerType
    {
        INVALID,
        PLAYER,
        AI,
        REPLAY,
    }

    /// <summary>
    ///  CharacterState
    ///  キャラクターのステートです。
    ///  
    /// Author:Windmill
    /// </summary>
    public enum CharacterState
    {
        /// <summary>
        /// 立ち
        /// </summary>
        Idole = 0,

        /// <summary>
        /// 移動
        /// </summary>
        Move,

        /// <summary>
        /// 攻撃
        /// </summary>
        Attack,

        /// <summary>
        /// ダッシュ
        /// </summary>
        Dash,

        /// <summary>
        /// ジャンプ
        /// </summary>
        Jump,

        /// <summary>
        /// 落下
        /// </summary>
        Fall,

    }

    /// <summary>
    /// InputableState
    /// 入力可能ステートです。
    /// 
    /// Author:Windmill
    /// </summary>
    public enum InputableState
    {
        /// <summary>
        /// 入力可能
        /// </summary>
        Enable,

        /// <summary>
        /// 入力不可能
        /// </summary>
        Disable,

    }

}