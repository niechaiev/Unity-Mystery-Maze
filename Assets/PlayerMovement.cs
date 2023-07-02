using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [Header("Parameters")]
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float speed = 3f;

    private float groundDistance = 0.2f;
    private Vector3 velocity;
    private bool isGrounded;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = new Vector3(x, 0, z).normalized;
        if (move.magnitude >= 0.1f)
        {
            characterAnimator.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(characterTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            characterTransform.rotation = Quaternion.Euler(0f, angle, 0f);
            characterController.Move(move * (speed * Time.deltaTime));
        }
        else
        {
            characterAnimator.SetBool("isRunning", false);
        }


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}