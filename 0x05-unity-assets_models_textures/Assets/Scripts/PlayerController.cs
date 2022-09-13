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

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, cam.transform.rotation.y, 0);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * Time.deltaTime * _speed);
        }

        

        if (Input.GetButtonDown("Jump") && _velocity.y > -1 && _velocity.y <= 0)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -3.0f * gravityValue);
        }

        _velocity.y += gravityValue * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
}
