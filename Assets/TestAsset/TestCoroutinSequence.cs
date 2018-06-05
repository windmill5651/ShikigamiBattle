using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Extensions;

public class TestCoroutinSequence : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var coroutine = ChaindCoroutine.Empty()
            .Continue( () =>
            {
                return Asyncer();
            } )
            .Continue( ()=>
            {
                throw new System.Exception( "エラーだよーん" );
                return Asyncer2();
            } )
            .OnComplete( () =>
            {
                Debug.Log( "Complete" );
            } )
            .OnException( ( e ) =>
            {
                Debug.Log( e.Message );
            } );

        StartCoroutine( coroutine );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Asyncer()
    {
        Debug.Log( "正常コルーチン" );

        yield return new WaitForSeconds( 3.0f );
    }

    private IEnumerator Asyncer2()
    {
        Debug.Log( "エラー発生" );

        yield return new WaitForSeconds( 3.0f );

        throw new System.Exception( "エラーだよーん" );

    }
}
