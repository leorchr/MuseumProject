
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler instance;
    [Space]
    [Header("Position Check\n--------------")]
    [SerializeField] private bool isGrounded;
    
    [Space]
    [Header("Player info\n--------------")]
    [Range(1f, 10f)]
    private Rigidbody rb;
    private InputAction controls;

    [Space]
    [Header("Movement\n-----------")]
    [Range(1f, 10f)]
    [SerializeField] private float walkSpeed;
    [Range(1f, 10f)]
    [SerializeField] private float sprintSpeed;
    [Range(1f, 10f)]
    [SerializeField] private float moveSpeed;
    private Vector2 direction;
    private Vector3 movementForce;
  



    [Space]
    [Header("Jump\n-----------")]
    [Range(1f, 10f)]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float holdJumpForce;
    private float fallMultiplier = 2.2f;
    private float upMultiplier = 1.9f;
    private bool isHolding = false;
    

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
        
        //Move

        transform.position += moveSpeed * Time.deltaTime * new Vector3(direction.x, 0, 0);

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
        //slow when jumping oppo direction
        //if(direction.x < 0 && rb.velocity.y < 0)
        //{
            //moveSpeed = moveSpeed / 2 * Time.deltaTime;
        //}


        


    }
    private void FixedUpdate()
    {
        AddJumpForce();


    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }



    public void Interact(InputAction.CallbackContext context)
    {

    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        Debug.Log(direction);
    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (context.started && isGrounded)
        {
            isHolding = true;
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            isGrounded = false;

        }

        if (context.canceled)
        {
            isHolding = false;
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
            
            moveSpeed = sprintSpeed;
        }
        if (context.canceled)
        {
            
            moveSpeed = walkSpeed;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


}
