using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDataManager {



    /// <summary>
    /// バトルキャラクターのステータスを取得します。
    /// </summary>
    /// <param name="characterId">キャラクターのIDです</param>
    /// <returns>バトルキャラクターのID</returns>
    public static MasterBattleCharacterStatus GetBattleCharacterStatus( int characterId )
    {
        return new MasterBattleCharacterStatus()
        {
            moveSpeed = 100,
            dashSpeed = 300,
            isGravityHalf = false,
        };
    }

}
