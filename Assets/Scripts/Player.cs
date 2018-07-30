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

    public float m_fJumpSpeed = 8.0f;

    // Private variable used to store the player's CharacterController in
    private CharacterController m_cc;

    // Private Vector3 stores the direction the player should move
    private Vector3 m_v3MoveDirection;

    private Vector3 m_v3LookDirection;

    private Vector3 m_v3Gravity;

    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Gets the CharacterController component on awake
        m_cc = GetComponent<CharacterController>();

        // Initialises the Move Direction to equal the zero Vector3
        m_v3MoveDirection = Vector3.zero;

        m_v3LookDirection = Vector3.zero;

        m_v3Gravity = Vector3.zero;
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

        if (Input.GetButtonDown("Jump") && m_cc.isGrounded)
        {
            m_v3Gravity.y = m_fJumpSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            m_v3Gravity += Physics.gravity * 10.0f * Time.deltaTime;
        }
        else if (!m_cc.isGrounded)
        {
            m_v3Gravity += Physics.gravity * Time.deltaTime;
        }
        else
        {
            m_v3Gravity = Vector3.zero;
        }

        // Applies the speed float to the move direction
        m_v3MoveDirection *= m_fSpeed;

        m_v3MoveDirection += m_v3Gravity;

        // Adds movement to CharacterController based on move direction and delta time
        m_cc.Move(m_v3MoveDirection * Time.deltaTime);

        if (m_v3LookDirection.x != 0 || m_v3LookDirection.z != 0)
        {
            transform.rotation = Quaternion.LookRotation(m_v3LookDirection);
        }
	}

    public void Damage()
    {
        Debug.Log("Ouch");
    }
}
