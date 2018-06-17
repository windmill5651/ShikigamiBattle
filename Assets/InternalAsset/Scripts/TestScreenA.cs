using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kugl.Transition;

public class TestScreenA : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPush()
    {
        StartCoroutine( TransitionSystem.Instance.TransitionScreenAsync( "TestSceneB" ) );
    }
}
