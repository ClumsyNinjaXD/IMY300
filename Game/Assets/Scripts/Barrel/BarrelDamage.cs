using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDamage : MonoBehaviour {
	public float timeBetweenDamage = 0.5f;		// The intervals between which the barrel can damage you
	public int barrelDamage = 5;				// Amount of damage the barrel does
	GameObject player;							// Reference to the player GameObject.
	PlayerHealth playerHealth;					// Reference to the player's health.
	bool playerInRange; 						// Whether player is within the trigger collider and can be damaged
	float timer;								// For counting up to next damage


	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{			
			playerInRange = true;
		}

	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{			
			playerInRange = false;
		}

	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

		if (timer >= timeBetweenDamage &&playerInRange) {
			damagePlayer ();
		} 

	}

	void damagePlayer (){
		timer = 0f;
		if (playerHealth.currentHealth > 0) 
		{
			playerHealth.PlayerTakeDamage (barrelDamage);
		}
	}
}
