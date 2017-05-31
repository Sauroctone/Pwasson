using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingFromBullet : MonoBehaviour {

	public Player player;

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Bullet") 
		{
			player.state = Player.PlayerStates.Dead;
		} 
	}
}
