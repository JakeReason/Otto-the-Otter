//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using UnityEngine;
using XboxCtrlrInput;

// Creates a class for the Player script requiring a CharacterController
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Allows access to xbox controller buttons
    public XboxController m_controller;

    // Public float represents the speed of the player's movement
    public float m_fSpeed = 10.0f;

    // Float indicates the speed of the player's jump
    public float m_fJumpSpeed = 8.0f;

    // Public float adds more gravity when player is falling (allows floats from 1-10)
    [Range(1.0f, 10.0f)]
    public float m_fExtraGravity = 4.0f;

    // Indicates maximum time for player to jump before falling (range from 0.1-1)
    [Range(0.1f, 1.0f)]
    public float m_fJumpTimeLimit = 0.4f;

    // Represents how far on the x a player can move while jumping (range from 0.1-1)
    [Range(0.1f, 1.0f)]
    public float m_fJumpMoveLimit = 0.5f;

    // Private variable used to store the player's CharacterController in
    private CharacterController m_cc;

    // Allows access to the BasicEnemy script
    private BasicEnemy m_be;

    // Variable is used to store the player's Grappling Hook script in
    private GrapplingHook m_gh;

    private CameraFollow2 m_cf;

    public GameObject m_cameraObj;

    // Private Vector3 stores the direction the player should move
    private Vector3 m_v3MoveDirection;

    // Vector3 represents the direction the player will look in
    private Vector3 m_v3LookDirection;

    // Private Vector3 indicates the previous frame looking direction direction
    private Vector3 m_v3PreviousLook;

    // Vector3 allows gravity to be applied in movement formulas
    private Vector3 m_v3Gravity;

    // Jump Timer represents how long the player has been jumping for
    private float m_fJumpTimer;

    // Private float indicates the movement on the x axis while jumping
    private float m_fMovementX;

    // Bool used to let the script know if the player has jumped from ground
    private bool m_bJumped;

    // Private bool indicates if the player is currently jumping
    private bool m_bJumping;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Gets the CharacterController component on awake
        m_cc = GetComponent<CharacterController>();

        // When script is called, the BasicEnemy script is obtained and stored
        m_be = GetComponent<BasicEnemy>();

        // Gets the GrapplingHook script and stores it in the variable
        m_gh = GetComponent<GrapplingHook>();

        m_cf = m_cameraObj.GetComponent<CameraFollow2>();

        // Initialises the Move, Look and Gravity Direction all to equal the zero Vector3
        m_v3MoveDirection = Vector3.zero;
        m_v3LookDirection = Vector3.zero;
        m_v3Gravity = Vector3.zero;

        // Initialises private floats to equal zero
        m_fJumpTimer = 0.0f;
        m_fMovementX = 0.0f;

        // Sets private bools to false on awake
        m_bJumped = false;
        m_bJumping = false;
    }

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
        if (!m_gh.GetFired())
        {
            // Creates a new Vector3 indicating which direction the left stick is facing
            m_v3MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0,
                                            Input.GetAxis("Vertical"));
        }

        // Stores the x value of Move Direction into the float for use when jumping
        m_fMovementX = m_v3MoveDirection.x;

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
        if (m_gh.m_bHooked)
        {
            // Sets gravity to equal the zero Vector3
            m_v3Gravity = Vector3.zero;
        }
        // Else if the grappling hook hasn't hooked an object
        else
        {
            // Checks if Jump is pressed, the player is grounded and if they haven't jumped
            if (Input.GetButton("Jump") && m_cc.isGrounded && !m_bJumped)
            {
                // Sets the y value of gravity to equal the jump speed
                m_v3Gravity.y = m_fJumpSpeed;

                // Sets jumped and jumping bools to be true
                m_bJumped = true;
                m_bJumping = true;
            }
            // Else if Jump button isn't pressed and the player is grounded
            else if (!Input.GetButton("Jump") && m_cc.isGrounded)
            {
                // Sets jumped and jumping bools back to false
                m_bJumped = false;
                m_bJumping = false;
                m_fJumpTimer = 0.0f;
            }
            // Else if Jump isn't pressed and player is in air or player has jumped too long
            else if ((!Input.GetButton("Jump") && !m_cc.isGrounded) ||
                      m_fJumpTimer > m_fJumpTimeLimit)
            {
                // Applies gravity to player with an extra multiplier to fall quicker
                m_v3Gravity += Physics.gravity * m_fExtraGravity * Time.deltaTime;
            }
            // Else if the player is falling
            else if (!m_cc.isGrounded)
            {
                // Apply gravity to the player without miltiplier
                m_v3Gravity += Physics.gravity * Time.deltaTime;
            }
            // Else if the player is grounded without pressing jump
            else
            {
                // Sets the gravity to zero
                m_v3Gravity = Vector3.zero;
            }
        }

        // Checks if the player is in the air
        if (m_bJumping)
        {
            // Adds the jump timer to deltaTime every second
            m_fJumpTimer += Time.deltaTime;

            // Restricts the movement on the x axis while jumping
            m_v3MoveDirection.x = m_fMovementX * m_fJumpMoveLimit;
        }

        // Stores the y movement direction in local float
        float fCurrentMoveY = m_v3MoveDirection.y;

        m_v3MoveDirection = Camera.main.transform.rotation * m_v3MoveDirection;

        // Stores local float value as the y value for the Move Direction
        m_v3MoveDirection.y = fCurrentMoveY;

        // Multiples Move Direction vector by speed
        m_v3MoveDirection *= m_fSpeed;

        // Applies the gravity to the move direction
        m_v3MoveDirection += m_v3Gravity;

        // Adds movement to CharacterController based on move direction and delta time
        m_cc.Move(m_v3MoveDirection * Time.deltaTime);

        if (m_v3MoveDirection.sqrMagnitude > 0.1f)
        {
            m_v3LookDirection = transform.position + m_v3MoveDirection.normalized;
            m_v3LookDirection.y = transform.position.y;
            transform.LookAt(m_v3LookDirection, Vector3.up);
        }
	}

    //--------------------------------------------------------------------------------
    // Function deducts health from the player when called.
    //--------------------------------------------------------------------------------
    public void Damage()
    {
        Debug.Log("Ouch.");
    }

    //--------------------------------------------------------------------------------
    // Function sends player to the start of the level when health is zero.
    //--------------------------------------------------------------------------------
    private void Death()
    {
        Debug.Log("Game Over.");
    }

    //--------------------------------------------------------------------------------
    // Function is called when the player is enters a trigger.
    //
    // Param:
    //      other: Represents the collider of the trigger.
    //--------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
        {
            transform.position = Vector3.zero;
        }
    }
}
