using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kugl.Transition;
using System;

public class DebugScene : SceneBase {


    public override IEnumerator OnCloseScene()
    {
        Debug.Log( "CloseScene" );
        yield return null;
    }

    public override IEnumerator OnLoadScene( SceneParameterBase param )
    {
        Debug.Log( "OnLoadScene" );
        yield return null;
    }

    public override IEnumerator OnOpenScene( SceneParameterBase param )
    {
        Debug.Log( "OnOpen" );
        yield return null;
    }

}
