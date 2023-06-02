
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public enum PlayerStatus
{
    Idle,
    Run,
    Sprint,
    WallSlide,
    Jump,
    Fall,
    Crouch,
    CrouchRun
}
public class PlayerControler : MonoBehaviour
{
    #region Player Infos
    public static PlayerControler instance;
    public PlayerStatus playerStatus;


    [Space]
    [Header("Player info\n----------")]
    [Range(1f, 10f)]
    private Rigidbody rb;
    private InputAction controls;

    #endregion

    #region Movement
    [Space]
    [Header("Movement\n----------")]
    [Range(1f, 10f)]
    [SerializeField] private float walkSpeed;
    [Range(1f, 10f)]
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float smoothTime;
    [SerializeField] private LayerMask groundMask;
    [HideInInspector]
    public Vector2 currentMovementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 smoothInputSmoothVelocity;
    public bool isGrounded = true;
    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public Vector2 direction;
    [HideInInspector]
    public bool isRunning = false;
    private Vector3 movementForce;
    #endregion

    #region Jump
    [Space]
    [Header("Jump\n----------")]
    [Range(1f, 10f)]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float holdJumpForce;
    [SerializeField] private float jumpBufferTime;
    [Range(1f, 10f)]
    [SerializeField] private float fallMultiplier;
    [Range(1f, 10f)]
    [SerializeField] private float upMultiplier;
    [SerializeField] private float coyoteTime;
    private float jumpBufferGrounded = 0f;
    private float coyoteTimeGrounded = 0f;
    [HideInInspector] public bool isHolding = false;
    #endregion

    #region Wall Slide
    [Space]
    [Header("Wall Slide\n----------")]
    [SerializeField] private float wallSlidingSpeed;
    [HideInInspector]
    public bool isWallSliding = false;


    #endregion

    #region Crouch
    [HideInInspector]
    public bool isCrouching = false;
    public bool isCrouchRunning = false;
    #endregion

    [HideInInspector]
    public Vector3 respawnPosition;
    [HideInInspector]
    public bool isSprinting = false;


    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        controls = new InputAction();
    }
    private void Start()
    {
        respawnPosition = transform.position;
        moveSpeed = walkSpeed;
    }
    void Update()
    {
        Idle();
        SprintEnum();
        CrouchEnum();

        //flip
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);

            if (isGrounded && !isSprinting)
            {
                playerStatus = PlayerStatus.Run;
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

        }
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            if (isGrounded && !isSprinting)
            {
                playerStatus = PlayerStatus.Run;
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }



        }

        //fallJump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (upMultiplier - 1) * Time.deltaTime;
        }

        //timer for coyote time
        if (isGrounded)
        {
            coyoteTimeGrounded = coyoteTime;
        }
        else
        {
            coyoteTimeGrounded -= Time.deltaTime;
        }

        jumpBufferGrounded -= Time.deltaTime;
        //fall anim
        if (rb.velocity.y < 0 && !isGrounded && !isWallSliding)
        {
            playerStatus = PlayerStatus.Fall;
        }

    }


    private void FixedUpdate()
    {
        AddJumpForce();
        WallSlide();


        //move

        currentMovementInput = Vector2.SmoothDamp(currentMovementInput, direction, ref smoothInputSmoothVelocity, smoothTime);
        rb.velocity = new Vector2(currentMovementInput.x * moveSpeed, rb.velocity.y);


    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    public void Idle()
    {

        if (rb.velocity.x < 0.2f && rb.velocity.x > -0.2f && isGrounded)
        {
            playerStatus = PlayerStatus.Idle;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

    }

    public void Jump(InputAction.CallbackContext context)
    {

        if ((jumpBufferGrounded > 0f) && coyoteTimeGrounded > 0f)
        {
            isHolding = true;
            jumpBufferGrounded = 0;
            coyoteTimeGrounded = 0f;
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            isGrounded = false;

            playerStatus = PlayerStatus.Jump;

        }

        if (context.started)
        {
            jumpBufferGrounded = jumpBufferTime;
        }

        if (context.canceled)
        {
            isHolding = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = Physics.Raycast(transform.position, -transform.up, 2.25f, groundMask);


        }

        if (collision.gameObject.CompareTag("wallSlide") && !isGrounded && rb.velocity.y != 0)
        {
            isWallSliding = true;
            playerStatus = PlayerStatus.WallSlide;

        }
        else
        {
            isWallSliding = false;
        }

        if (collision.gameObject.CompareTag("roof") && rb.velocity.y > 0)
        {
            jumpVelocity -= jumpVelocity;
        }
        else
        {
            jumpVelocity = 5.5f;
        }





    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("wallSlide") && !isGrounded && rb.velocity.y != 0)
        {
            isWallSliding = false;

        }
    }

    private void WallSlide()
    {
        if (isWallSliding && direction.x > 0)
        {

            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));


        }
        if (isWallSliding && direction.x < 0)
        {

            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));


        }
    }

    public void AddJumpForce()
    {
        if (isHolding)
        {
            rb.AddForce(Vector3.up * holdJumpForce, ForceMode.Impulse);
        }
    }


    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            isSprinting = true;
            moveSpeed = sprintSpeed;

        }
        if (context.canceled)
        {
            moveSpeed = walkSpeed;
            isSprinting = false;
        }
    }

    public void SprintEnum()
    {
        if (isSprinting && isGrounded)
        {
            playerStatus = PlayerStatus.Sprint;
        }
    }


    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            isCrouching = true;
            
          
        }
        if (context.canceled)
        {
            isCrouching = false;
           
           
        }

    }

    public void CrouchEnum()
    {
        if (isCrouching && isGrounded && !isRunning)
        {
            playerStatus = PlayerStatus.Crouch;

        }
        if (isCrouchRunning && isRunning && isGrounded)
        {
            playerStatus = PlayerStatus.CrouchRun;

        }



    }
}
