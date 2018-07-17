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

    // Float utilised to add a jump force to the player
    public float m_fJumpSpeed = 8.0f;

    // Private variable used to store the player's CharacterController in
    private CharacterController m_cc;

    // Private Vector3 stores the direction the player should move
    private Vector3 m_v3MoveDirection;
    
    //--------------------------------------------------------------------------------
    // Function is called when script first runs.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Gets the CharacterController component on awake
        m_cc = GetComponent<CharacterController>();

        // Initialises the Move Direction to equal the zero Vector3
        m_v3MoveDirection = Vector3.zero;
	}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
    {
        // Creates a new Vector3 indicating which direction the left stick is facing
        m_v3MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 
                                        -Input.GetAxis("Vertical"));

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

        if (XCI.GetButtonDown(XboxButton.A, m_controller) && m_cc.isGrounded)
        {
            m_v3MoveDirection.y += m_fJumpSpeed;
        }

        // Adds movement to CharacterController based on move direction, speed and time
        m_cc.Move(m_v3MoveDirection * m_fSpeed * Time.deltaTime);

        // Applies gravity to the CharacterController
        m_cc.SimpleMove(Physics.gravity);
	}
}
