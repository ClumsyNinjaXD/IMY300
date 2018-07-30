using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {


    public int startingHealth = 500;                         // Enemy's starting health
    public int currentHealth;                                // Enemy's current health  
    public float flashSpeed = 5f;                            // Speed at which damage flashes
    public Color flashColour = new Color(0, 0, 0, 0);  //Enemy flashes red when taking damage
    public SpriteRenderer damageImage;
    // public float sinkSpeed = 2.5f;                        // Speed at which an enemy sinks through floor when killed  
    // public int scoreValue = 10;                           //Score that enemy is worth
    // public AudioClip deathclip;                           //Sound that plays when enemy is killed
    public AudioClip deathclip;




    Animator anim;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool damaged;
    // AudioSource enemyAudio;
    // bool isSinking;


    public AudioSource mySource;


    void Awake ()
    {
        anim = GetComponent<Animator>();
        // enemyAudio = GetComponent();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
        damageImage = GetComponent<SpriteRenderer>();
        mySource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		if(damaged)
        {
            //Sound
            // mySource = GetComponent<AudioSource>();
            mySource.Stop();
            mySource.Play();

           damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = new Color(255, 255, 255, 255);
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {

        if(isDead == true)
        {
            return;
        }
        damaged = true;

        Debug.Log("enemy taking damage");

        //enemyAudio.Play();  //Play hurt sound

        currentHealth -= amount;


        if(currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        mySource.Stop();
        mySource.PlayOneShot(deathclip);
        // Turn the collider into a trigger so shots can pass through it.
        // capsuleCollider.isTrigger = true;

        // Tell the animator that the enemy is dead.
        // anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        // enemyAudio.clip = deathClip;
        // enemyAudio.Play();
        Destroy(gameObject, 0f);
    }
}
