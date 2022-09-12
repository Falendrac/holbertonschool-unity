using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _jumpForce = 2.0f;
    private float _speed = 10f;
    private float gravityValue = -9.81f;
    private Vector3 _velocity;
    private CharacterController controller;
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
            Debug.Log("Bite");
            _velocity.y = Mathf.Sqrt(_jumpForce * -3.0f * gravityValue);
        }

        _velocity.y += gravityValue * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
}
