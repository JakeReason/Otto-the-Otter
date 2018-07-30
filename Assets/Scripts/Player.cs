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

    // Private variable used to store the player's CharacterController in
    private CharacterController m_cc;

    // Private Vector3 stores the direction the player should move
    private Vector3 m_v3MoveDirection;

    // Vector3 represents the direction the player will look in
    private Vector3 m_v3LookDirection;

    private Vector3 m_v3PreviousLook;

    // Vector3 allows gravity to be applied in movement formulas
    private Vector3 m_v3Gravity;

    private bool m_bJumped;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Gets the CharacterController component on awake
        m_cc = GetComponent<CharacterController>();

        // Initialises the Move, Look and Gravity Direction all to equal the zero Vector3
        m_v3MoveDirection = Vector3.zero;
        m_v3LookDirection = Vector3.zero;
        m_v3Gravity = Vector3.zero;

        m_bJumped = false;
    }

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // Creates a new Vector3 indicating which direction the left stick is facing
        m_v3MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 
                                        Input.GetAxis("Vertical"));

        m_v3LookDirection = new Vector3(m_v3MoveDirection.x, 0, m_v3MoveDirection.z);

        // Caps the x value of move direction to 0 if the value is between 0.4f and -0.4f
        if (m_v3MoveDirection.x < 0.4f && m_v3MoveDirection.x > -0.4f)
        {
            m_v3MoveDirection.x = 0.0f;
        }

        // Caps the z value of move direction to 0 if the value is between 0.4f and -0.4f
        if (m_v3MoveDirection.z < 0.4f && m_v3MoveDirection.z > -0.4f)
        {
            m_v3MoveDirection.z = 0.0f;
        }

        if (m_v3LookDirection.x < 0.1f && m_v3LookDirection.z < 0.1f)
        {
            m_v3LookDirection = m_v3PreviousLook;
        }
        else
        {
            m_v3PreviousLook = m_v3LookDirection;
        }

        if (Input.GetButton("Jump") && m_cc.isGrounded && !m_bJumped)
        {
            m_v3Gravity.y = m_fJumpSpeed;
            m_bJumped = true;
        }
        else if (!Input.GetButton("Jump") && m_cc.isGrounded)
        {
            m_bJumped = false;
        }
        else if (!Input.GetButton("Jump") && !m_cc.isGrounded)
        {
            m_v3Gravity += Physics.gravity * Time.deltaTime;
        }
        else
        {
            m_v3Gravity = Vector3.zero;
        }

        float itsy = m_v3MoveDirection.y;
        m_v3MoveDirection = Camera.main.transform.rotation * m_v3MoveDirection;
        transform.eulerAngles = new Vector3(0, transform.rotation.y, 0);
        m_v3MoveDirection.y = itsy;

        m_v3MoveDirection *= m_fSpeed;

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

    public void Damage()
    {
        Debug.Log("Ouch");
    }
}
