using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 10;								// The amount of health the player starts the game with.
	public int currentHealth;									// The current health the player has.
	Text text;  												// Text to Modify how many Lives we have.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public SpriteRenderer damageImage;                          // Reference to an image to flash on the screen on being hurt.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(0, 0, 0, 0);     		// The colour the damageImage is set to, to flash.
	public GameObject HealthUI;									//To Reference the healthUI			


	Animator anim;                                              // Reference to the Animator component.
	PolygonCollider2D polygonCollider;							// References the players collider.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.


	void Awake()
	{
		anim = GetComponent <Animator>();
		HealthUI = GameObject.FindGameObjectWithTag ("HealthUI");
		//polygonCollider = getComponent<
		//playerAudio = GetComponent<AudioSource> ();
		text = HealthUI.GetComponent <Text> ();
		damageImage = GetComponent<SpriteRenderer>();
		currentHealth = startingHealth;
	}
	

	void Update () 
	{
		text.text = "x" + currentHealth;
		// If player takes damage
		if (damaged) 
		{
			//set player colour to red
			damageImage.color = flashColour;
			text.text = "x" + currentHealth;
		}
		else 
		{
			damageImage.color = new Color(255, 255, 255, 255);
		}
		damaged = false;
	}

	public void PlayerTakeDamage(int amount)
	{

		if (isDead == true) {
			return;
		}
		damaged = true;

		Debug.Log ("player taking damage");

		//enemyAudio.Play();  //Play hurt sound

		currentHealth -= amount;


		if (currentHealth <= 0) {
			Death ();
		}

	}

	void Death()
	{
		Debug.Log ("Player dead");
	}
}
