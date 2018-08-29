using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shikigami.Game.Character;

namespace Shikigami.Game
{

    public class GameStartUp : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

            var param = new BattleCharacterParameter()
            {
                characterId = 0,
            };

            CharacterManager.Instance.Setup( new List< BattleCharacterParameter >() { param } );

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
