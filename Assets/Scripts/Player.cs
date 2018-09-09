//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

// Creates a class for the Player script requiring a CharacterController
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Allows access to xbox controller buttons
    public XboxController m_controller;

	public GameObject m_hook;

	// Public color indicates what colour the player will flash after getting hit
    public Color m_flashColour;

	// Image stores the Image that represents the player's health
    public Image m_healthImage;

	// Sprite represents when the player is on half of its health
    public Image m_halfHealth;

	// Indicates the sprite shown when the player is at full health
    public Image m_fullHealth;

    // Public float represents the speed of the player's movement
    public float m_fSpeed = 10.0f;

    // Float indicates the speed of the player's jump
    public float m_fJumpSpeed = 8.0f;

	[Range(0.01f, 1.0f)]
	public float m_fDeadZone;

    // Public float adds more gravity when player is falling (allows floats from 1-10)
    [Range(1.0f, 10.0f)]
    public float m_fExtraGravity = 4.0f;

	// Float indicates the time when player cannot be hit (allows floats from 1-5)
	[Range(1.0f, 5.0f)]
    public float m_fHealthRecoveryTime = 3.0f;

    // Indicates maximum time for player to jump before falling (range from 0.1-1)
    [Range(0.1f, 1.0f)]
    public float m_fJumpTimeLimit = 0.4f;

    // Represents how far on the x a player can move while jumping (range from 0.1-1)
    [Range(0.1f, 1.0f)]
    public float m_fJumpMoveLimit = 0.5f;

	// Float used to demonstrate the rate the player flashes (range from 10-100)
	[Range(10.0f, 100.0f)]
    public float m_fFlashRate = 10.0f;

	// Used to access the animator component from the player
    private Animator m_animator;

    // Private variable used to store the player's CharacterController in
    private CharacterController m_cc;

    // Allows access to the BasicEnemy script
    private BasicEnemy m_enemyScript;

    // Variable is used to store the player's Grappling Hook script in
    private Hook m_grapplingScript;

	// Used to access the SkinnedMeshRenderer component from the player
    public SkinnedMeshRenderer m_meshRenderer;

	// Indicates the layer mask of a mushroom object
	public LayerMask m_mushroomLayer;

    // Vector3 represents the input direction from the analog stick
    private Vector3 m_v3MoveDirection;

	// Private Vector3 stores the direction the player should move
	private Vector3 m_v3MoveVector;

    // Vector3 represents the direction the player will look in
    private Vector3 m_v3LookDirection;

    // Private Vector3 indicates the previous frame looking direction direction
    private Vector3 m_v3PreviousLook;

    // Vector3 allows gravity to be applied in movement formulas
    private Vector3 m_v3Gravity;

    // Jump Timer represents how long the player has been jumping for
    private float m_fJumpTimer;

	// Keeps track of how long the playercan be invicible for after getting hit
    private float m_fHealthTimer;

	// Private float indicates the rate the player flashes after being hit
    private float m_fFlashingRate;

	// Int keeps track of the player's current health
    private int m_nHealth;

    // Bool used to let the script know if the player has jumped from ground
    private bool m_bJumped;

	// Indicates if the player is recovering after a hit or not
    private bool m_bRecovering;

	// Records the original colour of the player
    private Color m_originalColour;

	// Collectable manager GameObject used to get access to the collectable manager.
	private GameObject m_collectableManager;

	// Collectable manager Script used to get access to the collectable manager script.
	private CollectableManager m_CM;

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
			m_CM = m_collectableManager.GetComponent<CollectableManager>();
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

        // Initialises the Move, Look and Gravity Direction all to equal the zero Vector3
        m_v3MoveDirection = Vector3.zero;
		m_v3MoveVector = Vector3.zero;
		m_v3LookDirection = Vector3.zero;
        m_v3Gravity = Vector3.zero;

        // Initialises private floats to equal zero
        m_fJumpTimer = 0.0f;
        m_fHealthTimer = 0.0f;

        // Sets the health of the player to equal two when script is called
        m_nHealth = 2;

        // Sets private bools to false on awake
        m_bJumped = false;
        m_bRecovering = false;
	}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
		// Calls both Move and Animate functions in conjunction per frame
		Move();
		Animate();
	}

	//--------------------------------------------------------------------------------
	// Function allows the player to Move and Jump.
	//--------------------------------------------------------------------------------
	private void Move()
	{
		// Creates a new Vector3 indicating which direction the left stick is facing
		m_v3MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0,
										Input.GetAxis("Vertical"));

		// Sets the look direction to equal the direction the control stick is facing
		m_v3LookDirection = new Vector3(m_v3MoveDirection.x, 0, m_v3MoveDirection.z);

		// Checks if there is any movement from the control stick
		if (m_v3LookDirection.x < 0.1f && m_v3LookDirection.z < 0.1f)
		{
			// Makes the look direction equal the previous frame's look direction
			m_v3LookDirection = m_v3PreviousLook;
		}
		// Else if there is input from the control stick
		else
		{
			// Stores the look direction to be the previous frame direction
			m_v3PreviousLook = m_v3LookDirection;
		}

		// Detects if Grappling Hook is hooked on an object
		if (m_grapplingScript.GetHooked())
		{
			// Sets gravity to equal the zero Vector3
			m_v3Gravity = Vector3.zero;
		}
		// Else if the hook hasn't hooked an object or if the player hasn't bounced
		else
		{
			// Checks if Jump is pressed, the player is grounded and if they haven't jumped
			if (Input.GetButton("Jump") && m_cc.isGrounded && !m_bJumped)
			{
				// Sets the y value of gravity to equal the jump speed
				m_v3Gravity.y = m_fJumpSpeed;

				// Sets jumped bool to be true
				m_bJumped = true;
			}
			// Else if Jump isn't pressed and player is in air or player has jumped too long
			else if ((!Input.GetButton("Jump") && !m_cc.isGrounded) ||
					  m_fJumpTimer > m_fJumpTimeLimit)
			{
				// Applies gravity to player with an extra multiplier to fall quicker
				m_v3Gravity += Physics.gravity * m_fExtraGravity * Time.deltaTime;
			}
		}

		// Checks if the player is in the air
		if (m_bJumped)
		{
			// Adds the jump timer to deltaTime every second
			m_fJumpTimer += Time.deltaTime;
		}

		// Stores the y movement direction in local float
		float fCurrentMoveY = m_v3MoveDirection.y;

		// Calculates the move direction in camera space rather than world space
		m_v3MoveDirection = Camera.main.transform.rotation * m_v3MoveDirection;

		// Stores local float value as the y value for the Move Direction
		m_v3MoveDirection.y = fCurrentMoveY;

		// Multiples Move Direction vector by speed
		m_v3MoveVector = m_v3MoveDirection *= m_fSpeed;

		// Applies the gravity to the move direction
		m_v3MoveVector = m_v3MoveDirection + m_v3Gravity;

		// Makes player stop moving if the Grappling Hook has been fired or hooked
		if (m_grapplingScript.GetHooked() || m_grapplingScript.GetFired())
		{
			m_v3MoveDirection = Vector3.zero;
		}

		if (m_v3MoveDirection.sqrMagnitude > 1.0f)
		{
			m_v3MoveDirection.Normalize();
		}

		// Adds movement to CharacterController based on move direction and delta time
		m_cc.Move(m_v3MoveVector * Time.deltaTime);

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
		if (m_bJumped && m_v3Gravity.y == m_fJumpSpeed)
		{
			m_animator.SetBool("Jumping", true);
		}
		// Sets Jumping bool in animator to false otherwise
		else
		{
			m_animator.SetBool("Jumping", false);
		}
		
		// Sets Falling bool in animator to true if jumped and Gravity exceeds jump speed
		if (m_bJumped && m_v3Gravity.y >= m_fJumpSpeed)
		{
			m_animator.SetBool("Falling", true);
		}
		// Sets Falling bool in animator to false otherwise
		else
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
			m_fJumpTimer = 0.0f;
		}
		// Sets Landing bool in animator to false otherwise
		else
		{
			m_animator.SetBool("Landing", false);
		}

		// Sets Grapple bool in animator to true if player has launched hook or is hooked
		if (m_grapplingScript.GetHooked() || m_grapplingScript.GetFired())
		{
			m_animator.SetBool("Grapple", true);
		}
		// Sets Grapple bool in animator to false otherwise
		else
		{
			m_animator.SetBool("Grapple", false);
		}

		// Sets Moving bool in animator to true if the player has any movement
		if (m_v3MoveDirection.sqrMagnitude > 0.1f)
		{
			m_animator.SetBool("Moving", true);
		}
		// Sets Moving bool in animator to falseif player is still
		else
		{
			m_animator.SetBool("Moving", false);
		}
	}

	//--------------------------------------------------------------------------------
	// Function deducts health from the player when called.
	//--------------------------------------------------------------------------------
	public void Damage()
    {
		// Detects if the health timer is at zero or if the player isn't recovering
        if (m_fHealthTimer <= 0.0f || !m_bRecovering)
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
		//m_CM.RemoveLife();

		//if (m_CM.GetLives() > 0)
		//{
		//	transform.position = m_CM.GetCheckPoint().position;
		//}
		//else
		//{
		//	// Sets the player's position back to Vector3 zero
		//	transform.position = Vector3.zero;
		//}

		transform.position = Vector3.zero;
		
		// Resets health back to full health and updates the UI
        m_nHealth = 2;

		m_halfHealth.enabled = false;
		m_fullHealth.enabled = true;
	}

	public void Bounce(float fBounceForce)
	{
		// Sets the y value of gravity to equal jump speed multipled by bounce force
		m_v3Gravity.y = m_fJumpSpeed * fBounceForce;

		// Resets Jump timer to equal zero
		m_fJumpTimer = 0.0f;

		// Sets jumped bool to be true
		m_bJumped = true;
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
        if (other.tag == "Respawn")
        {
            Death();
        }
    }
}
