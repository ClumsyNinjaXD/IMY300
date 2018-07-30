using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
	//Nico Variables
	public AudioClip grappleSound;
	public AudioClip grappleClankSound;
	public AudioClip gulpHealth;
	public AudioClip swordSound;
	public AudioClip swordHit;
	public AudioClip ambientMusic;
	public AudioSource mySource;

	public Texture2D crosshairImage;


	//Nina Variables
	public DistanceJoint2D grapple;
	public bool facingRight = true;
	public bool isSwinging;
	public bool hasHooked = false;
	public bool firstHit = false;
	public bool projectTest = false;
	public bool isSet = false;
	public float grappleTime = 0f;
	RaycastHit2D hit;
	public LineRenderer line;
	public LayerMask mask;
	GameObject pirate;
	GameObject pickups;
	GameObject projectileCol;
	GameObject projectExit;
	GameObject poison;
	GameObject endGame;

	// //TESTING FOR TUTORIAL VIDEO'S
	GameObject GrapCollider;
	GameObject AttCollider;
	// GameObject video;


	float pokeForce; 
	Ray2D ray;
	Vector2 lineR;
	Vector2 values;
	Vector2 mouseCoords;
	Vector2 mouseDirection;
	Vector2 projectileStartingPos;
	Vector3 worldView;

	//Vanblerk Variables
	public int attackDamage = 10;      //Damage of the player's attack  
	public EnemyHealth enemyHealth;    //References the enemy's health

	//Variables for barrel rolling 
	GameObject barrelCollider;
	//GameObject barrel;
	public BarrelRoll barrelroll;


	Animator anim;
	Vector3 target;
	GameObject Enemy;           //References the enemy

	bool enemyInRange;








	void OnGUI()
	{
		float xMin = (Input.mousePosition.x - 40f) + (crosshairImage.width / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - (crosshairImage.height / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
	}









	private void Awake()
	{
		enemyHealth = FindObjectOfType<EnemyHealth>();
		barrelroll = FindObjectOfType<BarrelRoll>();
	}
	void Start()
	{
		// //TESTING FOR TUTORIAL VIDEO'S
		GrapCollider = GameObject.Find ("GrappleVideoCollider");
		AttCollider = GameObject.Find ("AttackVideoCollider");
		// video = GameObject.Find ("GrappleVideo");


		Cursor.visible = false;

		Enemy = GameObject.FindGameObjectWithTag("Enemy"); 
		pirate = GameObject.Find ("Character");
		pickups = GameObject.Find ("Pickups");
		poison = GameObject.Find ("Projectiles");
		projectExit = GameObject.Find ("ProjectilesExit");
		projectileCol = GameObject.Find ("ProjectileCollider");
		barrelCollider = GameObject.Find ("BarrelCollider");
		endGame = GameObject.Find ("EndGameCollider");
		//barrel = GameObject.Find ("Barrel");
		grapple = GetComponent<DistanceJoint2D> ();
		anim = GetComponent<Animator>();
		anim.SetBool("isWalking", false);
		anim.SetBool("isAttacking", false);
		anim.SetBool("isSwinging", false);
		target = transform.position;
		grapple.enabled = false;
		line.enabled = false;
		poison.SetActive (false);
		projectileCol.SetActive (true);
		projectileStartingPos.x = poison.transform.position.x;
		projectileStartingPos.y = poison.transform.position.y;




		//Music
		mySource = GetComponent<AudioSource>();
		mySource.Play();
	}


	// Check to see if the player is in attacking range


	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			//Debug.Log("Enemy in range");
			Enemy = coll.gameObject;
			enemyInRange = true;
		}

		if (coll.gameObject.name == "Platforms") {
			//Debug.Log("platform hit");

		}

		if (coll.gameObject.name == "HealthPickup") {
			pickups.SetActive (false);
			mySource.PlayOneShot(gulpHealth);
		}

		if (coll.gameObject.name == "ProjectileCollider") {
			projectileCol.SetActive (false);
			poison.SetActive (true);
			projectTest = true;
		}	
		if (coll.gameObject.name == "Projectiles") {

			Debug.Log("HHHHHHHHHH");
			poison.SetActive (false);
			projectTest = false;
			//decrement health
			//reset
			poison.transform.position = projectileStartingPos;
			poison.SetActive (true);
			projectTest = true;
			ThrowProjectile (projectTest);
		}

		if (coll.gameObject.name == "ProjectilesExit") {
			projectTest = false;
			poison.SetActive (false);
			projectExit.SetActive (false);

		}
		if (coll.gameObject == endGame) {
			Debug.Log("End Level");
			Application.LoadLevel(Application.loadedLevel);
			//bool isShowing;
			//isShowing = true;
			//video.SetActive(isShowing);
		}

		//if (coll.gameObject.name == "BarrelCollider") {
			
		//}

		//When Player touches collider that causes a barrel to be rolled




		// //TESTING FOR TUTORIAL VIDEO'S
		if (coll.gameObject == GrapCollider) {
			Debug.Log("YAYS");
			GrapCollider.SetActive (false);
			//bool isShowing;
			//isShowing = true;
			//video.SetActive(isShowing);
		}

		if (coll.gameObject == AttCollider) {
			Debug.Log("YAYS2");
			AttCollider.SetActive (false);
			//bool isShowing;
			//isShowing = true;
			//video.SetActive(isShowing);
		}

	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			Debug.Log("Enemy out of range");
			enemyInRange = false;
		}

	}

	 void OnTriggerEnter2D(Collider2D other)
     {

		if (other.gameObject == barrelCollider)
         {
			Debug.Log ("barrelCollider");
			barrelroll.rollBarrel ();

         }
     }

    /* void OnTriggerExit2D(Collider2D other)
     {

     }

     void OnTriggerStay2D(Collider2D other)
     {

     }



     private void OnTriggerEnter(Collider other)
     {

         if (other.gameObject == Enemy)
         {

             enemyInRange = true;

         }
     }

     void OnTriggerExit(Collider other)
     {
         if (other.gameObject == Enemy)
         {
             enemyInRange = false;

         }
     }*/

	// Update is called once per frame
	void Update()
	{


		Movement();
		Attack();
		Swinging();
		if(Input.GetMouseButtonUp(0) == true){
			//moving to on colision
			isSet = false;
			line.enabled = false;
			anim.SetBool("isSwinging", false);
			grapple.enabled = false;
			hasHooked = false;
			firstHit = false;
		} 

		if (Input.GetKey (KeyCode.Mouse0)) {
			if (hasHooked == false) {
				mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint (transform.position);
				ray = new Ray2D (pirate.transform.position, mouseDirection); 
				anim.SetBool ("isSwinging", true);
				hasHooked = true;
				grapple.enabled = true;

				mySource.PlayOneShot(grappleSound);

			}
		}

		//Sounds

		if(isSwinging == true){
			//Play Clank noise to say we've attached
			mySource.PlayOneShot(grappleClankSound);
		}
		if(Input.GetMouseButtonDown(0) == true){
			//moving to on colision
		} 
		if(Input.GetMouseButtonDown(1) == true){

			mySource.PlayOneShot(swordSound);
		}

		// This will change how we do our attack and hit enemy functions. 
		// if(anim.GetBool("isAttacking")){
		//     mySource.PlayOneShot(swordHit);
		// }
		ThrowProjectile(projectTest);

	}

	void Movement()
	{

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			anim.SetBool("isWalking", true);
			transform.Translate(Vector2.right * 5f * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 0);

		}
		else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{

			anim.SetBool("isWalking", true);
			transform.Translate(Vector2.right * 5f * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, -180);
		}
		else
		{
			anim.SetBool("isWalking", false);
		}


	}

	void Attack()
	{
		if (Input.GetKey(KeyCode.Mouse1))
		{
			anim.SetBool("isAttacking", true);

			if (enemyInRange)
			{
				//Debug.Log("in Range and attacking");
				enemyHealth = Enemy.GetComponent<EnemyHealth>();
				enemyHealth.TakeDamage(attackDamage);
			}
		}
		else
		{
			anim.SetBool("isAttacking", false);
		}
	}

	void Swinging()
	{

		if (Input.GetKey(KeyCode.Mouse0))
		{

			//if statement to ensure that you can't drag the mouse and the coordinates change
			if (hasHooked == false) {

				/*worldView = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.x, pirate.transform.position.z));
				mouseDirection = worldView - pirate.transform.position;
				mouseDirection.Normalize();*/

				/*ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
				ray.direction.Set (ray.direction.x, ray.direction.y, 0);*/
				//set it's max distance


			}
			hit = Physics2D.Raycast (ray.origin, ray.direction, 15f);
			//hit.rigidbody.AddForceAtPosition(ray.direction, hit.point);


			if (hit.collider != null) {

				Debug.Log ("platform hit");

				// mySource.PlayClipAtPoint(grappleSound, transform.position);
				// mySource.Play(grappleSound);
				// mySource.PlayClipAtPoint(grappleSound, new Vector3(5, 1, 0));
				// mySource.PlayOneShot(grappleSound);

				if (firstHit == false) {
					mySource.PlayOneShot (grappleClankSound);
					firstHit = true;
				}

				//if the mouse click is to the right
				if (pirate.transform.position.x < Input.mousePosition.x) {

					values.x = hit.point.x + 3.7f;
					values.y = hit.point.y;
					lineR.x = hit.point.x + 0f;
					lineR.y = hit.point.y;
					grapple.connectedAnchor = values;
					line.enabled = true;
					line.SetPosition (1, lineR);
					line.SetPosition (0, transform.position);



				} else {
					grapple.connectedAnchor = hit.point;
					lineR.x = hit.point.x + 0f;
					lineR.y = hit.point.y;
					line.enabled = true;
					line.SetPosition (0, transform.position);
					line.SetPosition (1, lineR);
				}

				while (grapple.distance > 2f) {
					grappleTime = grappleTime + 0.00001f;	
					grapple.distance = grapple.distance - grappleTime;
					
				}
					

			}//end of if collide
			else {


				mouseCoords = ray.GetPoint (5f);
				grapple.enabled = false;
				//grapple.connectedAnchor = mouseCoords;
				//mouseCoords = lineLimit();
				line.enabled = true;
				line.SetPosition (0, transform.position);
				line.SetPosition (1, mouseCoords);
				StartCoroutine(Example());

			}


		}
		
	}

	public void AdjustVolume (float newVolume) {
     	AudioListener.volume = newVolume;
 	}

	IEnumerator Example()
	{
		yield return new WaitForSeconds(0.3f);
		line.enabled = false;
	}

	public void ThrowProjectile(bool project){
		if (project == true) {
			Vector2 projectileCoords;
			projectileCoords.x = poison.transform.position.x + 40f;
			projectileCoords.y = poison.transform.position.y - 7f;
			poison.transform.position = Vector2.MoveTowards (poison.transform.position, projectileCoords, 3 * Time.deltaTime);
		} 
	}


}