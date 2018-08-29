using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Extensions;

/// <summary>
/// 式神のキャラクター名前空間です
/// </summary>
namespace Shikigami.Game.Character
{
    /// <summary>
    ///  CharacterManager
    ///  キャラクターを管理するクラスです
    /// </summary>
    public class CharacterManager : SingletonMonoBehaviourBase< CharacterManager >
    {
        #region フィールド/プロパティ

        /// <summary>
        /// フィールド上にいるバトルキャラ
        /// </summary>
        private List< ShikigamiBattleCharacter > fieldCharacters = null;

        #endregion

        /// <summary>
        /// セットアップを行います。
        /// </summary>
        /// <param name="characterParams">キャラクターのパラメータです</param>
        public void Setup( List< BattleCharacterParameter >  characterParams )
        {
            var startChatacterCount = characterParams.Count;

            var controlUnitResName = CharacterConst.GetCharacterUnitResName();
            var srcPrefab = Resources.Load< ShikigamiBattleCharacter >( "GameScene/" + controlUnitResName );

            var taskList = new List< IEnumerator >();


            for( int i = 0; i < startChatacterCount; i++ )
            {                
                var unit = Instantiate( srcPrefab );
                taskList.Add( unit.SetupAsync( characterParams[ i ] ) );
            }

            StartCoroutine( ParallelCoroutine.WhenAll( taskList.ToArray() ) );
        }

    }

}
