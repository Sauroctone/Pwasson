using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_1 : MonoBehaviour {

	public float targetDistance;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemyMovementSpeed;
	public float damping;
	public float shootDistance;
	//public float stoppingDistance = 10f;
	public Transform target;
	Rigidbody theRigidBody;
	Renderer myRender;
	private NavMeshAgent navMesh;
	private bool isCouvert;


	// Use this for initialization
	void Start () {
		myRender = GetComponent<Renderer> ();
		theRigidBody = GetComponent<Rigidbody> ();
		navMesh = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = new Ray(transform.position,(target.position - transform.position));
		RaycastHit hit;
		targetDistance = Vector3.Distance (target.position, transform.position);


		if (targetDistance > enemyLookDistance) {
			myRender.material.color = Color.blue;
			print ("Lost My Target !");
		} 
		else if (targetDistance <= enemyLookDistance && targetDistance>attackDistance) {
			myRender.material.color = Color.yellow;
			print ("looking At Player");
		} 
		else if (targetDistance <= attackDistance && targetDistance > shootDistance) {
			myRender.material.color = Color.red;
			moveToPlayer ();
			print ("ATTACK !");
		} 
		//if(Physics.Raycast(ray, out hit){
		//	if(hit.collider.tag == "environment"){
		//		isCouvert = true;
		//	}
		//}

		if (targetDistance <= shootDistance) {
			print ("shooting the bastard !");
		}
	}

	void lookAtPlayer(){
		Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
	}
	void moveToPlayer(){
		navMesh.SetDestination(target.position);
	}

}
