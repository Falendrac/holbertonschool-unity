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
    
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * _speed);

        if (Input.GetButtonDown("Jump"))
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -3.0f * gravityValue);
        }

        _velocity.y += gravityValue * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
}
