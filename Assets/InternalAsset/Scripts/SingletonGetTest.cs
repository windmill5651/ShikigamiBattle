using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonGetTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log( SingletonTest.Instance.SingletonInt );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
