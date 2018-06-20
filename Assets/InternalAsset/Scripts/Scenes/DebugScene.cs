using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kugl.Transition.Scene;
using System;

public class DebugScene : SceneBase {


    protected override IEnumerator OnCloseScene()
    {
        Debug.Log( "CloseScene" );
        yield return null;
    }

    protected override IEnumerator OnLoadScene( SceneParameterBase param )
    {
        Debug.Log( "OnLoadScene" );
        yield return null;
    }

    protected override IEnumerator OnOpenScene( SceneParameterBase param )
    {
        Debug.Log( "OnOpen" );
        yield return null;
    }

}
