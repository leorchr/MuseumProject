
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler instance;
    [Space]
    [Header("Position Check\n--------------")]
    [Space]
    [SerializeField] private bool isGrounded;
    

    [Space]
    [Header("Player info\n--------------")]
    [Space]
    [SerializeField] private bool isFacingRight = true;
    [Range(1f, 10f)]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float speed = 8f;


    private Vector3 movementForce;
    private float fallMultiplier = 2.2f;
    private float lowJumpMultiplier = 2f;
    private Vector2 direction;
    private Rigidbody rb;
    private InputAction controls;
  
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        controls = new InputAction();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        //float horizontal = Input.GetAxis("horizontal");
        //float vertical = Input.GetAxis("vertical");

        //movementForce = new Vector3(horizontal, 0, 0);
        
        
        transform.position += speed * Time.deltaTime * new Vector3(direction.x, 0, 0);

        //betterJump
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        
    }
    private void FixedUpdate()
    {
        

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
        //rb.AddForce(movementForce * speed);
       
    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (context.performed && isGrounded)
        {
            
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            isGrounded = false;

        }
        

    }

    public void Flip()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


}
