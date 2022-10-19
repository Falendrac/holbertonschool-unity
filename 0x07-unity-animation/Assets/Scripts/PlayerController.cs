using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the control for the player
/// Like ASDW and Jump
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// The force of the jump when the player use space bar
    private float _jumpForce = 2.0f;
    /// Speed of the player
    private float _speed = 10f;
    /// The default gravity to apply in the jump
    private float gravityValue = -9.81f;
    /// Vector to apply the force of the jump to the player
    private Vector3 _velocity;
    /// The controller of playe to apply movement and jump
    private CharacterController controller;
    /// The rigidbody for the mass
    private Rigidbody _rb;
    /// To turn smooth in time
    public float turnSmoothTime = 0.1f;
    /// The smooth velocity
    float turnSmoothVelocity;
    /// To get the rotation of camera
    public Transform cam;
    private Animator anim;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        anim = transform.Find("ty").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        handleMove();

        handleJumping();

        controller.transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * 100 * Time.deltaTime);

        if (transform.position.y < -30)
        {
            transform.position = new Vector3(0, 40, 0);
        }

        transform.Find("ty").localRotation = transform.rotation;
    }

    // Handle the AWSD movement
    void handleMove()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir.y = -0.1f;
            controller.Move(moveDir * Time.deltaTime * _speed);
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    // Handle the jump when the player hit the space bar
    void handleJumping()
    {
        if (controller.isGrounded && _velocity.y < -1f)
        {
            _velocity.y = -1f;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -3.0f * gravityValue);
        }

        _velocity.y += gravityValue * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }


}
