using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private const string RunningAnimationParameterName = "isRunning";
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const string JumpButtonName = "Jump";

    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private GameObject spawnPosition;

    [Header("Parameters")] 
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float speed = 3f;

    private float groundDistance = 0.2f;
    private Vector3 velocity;
    private bool isGrounded;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;
    private readonly int runningAnimationId = Animator.StringToHash(RunningAnimationParameterName);



    private void Start()
    {
        Respawn();
    }

    private void Update()
    {
        MovePlayer();
        HandleGravity();
    }

    private void HandleGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void MovePlayer()
    {
        if(!IsOwner) return;
        
        float x = Input.GetAxis(HorizontalAxisName);
        float z = Input.GetAxis(VerticalAxisName);

        Vector3 move = new Vector3(x, 0, z).normalized;
        if (move.magnitude >= 0.1f)
        {
            characterAnimator.SetBool(runningAnimationId, true);
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(characterTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            characterTransform.rotation = Quaternion.Euler(0f, angle, 0f);
            characterController.Move(move * (speed * Time.deltaTime));
        }
        else
        {
            characterAnimator.SetBool(runningAnimationId, false);
        }
        
        if (isGrounded && Input.GetButtonDown(JumpButtonName))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void Respawn()
    {
        characterController.enabled = false;
        transform.position = Vector3.zero;
        characterController.enabled = true;
    }
}