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
    public Transform cameraTransform;
    public float gravity = -9.8f;
    public float sprintMultiplier = 2f;
    public float verticalRotation;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.forward * verticalMove + transform.right * horizontalMove;
        moveDirection.Normalize();

        float speed = WalkSpeed;
        if (Input.GetAxis("Sprint") > 0)
        {
            speed *= sprintMultiplier;
        }
        cc.Move(moveDirection * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            velocity.y = JumpForce;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        cc.Move(velocity * Time.deltaTime);

        if (cameraTransform != null)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * lookSensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * lookSensY;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
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
