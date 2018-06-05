using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Extensions;

public class TestCoroutinSequence : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var coroutine = new SafeCoroutine( Asyncer );

        StartCoroutine( coroutine );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Asyncer()
    {
        Debug.Log( "tekitounaExceptionHassei" );

        yield return new WaitForSeconds( 3.0f );

        throw new System.Exception( "エラーだよーん" );
    }
}
