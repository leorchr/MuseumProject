
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
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
public class PlayerController : MonoBehaviour
{
    #region Player Infos
    public static PlayerController instance;
    public PlayerStatus playerStatus;


    [Space]
    [Header("Player info\n----------")]
    [Range(1f, 10f)]
    [HideInInspector]
    public Rigidbody rb;
    private InputAction controls;
    [SerializeField] private Transform raycastPos;
    [SerializeField] private float distance = 1f;

    #endregion

    #region Movement
    [Space]
    [Header("Movement\n----------")]
    [Range(1f, 10f)]
    [SerializeField] public float walkSpeed;
    [Range(1f, 10f)]
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float crouchSpeed;
    [SerializeField] private float smoothTime;
    [SerializeField] private LayerMask groundMask;
    [HideInInspector]
    public Vector2 currentMovementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 smoothInputSmoothVelocity;
    public bool isGrounded = true;
    //[HideInInspector]
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
    [HideInInspector] public bool isJumping = false;
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
    [HideInInspector]
    public bool isCrouchRunning = false;
    public bool headTouch = false;
    [SerializeField] private Transform headRaycast;
   
    #endregion

    [HideInInspector]
    public Vector3 respawnPosition;
    [HideInInspector]
    public bool isSprinting = false;

    #region Debug

    [SerializeField] private bool visualDebugging;
    [SerializeField] private Color color = new Color(0f, 0f, 1f, 1f);


    #endregion

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        controls = new InputAction();
    }
    private void Start()
    {
        respawnPosition = transform.position;
        moveSpeed = 0;
    }
    void Update()
    {

        CheckState();
        headTouch = Physics.OverlapSphere(headRaycast.position, distance, groundMask).Length > 0;

        int dirSign = direction.x < 0 ? -1 : 1;
        //flip
        if (direction.x != 0)
        {
            transform.rotation = Quaternion.Euler(0, dirSign * 90, 0);

            if (isGrounded && !isSprinting)
            {

                if (playerStatus == PlayerStatus.Crouch)
                {
                    playerStatus = PlayerStatus.CrouchRun;
                }
                else
                {
                    playerStatus = PlayerStatus.Run;
                }
            }
            if(isSprinting && isGrounded)
            {
                playerStatus = PlayerStatus.Sprint;
            }

        }


        //fallJump
        if (rb.velocity.y < 0) rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;




        if (rb.velocity.y > 0) rb.velocity += Vector3.up * Physics.gravity.y * (upMultiplier - 1) * Time.deltaTime;




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

    }


    private void FixedUpdate()
    {
        AddJumpForce();
        
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

    private void CheckState()
    {
        if (isWallSliding)
        {
            WallSlide();
        }
        else if (rb.velocity.y < 0 && !isGrounded && !isWallSliding && isJumping == false && !isHolding)
        {
            Fall();
            
        }
        else if (isCrouching)
            CrouchEnum();
        else if (isCrouchRunning)
            CrouchRunEnum();
        else if (rb.velocity.x < 0.2f && rb.velocity.x > -0.2f && isGrounded && !isRunning)
        {
            Idle();
        }
    }

    public void Idle()
    {

        playerStatus = PlayerStatus.Idle;
    }

    public void Fall()
    {
        playerStatus = PlayerStatus.Fall;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveSpeed = walkSpeed;
            if (isSprinting)
            {
                moveSpeed = sprintSpeed;
                playerStatus = PlayerStatus.Sprint;
            }
        }
            
        
        direction = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {

        if ((jumpBufferGrounded > 0f) && coyoteTimeGrounded > 0f && !isCrouching)
        {
            isHolding = true;
            isJumping = true;
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
            isJumping = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = Physics.OverlapSphere(raycastPos.position,distance, groundMask).Length>0;
           
        }

        if (collision.gameObject.CompareTag("wallSlide") && !isGrounded && rb.velocity.y != 0)
        {
            isWallSliding = true;


        }
        else
        {
            isWallSliding = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            isGrounded = Physics.OverlapSphere(raycastPos.position, distance, groundMask).Length > 0;
           


        }
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
            playerStatus = PlayerStatus.WallSlide;


        }
        if (isWallSliding && direction.x < 0)
        {

            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            playerStatus = PlayerStatus.WallSlide;


        }
    }

    public void AddJumpForce()
    {
        if (isHolding) rb.AddForce(Vector3.up * holdJumpForce, ForceMode.Impulse);
        
    }


    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded && !isCrouching && !isCrouchRunning)
        {
            isSprinting = true;
            moveSpeed = sprintSpeed;
            SprintEnum();

        }
        if (context.canceled)
        {
            moveSpeed = walkSpeed;
            isSprinting = false;
        }
       
    }

    public void SprintEnum()
    {
        if (isSprinting) playerStatus = PlayerStatus.Sprint;
       
    }


    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded && !isSprinting)
        {
            isCrouching = true;
            


            if (isCrouching && playerStatus == PlayerStatus.Run)
            {
                isCrouchRunning = true;
                
            }

        }

        if(headTouch == false)
        {
            if (context.canceled)
            {
                isCrouching = false;
                isCrouchRunning = false;
                moveSpeed = walkSpeed;

            }
        }

       

    }

    public void CrouchEnum()
    {
        if (isCrouching)
        {
            playerStatus = PlayerStatus.Crouch;
            moveSpeed = crouchSpeed;
        }
           

    }

    public void CrouchRunEnum()
    {
        if (isCrouchRunning)
        {
            playerStatus = PlayerStatus.CrouchRun;
            moveSpeed = crouchSpeed;

        }
        
    }


#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        if (!visualDebugging)
            return;
        Gizmos.color = color;
        if (visualDebugging)
        {
            Gizmos.DrawWireSphere(raycastPos.position, distance);
            Gizmos.DrawWireSphere(headRaycast.position, distance);
        }
    }

#endif

}