using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	//Tutoriel : https://www.youtube.com/watch?v=lkDGk3TjsIE

	public float moveSpeed;
	private Rigidbody myRigidbody;

	private Vector3 moveInput;
	private Vector3 moveVelocity;	

	public GunController theGun;

	public enum PlayerStates {Alive, Dead};
	public PlayerStates state;

	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody> ();
	}
	

	void Update () 
	{
		if (state != PlayerStates.Dead) 
		{
			moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, Input.GetAxisRaw ("Vertical"));
			moveVelocity = moveInput * moveSpeed;

			Vector3 aimInput = Vector3.right * Input.GetAxisRaw ("Right_Horizontal") + Vector3.forward * -Input.GetAxisRaw ("Right_Vertical");

			if (aimInput.sqrMagnitude > 0.0f) {
				transform.rotation = Quaternion.LookRotation (aimInput, Vector3.up);
			}

			if (Input.GetAxisRaw ("Right_Trigger") == 1) {
				if (theGun.state == GunController.GunType.Harpoon)
					theGun.isUsingHarpoon = true;
				else
					theGun.isFiring = true;
			} else {
				if (theGun.state == GunController.GunType.Harpoon)
					theGun.isUsingHarpoon = false;
				else
					theGun.isFiring = false;
			}

			if (Input.GetButtonDown ("Right_Bumper") && !theGun.isFiring && !theGun.isUsingHarpoon) {
				switch (theGun.state) {
				case GunController.GunType.AK47:
					theGun.state = GunController.GunType.Shotgun;
					break;
				case GunController.GunType.Shotgun:
					theGun.state = GunController.GunType.Harpoon;
					break;
				case GunController.GunType.Harpoon:
					theGun.state = GunController.GunType.AK47;
					break;
				}
			}

			if (Input.GetButtonDown ("Left_Bumper") && !theGun.isFiring && !theGun.isUsingHarpoon) {
				switch (theGun.state) {
				case GunController.GunType.AK47:
					theGun.state = GunController.GunType.Harpoon;
					break;
				case GunController.GunType.Shotgun:
					theGun.state = GunController.GunType.AK47;
					break;
				case GunController.GunType.Harpoon:
					theGun.state = GunController.GunType.Shotgun;
					break;
				}
			}
		}

		else if (Input.GetButtonDown("A_Button"))
		{
			//Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene("poisson", LoadSceneMode.Single);
			state = PlayerStates.Alive;
		}
	}

	void FixedUpdate ()
	{
		myRigidbody.velocity = moveVelocity;
	}
}
