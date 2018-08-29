using System.Collections.Generic;
using UnityEngine.Extensions;
using Shikigami.System;

namespace Game.Library.PlayerInput
{

    public class InputManager : SingletonMonoBehaviourBase<InputManager>
    {

        List< InputOwnerBase > ownerList;

        public void SetAdapter( IInputAdapter adapter, int ownerNo )
        {
            // オーナーリストがなければ生成する
            if( ownerList == null )
            {
                ownerList = new List< InputOwnerBase >(); 
            }

            var owner = new ShikigamiPlayerInput();

            owner.Initialize( adapter );

            ownerList.Add( owner );

        }

        private void Update()
        {
            if( ownerList == null )
            {
                return;
            }
            foreach( var owner in ownerList )
            {
                owner.OnUpdate();
            }
        }
    }

}
