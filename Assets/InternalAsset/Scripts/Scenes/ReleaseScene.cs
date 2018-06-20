using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kugl.Transition.Scene;
using System;

public class ReleaseScene : SceneBase {


    protected override IEnumerator OnCloseScene()
    {
        Debug.Log( "OnCloseScene" );
        yield break;
    }

    protected override IEnumerator OnLoadScene( SceneParameterBase param )
    {
        Debug.Log( "OnLoadScene" );
        yield break;
    }

    protected override IEnumerator OnOpenScene( SceneParameterBase param )
    {
        Debug.Log( "OnOpenScene" );
        yield break;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
