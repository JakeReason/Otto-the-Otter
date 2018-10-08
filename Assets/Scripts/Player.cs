//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
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

	// Represents the particle system for when the player lands
	public ParticleSystem m_landing;

	// Scarf particle system used when scarf is thrown
	public ParticleSystem m_scarfWrap;

	// Particla System indicates the grass particle system when Otto runs
	public ParticleSystem m_grass;

	// GameObject used to access variables for the hook
	public GameObject m_hook;

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

	// Is the maximum angle of the ground that otto can walk on without slipping
	[Range(0f, 90f)]
	public float m_fMaximumGroundAngle = 35.0f;

	// Represents the
	[Range(0.01f, 1.0f)]
	public float m_fDeadZone;

	// Float indicates the time when player cannot be hit (allows floats from 1-5)
	[Range(1.0f, 5.0f)]
    public float m_fHealthRecoveryTime = 3.0f;

	// Float used to demonstrate the rate the player flashes (range from 10-100)
	[Range(10.0f, 100.0f)]
    public float m_fFlashRate = 10.0f;

	[Range(0.01f, 0.2f)]
	public float m_fFallRecovery = 0.15f;

	// Used to access the SkinnedMeshRenderer component from the player
	public SkinnedMeshRenderer m_meshRenderer;

	// Indicates the layer mask of a mushroom object
	public LayerMask m_mushroomLayer;

	public LayerMask m_ground;

	// Used to access the animator component from the player
	private Animator m_animator;

    // Private variable used to store the player's CharacterController in
    private CharacterController m_cc;

    // Allows access to the BasicEnemy script
    private BasicEnemy m_enemyScript;

    // Variable is used to store the player's Grappling Hook script in
    private Hook m_grapplingScript;

    // Vector3 represents the input direction from the analog stick
    private Vector3 m_v3MoveDirection;

    // Vector3 represents the direction the player will look in
    private Vector3 m_v3LookDirection;

	private Vector3 m_v3StartPosition;

    // Vector3 allows gravity to be applied in movement formulas
    private Vector3 m_v3Velocity;

	private float m_fGroundAngle;

	private float m_fGravity;

	private float m_fJumpVelocity;

	private float m_fVelocityY;

	// Private float indicates the rate the player flashes after being hit
    private float m_fFlashingRate;

	// Keeps track of how long the playercan be invicible for after getting hit
	private float m_fHealthTimer;

	private float m_fJumpTimer;

	private float m_fForward;

	private float m_fSideways;

	// Int keeps track of the player's current health
	private int m_nHealth;

    // Bool used to let the script know if the player has jumped from ground
    private bool m_bJumped;

	// Indicates if the player is recovering after a hit or not
    private bool m_bRecovering;

	private RaycastHit m_hitDown;

	private Transform m_cameraTransform;

	// Records the original colour of the player
    private Color m_originalColour;

	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// Collectable manager Script used to get access to the collectable manager script
	private CollectableManager m_cm;

	private AudioSource m_audioSource;

	[SerializeField]
	private LayerMask m_platformLayer;

	//--------------------------------------------------------------------------------
	// Function is used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
    {
		// Gets reference to the collectable manager gameObject.
		m_collectableManager = GameObject.FindGameObjectWithTag("CollectableManager");

		if (m_collectableManager)
		{
			// Gets reference to the collectable manager script.
			m_cm = m_collectableManager.GetComponent<CollectableManager>();
		}

		// Calculates the flashing rate from the reciprocal of public float flash rate
		m_fFlashingRate = 1 / m_fFlashRate;

		// Gets the Animator component and stores it in the animator variable
        m_animator = GetComponent<Animator>();

		// Sets all bools in the animator controller to false
		m_animator.SetBool("Moving", false);
		m_animator.SetBool("Grapple", false);
		m_animator.SetBool("Jumping", false);
		m_animator.SetBool("Falling", false);
		m_animator.SetBool("Landing", false);

		// Stores the full heath sprite as the current health image
		m_healthImage = m_fullHealth;

        // Gets the CharacterController component on awake
        m_cc = GetComponent<CharacterController>();

        // When script is called, the BasicEnemy script is obtained and stored
        m_enemyScript = GetComponent<BasicEnemy>();

        // Gets the GrapplingHook script and stores it in the variable
        m_grapplingScript = m_hook.GetComponent<Hook>();

		// Obtains the original colour for the player from the mesh renderer's material
		m_originalColour = m_meshRenderer.material.color;

		m_cameraTransform = Camera.main.transform;

		m_v3StartPosition = transform.position;

		// Initialises all private floats to equal zero
		m_fGroundAngle = 0.0f;
		m_fHealthTimer = 0.0f;
		m_fJumpTimer = 0.0f;
		m_fForward = 0.0f;
		m_fSideways = 0.0f;
		m_fVelocityY = 0.0f;

		m_fGravity = -(2 * m_fJumpHeight) / Mathf.Pow(m_fTimeToJumpApex, 2);
		m_fJumpVelocity = Mathf.Abs(m_fGravity) * m_fTimeToJumpApex;

		// Sets the health of the player to equal two when script is called
		m_nHealth = 2;

        // Sets private bools to false on awake
        m_bJumped = false;
        m_bRecovering = false;

		m_audioSource = GetComponent<AudioSource>();
	}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
		// Calls both Move and Animate functions in conjunction per frame
		Move();
		PlatformDetection();
		Animate();
	}

	//--------------------------------------------------------------------------------
	// Function allows the player to Move and Jump.
	//--------------------------------------------------------------------------------
	private void Move()
	{
		m_fGroundAngle = Vector3.Angle(Vector3.up, m_hitDown.normal);

		//if (m_fGroundAngle >= m_fMaximumGroundAngle)
		//{
		//	m_cc.Move(CalculateSlideVector() * Time.deltaTime * 10.0f);
		//	return;
		//}

		if (IsGrounded())
		{
			m_fVelocityY = 0.0f;
			m_fJumpTimer = 0.0f;
			m_grapplingScript.SetLaunchable(true);
		}
		else
		{
			m_fJumpTimer += Time.deltaTime;
		}

		if (Input.GetAxis("LeftStickHorizontal") < 0.1f && 
			Input.GetAxis("LeftStickVertical") < 0.1f &&
			Input.GetAxis("LeftStickHorizontal") > -0.1f &&
			Input.GetAxis("LeftStickVertical") > -0.1f)
		{
			m_fForward = Input.GetAxis("KeyboardHorizontal");
			m_fSideways = Input.GetAxis("KeyboardVertical");
		}
		else
		{
			m_fForward = Input.GetAxis("LeftStickHorizontal");
			m_fSideways = Input.GetAxis("LeftStickVertical");
		}

		m_v3MoveDirection = new Vector3(m_fForward, 0, m_fSideways);

		// Detects if Grappling Hook is hooked on an object
		if (m_grapplingScript.GetHooked() || m_grapplingScript.GetFired())
		{
			m_fVelocityY = 0;

			m_v3MoveDirection *= 0.25f;

			m_grapplingScript.SetLaunchable(false);
		}
		// Else if the hook hasn't hooked an object or if the player hasn't bounced
		if ((Input.GetButtonDown("Jump") && IsGrounded()) || 
			(Input.GetButtonDown("Jump") && m_fJumpTimer < m_fFallRecovery && !m_bJumped))
		{
			Jump();
		}

		// Stores the y movement direction in local float
		float fCurrentMoveY = m_v3MoveDirection.y;

		// Calculates the move direction in camera space rather than world space
		m_v3MoveDirection = Camera.main.transform.rotation * m_v3MoveDirection;

		// Stores local float value as the y value for the Move Direction
		m_v3MoveDirection.y = fCurrentMoveY;

		if (m_v3MoveDirection.sqrMagnitude > 1.0f)
		{
			m_v3MoveDirection.Normalize();
		}

		if (m_fVelocityY <= -30)
		{
			m_fVelocityY = -30;
		}

		//Debug.Log(m_fGroundAngle);

		// Applies the gravity to the move direction
		m_fVelocityY += m_fGravity * Time.deltaTime;

		//Debug.Log(m_fVelocityY);

		// Multiples Move Direction vector by speed
		m_v3Velocity = m_v3MoveDirection * m_fSpeed + Vector3.up * m_fVelocityY;

		//if (m_fGroundAngle >= m_fMaximumGroundAngle)
		//{
		//	Debug.DrawLine(transform.position, m_hitDown.normal, Color.cyan);
		//	m_cc.Move(m_v3SlideVector * Time.deltaTime);
		//}

		// Adds movement to CharacterController based on move direction and delta time
		m_cc.Move(m_v3Velocity * Time.deltaTime);

		// Sets the look direction to equal the direction the control stick is facing
		m_v3LookDirection = new Vector3(m_v3MoveDirection.x, 0, m_v3MoveDirection.z);

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

			if (Mathf.Sin(m_fHealthTimer / m_fFlashingRate) >= 0)
			{
				m_meshRenderer.material.color = m_flashColour;
			}
			else
			{
				m_meshRenderer.material.color = m_originalColour;
			}

			if (m_fHealthTimer >= m_fHealthRecoveryTime)
			{
				m_fHealthTimer = 0.0f;
				m_bRecovering = false;
				m_meshRenderer.material.color = m_originalColour;
			}
		}
	}

	//--------------------------------------------------------------------------------
	// Function animates Otto based on the player's actions.
	//--------------------------------------------------------------------------------
	private void Animate()
	{
		// Sets Falling bool in animator to true if jumped and Gravity exceeds jump speed
		if (m_bJumped && m_fVelocityY > 0.0f)
		{
			m_animator.SetBool("Jumping", true);
			m_audioSource.PlayOneShot(m_jumpingAudio);
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
		// Sets Falling bool in animator to false otherwise
		else if (IsGrounded())
		{
			m_animator.SetBool("Falling", false);
		}

		// Checks if the player has jumped and is grounded
		if (m_bJumped && IsGrounded())
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

		// Sets Grapple bool in animator to true if player has launched hook or is hooked
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
		if (m_v3MoveDirection.sqrMagnitude >= 0.7f)
		{
			m_animator.SetBool("Running", true);

			if (!m_audioSource.isPlaying)
			{
				m_audioSource.time = 0.0f;
				m_audioSource.PlayOneShot(m_runningAudio);
			}
		}
		// Sets Running bool in animator to false if player is not running
		else
		{
			m_animator.SetBool("Running", false);
		}

		// Sets Moving bool in animator to true if the player has any movement
		if (m_v3MoveDirection.sqrMagnitude > 0.1f && m_v3MoveDirection.sqrMagnitude < 0.99f)
		{
			m_animator.SetBool("Walking", true);
		}
		// Sets Moving bool in animator to falseif player is still
		else
		{
			m_animator.SetBool("Walking", false);
		}
	}

	private void Footstep()
	{
		m_audioSource.PlayOneShot(m_throwAudio);
	}

	private Vector3 CalculateSlideVector()
	{
		Vector3 v3Parallel = m_hitDown.normal;
		v3Parallel.y = m_hitDown.normal.y * -1;

		return v3Parallel;
	}

	private bool IsGrounded()
	{
		if (m_cc.isGrounded)
		{
			return true;
		}

		//if (Physics.Raycast(transform.position, Vector3.down, out m_hitDown, 0.1f, m_ground))
		//{
		//	m_cc.Move(new Vector3(0, -m_hitDown.distance, 0));
		//	return true;
		//}

		return false;
	}

	//private void CalculateHitForward()
	//{
	//	if (Physics.Raycast(transform.position, Vector3.forward, out m_hitForward, 0.5f))
	//	{
	//		return;
	//	}
	//}

	private void Jump()
	{
		m_fJumpVelocity = Mathf.Sqrt(-2 * m_fGravity * m_fJumpHeight);
		m_fVelocityY = m_fJumpVelocity;
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
			m_halfHealth.enabled = true;
			m_fullHealth.enabled = false;

			// Calls the death function if the player's health is zero
            if (m_nHealth <= 0)
            {
                Death();
            }
			// Sets the player to be recovering if the player still has health
            else
            {
                m_bRecovering = true;
				m_audioSource.PlayOneShot(m_hitAudio);
            }           
        }
    }

	//--------------------------------------------------------------------------------
	// Function restores the player's health when called.
	//--------------------------------------------------------------------------------
	public void RestoreHealth()
    {
		// Updates health and UI if the player is not already at full health
        if (m_nHealth != 2)
        {
            m_nHealth = 2;

			m_halfHealth.enabled = false;
			m_fullHealth.enabled = true;
		}
    }

    //--------------------------------------------------------------------------------
    // Function sends player to the start of the level when health is zero.
    //--------------------------------------------------------------------------------
    private void Death()
    {
		m_audioSource.PlayOneShot(m_deathAudio);
		m_cm.RemoveLife();

		if (m_cm.GetLives() > 0)
		{
			if (!m_cm.GetCheckPoint())
			{
				// Sets the player's position back to where they spawned
				transform.position = m_v3StartPosition;
			}

			transform.position = m_cm.GetCheckPoint().position;
		}
		else
		{
			// Sets the player's position back to where they spawned
			SceneManager.LoadScene(m_nMainMenu);
		}

		transform.position = Vector3.zero;
		
		// Resets health back to full health and updates the UI
        m_nHealth = 2;

		m_halfHealth.enabled = false;
		m_fullHealth.enabled = true;
	}

	public void Bounce(float fBounceForce)
	{
		// Sets the y value of gravity to equal jump speed multipled by bounce force
		m_fVelocityY = m_fJumpVelocity * fBounceForce;

		// Sets jumped bool to be true
		m_bJumped = true;

		//Vector3 v3Bounce = new Vector3(0, m_fVelocityY, 0);

		//m_cc.SimpleMove(v3Bounce * Time.deltaTime);
	}

	private bool UpCheck()
	{
		return Physics.Raycast(transform.position, Vector3.up, 0.5f);
	}

	void PlatformDetection()
	{
		RaycastHit hit;

		// Sets forward vector to the transforms forward.
		Vector3 down = transform.TransformDirection(Vector3.down);

		if (Physics.Raycast(transform.position, down, out hit, 1, m_platformLayer))
		{
			transform.parent = hit.transform;
		}
		else
		{
			transform.parent = null;
		}
	}

    //--------------------------------------------------------------------------------
    // Function is called when the player is enters a trigger.
    //
    // Param:
    //      other: Represents the collider of the trigger.
    //--------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
		// Calls the death function if the colliding object has the "Respawn" tag
        if (other.CompareTag("Respawn") && m_fVelocityY <= -30)
        {
            Death();
        }
    }

	//private void DebugLines()
	//{
	//	if (!m_bDebug) return;

	//	Debug.DrawLine(transform.position, transform.position + m_v3Forward, Color.cyan);
	//	Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.magenta);
	//}
}
