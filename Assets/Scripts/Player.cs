//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

// Creates a class for the Player script requiring a CharacterController
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Allows access to xbox controller buttons
    public XboxController m_controller;

	// GameObject used to access variables for the hook
	public GameObject m_hook;

	// Allows for access to camera that uses Cinemachine
	public GameObject m_cineCamera;

	// AudioClip used to store the audio for when Otto runs
	public AudioClip m_runningAudio;

	// Stores the jumping audio for use in script
	public AudioClip m_jumpingAudio;

	// Clip represents audio that will be played when Otto gets hit
	public AudioClip m_hitAudio;

	// AudioClip used for when Otto dies in game
	public AudioClip m_deathAudio;

	// Stores the audio for when Otto throws his scarf
	public AudioClip m_throwAudio;

	// Used to access the SkinnedMeshRenderer component from the player
	public SkinnedMeshRenderer m_meshRenderer;

	// Indicates the layer mask of a mushroom object
	public LayerMask m_mushroomLayer;

	// Represents the ground as a layer
	public LayerMask m_ground;

	// Represents the particle system for when the player lands
	public ParticleSystem m_landing;

	// Scarf particle system used when scarf is thrown
	public ParticleSystem m_scarfWrap;

	// Particla System indicates the grass particle system when Otto runs
	public ParticleSystem m_grass;

	// Public color indicates what colour the player will flash after getting hit
	public Color m_flashColour;

	// Image stores the Image that represents the player's health
    public Image m_healthImage;

	// Sprite represents when the player is on half of its health
    public Image m_halfHealth;

	// Indicates the sprite shown when the player is at full health
    public Image m_fullHealth;

	// Integer indicates what scene number the main menu is in unity
	public int m_nMainMenu;

    // Public float represents the speed of the player's movement
    public float m_fSpeed = 10.0f;

	// Float indicates the height of the player's jump
	public float m_fJumpHeight = 1.0f;

	// Represents the time it takes to reach the max height of a jump
	public float m_fTimeToJumpApex = 0.4f;

	// Stores how tall Otto is as a float
	public float m_fHeight = 2.0f;

	// Represents the left control stick dead zone for movement
	[Range(0.01f, 1.0f)]
	public float m_fDeadZone;

	// Float indicates the time when player cannot be hit (allows floats from 1-5)
	[Range(1.0f, 5.0f)]
    public float m_fHealthRecoveryTime = 3.0f;

	// Float used to demonstrate the rate the player flashes (range from 10-100)
	[Range(10.0f, 100.0f)]
    public float m_fFlashRate = 10.0f;

	// Indicates the amount of time given after the player falls for them to jump
	[Range(0.01f, 0.2f)]
	public float m_fFallRecovery = 0.15f;

	// Used to access the animator component from the player
	private Animator m_animator;

    // Private variable used to store the player's CharacterController in
    private CharacterController m_cc;

    // Allows access to the BasicEnemy script
    private BasicEnemy m_enemyScript;

    // Variable is used to store the player's Grappling Hook script in
    private Hook m_grapplingScript;

	// Records the original colour of the player
	private Color m_originalColour;

	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// Collectable manager Script used to get access to the collectable manager script
	private CollectableManager m_cm;

	// Stores a reference to the Cinemachine Camera script
	private CineCamera m_cineCameraScript;

	// Used to access and change the audio source component on the player
	private AudioSource m_audioSource;

	// Represents the platform layer in the game
	[SerializeField]
	private LayerMask m_platformLayer;

	// Vector3 represents the input direction from the analog stick
	private Vector3 m_v3MoveDirection;

    // Vector3 represents the direction the player will look in
    private Vector3 m_v3LookDirection;

	// Private Vector3 indicates the position where the player spawns
	private Vector3 m_v3StartPosition;

    // Vector3 allows gravity to be applied in movement formulas
    private Vector3 m_v3Velocity;

	// Represents gravity applied to the player
	private float m_fGravity;

	// Float indicates the velo9city of Otto's jump
	private float m_fJumpVelocity;

	// Keeps track of the velocity of Otto on the Y axis
	private float m_fVelocityY;

	// Stores the initial jump apex calculations on awake
	private float m_fInitialJumpApex;

	// Stores the initial gravity calculation on awake
	private float m_fInitialGravity;

	// Private float indicates the rate the player flashes after being hit
    private float m_fFlashingRate;

	// Keeps track of how long the playercan be invicible for after getting hit
	private float m_fHealthTimer;

	// Records the amount of time the player spends whilst jumping
	private float m_fJumpTimer;

	private float m_fWaitTimer;

	// Float indicates the forward direction of Otto
	private float m_fForward;

	// Keeps track of the player's sideways direction
	private float m_fSideways;

	// Int keeps track of the player's current health
	private int m_nHealth;

	private int m_nPrevHealth;

    // Bool used to let the script know if the player has jumped from ground
    private bool m_bJumped;

	// Indicates if the player is recovering after a hit or not
    private bool m_bRecovering;

	// Bool is activated when the player is performing a mini jump
	private bool m_bMiniJump;

	// Detects if the player has bounced off a mushroom or not
	private bool m_bBounced;

	public GameObject m_fadeToBlack;

	private DeathFade m_deathFade;

	//--------------------------------------------------------------------------------
	// Function is used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
    {
		m_deathFade = m_fadeToBlack.GetComponent<DeathFade>();

		// Gets reference to the collectable manager gameObject.
		m_collectableManager = GameObject.FindGameObjectWithTag("CollectableManager");

		// Checks if the collectable manager object was found
		if (m_collectableManager)
		{
			// Gets reference to the collectable manager script.
			m_cm = m_collectableManager.GetComponent<CollectableManager>();
		}

		// Gets the Animator component and stores it in the animator variable
        m_animator = GetComponent<Animator>();

		// Gets Audio Source component off of the player
		m_audioSource = GetComponent<AudioSource>();

		// Gets the CharacterController component on awake
		m_cc = GetComponent<CharacterController>();

		// When script is called, the BasicEnemy script is obtained and stored
		m_enemyScript = GetComponent<BasicEnemy>();

		// Gets the GrapplingHook script and stores it in the variable
		m_grapplingScript = m_hook.GetComponent<Hook>();

		// Gets the Camera Script from the Camera Object if the object is attached
		if (m_cineCamera != null)
		{
			m_cineCameraScript = m_cineCamera.GetComponent<CineCamera>();
		}

		// Disables half health image initially
		m_halfHealth.enabled = false;

		// Sets all bools in the animator controller to false initially
		m_animator.SetBool("Walking", false);
		m_animator.SetBool("Running", false);
		m_animator.SetBool("Grapple", false);
		m_animator.SetBool("Jumping", false);
		m_animator.SetBool("Falling", false);
		m_animator.SetBool("Landing", false);

		// Stores the full heath sprite as the current health image
		m_healthImage = m_fullHealth;

		// Obtains the original colour for the player from the mesh renderer's material
		m_originalColour = m_meshRenderer.material.color;

		// Records the start position as Otto's position on awake
		m_v3StartPosition = transform.position;

		// Initialises all private floats to equal zero
		m_fHealthTimer = 0.0f;
		m_fJumpTimer = 0.0f;
		m_fWaitTimer = 0.0f;
		m_fForward = 0.0f;
		m_fSideways = 0.0f;
		m_fVelocityY = 0.0f;

		// Stores the initial time to jump apex
		m_fInitialJumpApex = m_fTimeToJumpApex;

		// Calculates the flashing rate from the reciprocal of public float flash rate
		m_fFlashingRate = 1 / m_fFlashRate;

		// Calculates gravity based on the jump height and time to apex
		m_fGravity = -(2 * m_fJumpHeight) / Mathf.Pow(m_fTimeToJumpApex, 2);

		// Stores the initial gravity for later use
		m_fInitialGravity = m_fGravity;

		// Calculates the jump velocity from gravity
		m_fJumpVelocity = Mathf.Abs(m_fGravity) * m_fTimeToJumpApex;

		// Sets the health of the player to equal two when script is called
		m_nHealth = 2;

		m_nPrevHealth = m_nHealth;

		// Sets private bools to false on awake
		m_bJumped = false;
        m_bRecovering = false;
		m_bMiniJump = false;
		m_bBounced = false;
	}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
		// Calls Move, PlatformDetection and Animate functions in conjunction per frame
		Move();
		//PlatformDetection();
		Animate();
	}

	//--------------------------------------------------------------------------------
	// Function allows the player to Move and Jump.
	//--------------------------------------------------------------------------------
	private void Move()
	{
		// Detects if the player is on the ground
		if (m_cc.isGrounded)
		{
			// Resets y velocity and jump timer back to zero
			m_fVelocityY = 0.0f;
			m_fJumpTimer = 0.0f;

			// Resets the time to jump apex to what it was on awake.
			m_fTimeToJumpApex = m_fInitialJumpApex;

			// Allows for targets to be set for the grappling hook
			m_grapplingScript.SetLaunchable(true);

			// Resets the mini jump and bounced booleans to false
			m_bMiniJump = false;
			m_bBounced = false;
		}
		// Otherwise adds jump timer by delta time if Otto isn't grounded
		else
		{
			m_fJumpTimer += Time.deltaTime;
		}

		// Detects if the left stick is centred
		if (Input.GetAxis("LeftStickHorizontal") < 0.1f && 
			Input.GetAxis("LeftStickVertical") < 0.1f &&
			Input.GetAxis("LeftStickHorizontal") > -0.1f &&
			Input.GetAxis("LeftStickVertical") > -0.1f)
		{
			// Sets input floats based on the keyboard axes
			m_fForward = Input.GetAxis("KeyboardHorizontal");
			m_fSideways = Input.GetAxis("KeyboardVertical");
		}
		// Else if left stick is not centred
		else
		{
			// Sets input floats based on the controller's axes
			m_fForward = Input.GetAxis("LeftStickHorizontal");
			m_fSideways = Input.GetAxis("LeftStickVertical");
		}

		// Creates a "new" vector3 for the player to move based on input
		m_v3MoveDirection = new Vector3(m_fForward, 0, m_fSideways);

		// Stops player if a cut scene is playing and Otto needs to wait
		if (m_cineCameraScript != null && m_cineCameraScript.GetPlayerWait())
		{
			m_v3MoveDirection = Vector3.zero;
		}
		// Otherwise he can move freely if the player is not in a cutscene
		else
		{
			// Detects if Grappling Hook is hooked on an object or launched
			if (m_grapplingScript.GetHooked() || m_grapplingScript.GetFired())
			{
				// Disables any Y velocity to be applied to the player
				m_fVelocityY = 0;

				// Restricts movement by a little
				m_v3MoveDirection *= 0.6f;

				// Sets launchable boolean to false in hook script
				m_grapplingScript.SetLaunchable(false);
			}
			else
			{
				// Calls jump function if button is pressed whilst grounded or if Otto just fell
				if ((Input.GetButtonDown("Jump") && m_cc.isGrounded) ||
					(Input.GetButtonDown("Jump") && m_fJumpTimer < m_fFallRecovery && !m_bJumped))
				{
					Jump();
				}
				// Sets mini jump bool to true if the jump button is let go before apex is reached
				else if (Input.GetButtonUp("Jump") && !m_cc.isGrounded &&
						 m_fJumpTimer < m_fTimeToJumpApex && !m_bBounced)
				{
					m_bMiniJump = true;
				}

				// Sets mini jump bool to false if the player has bounced
				if (m_bBounced)
				{
					m_bMiniJump = false;
				}

				// Decreases Y Velocity by 2 if mini jump is true and y velocity is a positive
				if (m_bMiniJump && m_fVelocityY >= 0)
				{
					m_fVelocityY -= 2.0f;
				}
			}

			// Stores the y movement direction in local float
			float fCurrentMoveY = m_v3MoveDirection.y;
			

			// Calculates the move direction in camera space rather than world space
			m_v3MoveDirection = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * 
													 m_v3MoveDirection;

			// Stores local float value as the y value for the Move Direction
			m_v3MoveDirection.y = fCurrentMoveY;

			// Normalizes the move vector if the magnitude exceeds 1
			if (m_v3MoveDirection.sqrMagnitude > 1.0f)
			{
				m_v3MoveDirection.Normalize();
			}
		}

		// Increases gravity if the player is near their apex
		if (m_fVelocityY <= 3.0f && m_fVelocityY >= 0.0f && 
			!m_cc.isGrounded && !m_bMiniJump)
		{
			m_fGravity += 2.0f;
		}
		// Decreases gravity just after the player reaches their apex
		else if (m_fVelocityY < 0.0f && m_fGravity > m_fInitialGravity && 
				 !m_cc.isGrounded && !m_bMiniJump)
		{
			m_fGravity -= 2.0f;
		}
		// Otherwise sets gravity back to its initial value
		else
		{
			m_fGravity = m_fInitialGravity;
		}

		// Caps the y velocity to -30 if the value goes below -30
		if (m_fVelocityY <= -30)
		{
			m_fVelocityY = -30;
		}

		// Sets Y Velocity to zero if the player hits his head above something
		if (UpCheck())
		{
			m_fVelocityY = 0.0f;
		}

		// Applies the gravity to y velocity if the player hasn't launched hook
		if (!m_grapplingScript.GetFired())
		{
			m_fVelocityY += m_fGravity * Time.deltaTime;
		}

		// Multiples Move Direction vector by speed and the y velocity
		m_v3Velocity = m_v3MoveDirection * m_fSpeed + Vector3.up * m_fVelocityY;

		// Adds movement to CharacterController based on move direction and delta time
		m_cc.Move(m_v3Velocity * Time.deltaTime);

		// Checks if the magnitude of the move direction is greater than 0.1
		if (m_v3MoveDirection.sqrMagnitude > 0.1f)
		{
			// Calculates the look direction by adding the position with the move direction
			m_v3LookDirection = transform.position + m_v3MoveDirection.normalized;

			// Sets the y value of look direction to equal the player's y position
			m_v3LookDirection.y = transform.position.y;

			// Makes the player face the direction that they are walking
			transform.LookAt(m_v3LookDirection, Vector3.up);
		}

		// Detects if the player is in recovery mode
		if (m_bRecovering)
		{
			// Updates the health timer every second by deltaTime
			m_fHealthTimer += Time.deltaTime;

			// Detects if the sine wave value is less than 0
			if (Mathf.Sin(m_fHealthTimer / m_fFlashingRate) >= 0)
			{
				// Sets Otto's material colour to equal that of the flash colour
				m_meshRenderer.material.color = m_flashColour;
			}
			// Otherwise the player's colour is reverted to his initial colour
			else
			{
				m_meshRenderer.material.color = m_originalColour;
			}

			// Checks if the health timer exceeds the maximum recovery time
			if (m_fHealthTimer >= m_fHealthRecoveryTime)
			{
				// Sets the health timer to equal zero
				m_fHealthTimer = 0.0f;

				// Reverts recovery bool back to false
				m_bRecovering = false;

				// Otto's colour is set to his original colour
				m_meshRenderer.material.color = m_originalColour;
			}
		}
	}

	//--------------------------------------------------------------------------------
	// Function animates Otto based on the player's actions.
	//--------------------------------------------------------------------------------
	private void Animate()
	{
		// Detects if the player is in a cutscene
		if (m_cineCameraScript != null && m_cineCameraScript.GetPlayerWait())
		{
			// Sets all bools in the animator controller to false so Otto goes to idle
			m_animator.SetBool("Running", false);
			m_animator.SetBool("Grapple", false);
			m_animator.SetBool("Jumping", false);
			m_animator.SetBool("Falling", false);
			m_animator.SetBool("Landing", false);
			m_animator.SetBool("Bounce", false);
			m_animator.SetBool("Damaged", false);
			m_animator.SetBool("Dying", false);
			m_animator.SetBool("Waiting", false);
		}
		// Runs if gameplay is running
		else
		{
			// Checks if jumped is true and Gravity exceeds jump speed
			if (m_bJumped && m_fVelocityY > 0.0f)
			{
				// Sets jumping bool to true in animator
				m_animator.SetBool("Jumping", true);
			}
			// Sets Jumping bool in animator to false otherwise
			else
			{
				m_animator.SetBool("Jumping", false);
			}

			// Sets Falling bool in animator to true if jumped and Gravity exceeds jump speed
			if (m_bJumped && m_fVelocityY <= 0.0f)
			{
				m_animator.SetBool("Falling", true);
			}
			// Sets Falling bool in animator to false if the player is grounded
			else if (m_cc.isGrounded)
			{
				m_animator.SetBool("Falling", false);
			}

			// Checks if the player has jumped and is grounded
			if (m_bJumped && m_cc.isGrounded)
			{
				// Sets Landing bool in animator to true
				m_animator.SetBool("Landing", true);

				// Resets Jumped bool back to false and and Jump Timer to zero
				m_bJumped = false;
			}
			// Sets Landing bool in animator to false otherwise
			else
			{
				m_animator.SetBool("Landing", false);
			}

			// Detects if player has launched hook or is hooked
			if (m_grapplingScript.GetFired() && !m_grapplingScript.GetHooked())
			{
				m_animator.SetBool("Grapple", true);

				m_scarfWrap.Play();
			}
			// Sets Grapple bool in animator to false otherwise
			else
			{
				m_animator.SetBool("Grapple", false);
			}

			// Sets Running bool in animator to true if the player has any running movement
			if (m_v3MoveDirection.sqrMagnitude > 0.0f)
			{
				m_animator.SetBool("Running", true);
			}
			// Sets Running bool in animator to false if player is not running
			else
			{
				m_animator.SetBool("Running", false);

				m_fWaitTimer += Time.deltaTime;
			}

			if (m_bBounced)
			{
				m_animator.SetBool("Bounce", true);
			}
			else
			{
				m_animator.SetBool("Bounce", false);
			}
			
			if (m_nPrevHealth > m_nHealth && m_nHealth != 0)
			{
				m_animator.SetBool("Damaged", true);

				m_nPrevHealth = m_nHealth;
			}
			else
			{
				m_animator.SetBool("Damaged", false);
			}

			if (m_nHealth == 0)
			{
				m_animator.SetBool("Dying", true);
			}
			else
			{
				m_animator.SetBool("Dying", false);
			}

			if (m_fWaitTimer >= 5.0f)
			{
				m_animator.SetBool("Waiting", true);

				m_fWaitTimer = 0.0f;
			}
			else
			{
				m_animator.SetBool("Waiting", false);
			}
		}
	}

	//--------------------------------------------------------------------------------
	// Function plays the footsteps audio for every step Otto takes.
	//--------------------------------------------------------------------------------
	private void Footstep()
	{
		m_audioSource.PlayOneShot(m_runningAudio);
	}

	//--------------------------------------------------------------------------------
	// Function plays the jump audio when called in animator.
	//--------------------------------------------------------------------------------
	private void JumpAudio()
	{
		m_audioSource.PlayOneShot(m_jumpingAudio);
	}

	//--------------------------------------------------------------------------------
	// Function applies a jump force to the player when called.
	//--------------------------------------------------------------------------------
	private void Jump()
	{
		// Calculates the jump nvelocity based on gravity and the height of the jump
		m_fJumpVelocity = Mathf.Sqrt(-2 * m_fGravity * m_fJumpHeight);

		// Sets the y velocity to equal the jump velocity
		m_fVelocityY = m_fJumpVelocity;

		// Sets jumped bool to true
		m_bJumped = true;
	}

	//--------------------------------------------------------------------------------
	// Function deducts health from the player when called.
	//--------------------------------------------------------------------------------
	public void Damage()
    {
		// Detects if the health timer is at zero or if the player isn't recovering
        if ((m_fHealthTimer <= 0.0f || !m_bRecovering) && !m_grapplingScript.GetFired())
        {
			// Deducts one bar of health from the player and updates UI
            m_nHealth -= 1;

			// Enables half health UI and disables full health UI
			m_halfHealth.enabled = true;
			m_fullHealth.enabled = false;

			// Calls the death function if the player's health is zero
            if (m_nHealth <= 0)
            {
                Death();
            }
			// Otherwise if the player still has health
            else
            {
				// Sets the player to be recovering
				m_bRecovering = true;

				// Plays the audio for being hit through the audio source
				m_audioSource.PlayOneShot(m_hitAudio);
            }           
        }
    }

	//--------------------------------------------------------------------------------
	// Function restores the player's health when called.
	//--------------------------------------------------------------------------------
	public void RestoreHealth()
    {
		// Detects if the health is not full
        if (m_nHealth != 2)
        {
			// Initialises health to be at full health
            m_nHealth = 2;

			// Disables half health UI and enables full health UI
			m_halfHealth.enabled = false;
			m_fullHealth.enabled = true;
		}
    }

    //--------------------------------------------------------------------------------
    // Function gets called when Otto dies in game.
    //--------------------------------------------------------------------------------
    private void Death()
    {
		// Plays the death audio using the audio source on the player
		m_audioSource.PlayOneShot(m_deathAudio);

		// Removes a life via the collectable manager
		m_cm.RemoveLife();

		// Fades to black when dead
		m_deathFade.DoFadeIn();

		// Detects if the player has any lives left in the game
		if (m_cm.GetLives() > 0)
		{
			StartCoroutine(SpawnDelay());
			// Fades out from black when respawned
			m_deathFade.DoFadeOut();
		}
		// Sends the user back to the main menu if they have no lives left
		else
		{
			SceneManager.LoadScene(m_nMainMenu);
		}

		// Calls the restore health function
		RestoreHealth();
	}

	//--------------------------------------------------------------------------------
	// Function applies a bounce force to the player.
	//
	// Param:
	//		fBounceForce: Indicates how hard Otto will be bounced.
	//--------------------------------------------------------------------------------
	public void Bounce(float fBounceForce)
	{
		// Checks if the jump button has been interracted with
		if (Input.GetButton("Jump"))
		{
			// Sets the y velocity to a bounce force which is increased by 50%
			m_fVelocityY = m_fJumpVelocity * fBounceForce * 1.5f;
		}
		// Otherwise ets the y velocity to a bounce force by the jump velocity
		else
		{
			m_fVelocityY = m_fJumpVelocity * fBounceForce;
		}

		// Sets launchable boolean to false in hook script
		m_grapplingScript.SetLaunchable(true);

		// Sets jumped and bounced bools to be true
		m_bJumped = true;
		m_bBounced = true;

		// Multiples Move Direction vector by speed and the bounce velocity
		m_v3Velocity = m_v3MoveDirection * m_fSpeed + Vector3.up * m_fVelocityY;

		// Adds the bounce velocity to the CharacterController
		m_cc.Move(m_v3Velocity * Time.deltaTime);
	}

	//--------------------------------------------------------------------------------
	// Function detects if there is an object above Otto.
	//
	// Return:
	//		Returns the result of an upward raycast.
	//--------------------------------------------------------------------------------
	private bool UpCheck()
	{
		return Physics.Raycast(transform.position + Vector3.up * m_fHeight, Vector3.up, 0.5f);
	}

    //--------------------------------------------------------------------------------
    // Function is called when the player is enters a trigger.
    //
    // Param:
    //      other: Represents the collider of the trigger.
    //--------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
		// Calls the death function other's tag is "Respawn" and velocity exceeds 30
        if (other.CompareTag("Respawn") && m_fVelocityY <= -30)
        {
            Death();
        }

		if (other.gameObject.name == "Hitbox" && m_grapplingScript.GetHooked())
		{
			Bounce(20.0f);
		}
    }

	IEnumerator SpawnDelay()
	{
		// Waits for 0.1 seconds before being called
		yield return new WaitForSeconds(2.0f);

		// Sends player back to the checkpoint if Otto has passed one
		if (m_cm.GetCheckPoint() != null)
		{
			transform.position = m_cm.GetCheckPoint().position;
		}
		// Otherwise sets the player's position back to where they spawned
		else
		{
			transform.position = m_v3StartPosition;
		}

		m_nHealth = 2;

		// Disables half health UI and enables full health UI
		m_halfHealth.enabled = false;
		m_fullHealth.enabled = true;
	}
}
