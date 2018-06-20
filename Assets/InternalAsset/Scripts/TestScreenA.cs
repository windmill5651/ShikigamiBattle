using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kugl.Transition;
using Kugl.Transition.Screen;
using System;

public class TestScreenA : ScreenBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPush()
    {
        TransitionSystem.Instance.TransitionScreen<TestScreenB>();
    }

    protected override IEnumerator OnLoadScreen( ScreenParameterBase param )
    {
        return null;
    }

    protected override IEnumerator OnOpenScreen( ScreenParameterBase param )
    {
        return null;
    }

    protected override IEnumerator OnCloseScreen( ScreenParameterBase param )
    {
        return null;
    }
}
