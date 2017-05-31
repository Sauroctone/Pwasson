using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public enum GunType {AK47, Shotgun, Harpoon};
	public GunType state;

	public GameObject AK47;
	public GameObject Shotgun;
	public GameObject Harpoon;
	public GameObject harpoonInGun;

	public bool isFiring;
	public bool isUsingHarpoon;

	public BulletController bullet;
	public BulletController bulletHarpoon;

	public float bulletSpeed;
	public float bulletSpreadRange;
	public float bulletsPerShot;
	public float timeBetweenShots;
	private float shotCounter;

	public Transform firePoint;

	private AudioSource source;
	public AudioClip shootSound;
	private float volLowRange = 0.8f;
	private float volHighRange = 1f;

	// Use this for initialization
	void Awake ()
	{
		source = GetComponent<AudioSource> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (state) {
		case GunType.AK47:
			AK47.SetActive (true);
			Shotgun.SetActive (false);
			Harpoon.SetActive (false);

			bulletSpeed = 40f;
			bulletSpreadRange = 5f;
			bulletsPerShot = 1f;
			timeBetweenShots = 0.1f;
			break;

		case GunType.Shotgun:
			AK47.SetActive (false);
			Shotgun.SetActive (true);
			Harpoon.SetActive (false);

			bulletSpeed = 40f;
			bulletSpreadRange = 7f;
			bulletsPerShot = 7f;
			timeBetweenShots = 1f;
			break;

		case GunType.Harpoon:
			AK47.SetActive (false);
			Shotgun.SetActive (false);
			Harpoon.SetActive (true);

			bulletSpeed = 30f;
			bulletSpreadRange = 0f;
			timeBetweenShots = 2f;

			break;
		}


		shotCounter -= Time.deltaTime;
		if (shotCounter <= 0) 
		{
			shotCounter = 0;

			if (state == GunType.Harpoon)
				harpoonInGun.SetActive (true);
		}

		if (isFiring) 
		{
			if (shotCounter <= 0f) 
			{
				shotCounter = timeBetweenShots;

				for (int i = 0; i < bulletsPerShot; i++) 
				{
					BulletController newBullet = Instantiate (bullet, firePoint.position, Quaternion.Euler (firePoint.eulerAngles.x, firePoint.eulerAngles.y + Random.Range (-bulletSpreadRange, bulletSpreadRange), firePoint.eulerAngles.z)) as BulletController;

					newBullet.speed = bulletSpeed; 
				}

				float vol = Random.Range (volLowRange, volHighRange);
				source.PlayOneShot (shootSound, vol);	
			}
		}

		else if (isUsingHarpoon)
		{
			if (shotCounter <= 0f)
			{
				harpoonInGun.SetActive (false);
				shotCounter = timeBetweenShots;

				BulletController newBullet = Instantiate(bulletHarpoon, firePoint.position, Quaternion.Euler (firePoint.eulerAngles.x, firePoint.eulerAngles.y + Random.Range (-bulletSpreadRange, bulletSpreadRange), firePoint.eulerAngles.z)) as BulletController;
			
				newBullet.speed = bulletSpeed;
			}
		}
	}
}