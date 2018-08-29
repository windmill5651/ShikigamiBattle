using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shikigami.Game.Character;
using Shikigam.Game.Camera;

namespace Shikigami.Game
{

    public class GameStartUp : MonoBehaviour
    {
        [SerializeField]
        CameraRoot cameraRoot;

        // Use this for initialization
        void Start()
        {

            var paramList = new List< BattleCharacterParameter >();

            var param = new BattleCharacterParameter()
            {
                characterId = 0,
                ownerType = CharacterOwnerType.PLAYER,
                baseDir = cameraRoot.CameraTransform,
            };

            paramList.Add( param );

            CharacterManager.Instance.Setup( paramList );

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
