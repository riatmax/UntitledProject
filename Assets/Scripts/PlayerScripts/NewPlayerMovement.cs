using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public float WalkSpeed = 5f;
    public float JumpForce = 5;
    public float GroundCheckDistance;
    public float lookSensX = 1f;
    public float lookSensY = 1f;
    public Vector3 velocity;
    CharacterController cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }
    private void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.forward * verticalMove + transform.right * horizontalMove;
        moveDirection.Normalize();

        cc.Move(moveDirection * WalkSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            velocity.y = JumpForce;
        }
        else
        {
            velocity.y = gravity
        }
    }

    bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, GroundCheckDistance))
        {
            return true;
        }
        return false;
    }
}
