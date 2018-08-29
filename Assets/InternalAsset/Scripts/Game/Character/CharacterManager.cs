using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Extensions;
using Game.Library.PlayerInput;
using Shikigam.Game.Camera;
using Game.Library.Camera;

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

        /// <summary>
        /// このゲームで登場するすべてのキャラクタ
        /// </summary>
        private List< ShikigamiBattleCharacter > allCharacters = null;

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

            // 全てのキャラクターのリストを生成
            allCharacters = new List< ShikigamiBattleCharacter >();


            for( int i = 0; i < startChatacterCount; i++ )
            {                
                var unit = Instantiate( srcPrefab );
                taskList.Add( unit.SetupAsync( characterParams[ i ] ) );

                allCharacters.Add( unit );
            }

            // 非同期セットアップタスク生成
            var task = ChaindCoroutine.Empty();

            task.Continue( () =>
            {
                return ParallelCoroutine.WhenAll( taskList.ToArray() );
            } )
            .Continue( () =>
            {
                SetControlOwner();
                return null;
            } );

            StartCoroutine( task );
        }

        /// <summary>
        /// コントロールするオーナーを設定します
        /// </summary>
        private void SetControlOwner(  )
        {

            foreach( var character in allCharacters )
            {
                if( character.OwnerType == CharacterOwnerType.PLAYER )
                {
                    var adapter = new InputAdapter();
                    var cameraController = FindObjectOfType< CameraRoot >();

                    var cameraParam = new CameraTargetParameter()
                    {
                        center = character.transform,
                        distance = 15,
                        heightOffset = 3,
                        thetaRestrictionDegree = 50,
                        speed = 10,
                    };

                    cameraController.Setup( cameraParam, character.transform );

                    adapter.Setup( character, cameraController );
                    // プレイヤーの操作系につなげる
                    InputManager.Instance.SetAdapter( adapter, character.OwnerNo );
                }
                else if( character.OwnerType == CharacterOwnerType.AI )
                {
                    // AIの操作系に繋げる
                }
                else if( character.OwnerType == CharacterOwnerType.REPLAY )
                {
                    // リプレイの操作系に繋げる
                }
            }

        }

    }

}
