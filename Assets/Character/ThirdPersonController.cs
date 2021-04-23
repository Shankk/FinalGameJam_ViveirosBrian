using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    public AudioSource JumpSound = new AudioSource();
    public AudioClip JumpClip;
    Animator animator;
    CharacterControls action;
    CharacterController controller;
    public Rigidbody rb;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Vector3 velocity;
    public bool IsGrounded;

    public float Speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        action = new CharacterControls();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        JumpSound.clip = JumpClip;
        controller = GetComponent<CharacterController>();
        action.Player.Jump.performed += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("IsGrounded", IsGrounded);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Move();
        
    }

    private void Move()
    {
        Vector2 inputVector = action.Player.WASD.ReadValue<Vector2>();
        //Debug.Log(inputVector);
        Vector3 direction = new Vector3(inputVector.x, 0f, 1f).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * (Speed + (inputVector.y > 0 ? Speed/3 : 0) + (inputVector.y < 0 ? -Speed/3 : 0)) * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if(IsGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            JumpSound.Play();
        }
    }

}
