using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float speed;
	public float speedMultiplier;
	private float moveSpeedStore;

	public float speedIncreaseMilestone;
	private float speedIncreaseMilestoneStore;
	private float speedMilestoneCount;
	private float speedMilestoneCountStore;

	public float jumpSpeed;
	public float movement;
	private Rigidbody2D rigidBody;
	
	public LayerMask coinLayer;
	public bool facingRight;

	public bool grounded;
	public LayerMask groundLayer;
	//private Collider2D myCollider;
	public Transform groundCheck;
	public float groundCheckRadius;

	public float jumpTime;
	private float jumpTimeCounter;
	private Animator myAnimator;

	public GameManager theGameManager;

	private bool stoppedJumping;

	private bool canDoubleJump;

	public AudioSource jumpSound;
	public AudioSource deathSound;

	public ParticleSystem footsteps;
	private ParticleSystem.EmissionModule footEmission;

	public ParticleSystem powerup1;
	private ParticleSystem.EmissionModule powerup1Emission;

	public ParticleSystem powerup2;
	private ParticleSystem.EmissionModule powerup2Emission;
	public PowerupManager thePowerupManager;

	Collider2D mace;
	private bool attacking = false;

	void Start()
	{
		facingRight = true;

		rigidBody = GetComponent<Rigidbody2D>();

		//myCollider = GetComponent<Collider2D>();

		myAnimator = GetComponent<Animator>();

		jumpTimeCounter = jumpTime;

		speedMilestoneCount = speedIncreaseMilestone;

		moveSpeedStore = speed;

		speedMilestoneCountStore = speedMilestoneCount;

		speedIncreaseMilestoneStore = speedIncreaseMilestone;

		stoppedJumping = true;

		footEmission = footsteps.emission;
		powerup1Emission = powerup1.emission;
		powerup2Emission = powerup2.emission;
		thePowerupManager = FindObjectOfType<PowerupManager>();

		mace = GameObject.Find("Mace").GetComponent<Collider2D>();


	}

	// Update is called once per frame
	void Update()
	{

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		//grounded = Physics2D.IsTouchingLayers(myCollider, groundLayer);

		if (transform.position.x > speedMilestoneCount)
        {
			speedMilestoneCount += speedIncreaseMilestone;

			speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;

			speed = speed * speedMultiplier;
        }

		rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
		

		if (Input.GetButtonDown("Jump"))
		{
			if (grounded)
			{
				rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
				stoppedJumping = false;
				jumpSound.Play();
				
			} 

			if(!grounded && canDoubleJump == true)
            {
				rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
				jumpTimeCounter = jumpSpeed;
				canDoubleJump = false;
				stoppedJumping = false;
				jumpSound.Play();
			}

		}
		
		else if (Input.GetButton("Jump") && !stoppedJumping && grounded)
		{
			if (jumpTimeCounter > 0)
			{
				rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if(Input.GetButtonUp("Jump") && rigidBody.velocity.y > 0)
        {
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
			jumpTimeCounter = 0;
			stoppedJumping = true;
        }

		if(grounded)
        {
			jumpTimeCounter = jumpSpeed;
			canDoubleJump = true;
		}

		//show footsteps
		if (grounded)
        {
			footEmission.rateOverTime = 35f;
        } else
        {
			footEmission.rateOverTime = 0f;
        }

		//show powerup coin
		if (thePowerupManager.getPowerupActive() == true && thePowerupManager.getDoublePoints() == true)
		{
			powerup1Emission.rateOverTime = 100f;
		}
		else
		{
			powerup1Emission.rateOverTime = 0f;
		}

		//show powerup safe
		if (thePowerupManager.getPowerupActive() == true && thePowerupManager.getSafeMode() == true)
		{
			powerup2Emission.rateOverTime = 50f;
		}
		else
		{
			powerup2Emission.rateOverTime = 0f;
		}

		//Settare variabili ANIMAZIONI
		//myAnimator.SetFloat("speed", rigidBody.velocity.x);
		myAnimator.SetBool("Grounded", grounded); 

		Flip(movement);

		if (Input.GetButtonDown("Attack") && !attacking)
		{
			myAnimator.SetTrigger("Attack");
			mace.enabled = true;
			//timeBtwAttack = startTimeBtwAttack;

			attacking = true;
		}

		if (Input.GetButtonUp("Attack") && attacking)
		{
			/*timeBtwAttack -= Time.deltaTime;
        }
        else
        {*/
			attacking = false;
			mace.enabled = false;
			myAnimator.ResetTrigger("Attack");
		}

	}

	void OnCollisionEnter2D (Collision2D other)
    {
		if(other.gameObject.tag == "KillBox")
        {
			theGameManager.RestartGame();
			speed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedIncreaseMilestone = speedIncreaseMilestoneStore;
			deathSound.Play();
		}
    }


	//Flip Personaggio
	public void Flip(float movement)
	{
		if (movement > 0 && !facingRight || movement < 0 && facingRight)
        {
			facingRight = !facingRight;

			Vector3 theScale = transform.localScale;

			theScale.x *= -1;

			transform.localScale = theScale;
        }
	}


	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
			Destroy(other.gameObject);
        }
    }

}