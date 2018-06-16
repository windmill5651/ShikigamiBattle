using System.Collections;
using System.Collections.Generic;
using UnityEngine.Extensions;

public class SingletonTest : SingletonMonoBehaviourBase< SingletonTest > {

    public int SingletonInt
    {
        get { return 2; }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
