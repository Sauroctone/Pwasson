using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Static")
			Destroy (gameObject);
	}
}
