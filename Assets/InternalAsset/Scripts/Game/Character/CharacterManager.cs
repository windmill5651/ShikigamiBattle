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
                taskList.Add( SetupUnit( unit, characterParams[ i ] ) );
                allCharacters.Add( unit );
            }

            StartCoroutine( ParallelCoroutine.WhenAll( taskList.ToArray() ) );
        }

        /// <summary>
        /// コントロールするオーナーを設定します
        /// </summary>
        private IEnumerator SetupUnit( ShikigamiBattleCharacter unit, BattleCharacterParameter param )
        {

            // キャラクターの向きのベース
            Transform characterDirBase = null;

            if ( param.ownerType == CharacterOwnerType.PLAYER )
            {
                var adapter = new InputAdapter();

                var request = Resources.LoadAsync<CameraRoot>( "GameScene/CameraRoot" );

                yield return request;

                var asset = request.asset as CameraRoot;

                var camera = Instantiate( asset );

                var cameraParam = new CameraTargetParameter()
                {
                    center = unit.transform,
                    distance = 15,
                    heightOffset = 3,
                    thetaRestrictionDegree = 50,
                    speed = 10,
                };

                camera.Setup( cameraParam, unit.transform );
                characterDirBase = camera.CameraTransform;

                adapter.Setup( unit, camera );
                // プレイヤーの操作系につなげる
                InputManager.Instance.SetAdapter( adapter, param.ownerNo );
            }
            else if ( param.ownerType == CharacterOwnerType.AI )
            {
                // AIの操作系に繋げる
            }
            else if ( param.ownerType == CharacterOwnerType.REPLAY )
            {
                // リプレイの操作系に繋げる
            }

            // キャラクターのセットアップ
            yield return unit.SetupAsync( param, characterDirBase );
        }

    }

}
