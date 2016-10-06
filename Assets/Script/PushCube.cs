using UnityEngine;
using System.Collections;

//public static float pushpower = 2.0f;

public class PushCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag == "Cube")
		{
			
		}
	}
}
