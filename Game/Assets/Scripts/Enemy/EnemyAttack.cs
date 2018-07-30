using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f; 	// Time in seconds between attacks
	public int attackDamage = 1;				// Amount of damage to player

	Animator anim;								// Reference to animator component.
	GameObject player;							// Reference to the player GameObject.
	PlayerHealth playerHealth;					// Reference to the player's health.
	EnemyHealth enemyHealth; 					// Reference to this enemy's health.
	bool playerInRange; 						// Whether player is within the trigger collider and can be damaged
	float timer;								// For counting up to next attack				

	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		anim = GetComponent<Animator>();
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

	/*void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)	//if colliding with player
		{
			Debug.Log ("Player in range");
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player) 
		{
			playerInRange = false;
		}
	}*/
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && playerInRange) {
			Attack ();
		} 
		else if(playerInRange == false)
		{			
			anim.SetBool ("isAttacking", false);
		}

		if (playerHealth.currentHealth <= 0) 
		{
			Debug.Log ("Player Dead");
			//anim.SetTrigger("PlayerDead);
		}
	}

	void Attack()
	{
		timer = 0f;

		anim.SetBool("isAttacking", true);
		if (playerHealth.currentHealth > 0) 
		{
			playerHealth.PlayerTakeDamage (attackDamage);
		}
	}
}
