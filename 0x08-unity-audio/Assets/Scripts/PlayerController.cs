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
    private bool isControl;
    Vector3 move;
    public GameObject model;
    private AudioSource runSound;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        anim = transform.Find("ty").GetComponent<Animator>();
        isControl = true;
        runSound = GetComponent<AudioSource>();
    }

    // FixedUpdate is called according to framerate
    void FixedUpdate()
    {
        handleMove();
    }

    // Update is called once per frame
    void Update()
    {
        handleJumping();
        handleFall();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Happy Idle"))
        {
            anim.SetBool("isImpact", false);
            isControl = true;
        }
    }

    // Handle the AWSD movement
    void handleMove()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        if (move.magnitude >= 0.1f && isControl)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir.y = -0.1f;
            controller.Move(moveDir * Time.deltaTime * _speed);
            handleRotation(move);
            anim.SetBool("isRunning", true);
            if (controller.isGrounded)
            {
                if (!runSound.isPlaying)
                {
                    runSound.Play();
                }
            }
            else
            {
                runSound.Stop();
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            runSound.Stop();
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
            anim.SetBool("isJump", true);
        }
        else
        {
            anim.SetBool("isJump", false);
        }

        _velocity.y += gravityValue * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    // Handle the conditions to start the animation of falling
    void handleFall()
    {
        if (!controller.isGrounded && transform.position.y < -30)
        {
            transform.position = new Vector3(0, 40, 0);
            anim.SetBool("isFall", true);
            isControl = false;
        }
        else if (controller.isGrounded && anim.GetBool("isFall"))
        {
            anim.SetBool("isFall", false);

            anim.SetBool("isImpact", true);
        }
    }

    // Handle the rotation of the character
    void handleRotation(Vector3 move)
    {
        if (move != Vector3.zero)
        {
            model.transform.rotation = Quaternion.LookRotation(Quaternion.Euler(0.0f, cam.transform.localEulerAngles.y, 0.0f) * move);
        }

        if (move.x != 0 || move.z != 0)
        {
            Vector3 v = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(v.x, cam.transform.rotation.eulerAngles.y, v.z);
        }
    }
}
