using UnityEngine;
using System.Collections;

public class destroyMe : MonoBehaviour{

    float timer;
    public float deathtimer = 10;


	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, deathtimer);
	}

}
