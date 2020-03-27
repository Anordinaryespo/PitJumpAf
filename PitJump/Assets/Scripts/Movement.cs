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

		//Settare variabili ANIMAZIONI
		//myAnimator.SetFloat("Speed", rigidBody.velocity.x);
		myAnimator.SetBool("Grounded", grounded); 

		Flip(movement);

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