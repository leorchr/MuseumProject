
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControler : MonoBehaviour
{
    #region Player Infos
    public static PlayerControler instance;
  
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
    [HideInInspector]
    public Vector2 currentMovementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 smoothInputSmoothVelocity;
    [HideInInspector]
    public bool isGrounded = true;
    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public Vector2 direction;
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
    [Space]
    [Header("Crouch\n----------")]
    #endregion

    [HideInInspector]
    public Vector3 respawnPosition;
    
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        controls = new InputAction();
    }
    private void Start()
    {
        moveSpeed = walkSpeed;
       
    }
    void Update()
    {
       
        //flip
        if (direction.x < 0)
            {
                transform.rotation = new Quaternion(0,180,0,0);
            }
            if (direction.x > 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
       
        //fallJump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        if(rb.velocity.y > 0)
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



    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        if (context.performed && isGrounded)
        {
            //play anim
        }
        if(context.canceled)
        {
            //end anim
        }
       
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
            isGrounded = true;
           
        }

        if (collision.gameObject.CompareTag("wallSlide") && !isGrounded && rb.velocity.y != 0)
        {
            isWallSliding = true;
       
        }
        else
        {
            isWallSliding = false;
        }

        //if (collision.gameObject.CompareTag("roof") && rb.velocity.y > 0)
        //{
           //jumpVelocity -= jumpVelocity;
        //}
        //else
        //{
            //jumpVelocity = 5.5f;
        //}
        
        
       

        
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
            //lancer anime de droite 
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            
            
        }
        if(isWallSliding && direction.x < 0)
        {
            //lancer anim de gauche
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            
        }
        else if(!isWallSliding)
        {
            Debug.Log("detaché");
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
            //PlayerAnimation.instance.IsSprinting();
            moveSpeed = sprintSpeed;
        }
        if (context.canceled)
        {
            
            moveSpeed = walkSpeed;
            //PlayerAnimation.instance.isRunning();


        }
    }

   

    public void Crouch(InputAction.CallbackContext context)
    {




    }

 

}
