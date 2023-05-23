
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
    [SerializeField] private bool isFacingRight = true;
    [Range(1f, 10f)]
    [SerializeField] private float speed;
    private Rigidbody rb;
    private InputAction controls;

    [Space]
    [Header("Movement\n-----------")]
    private Vector2 direction;
    private Vector3 movementForce;

    [Space]
    [Header("Jump\n-----------")]
    [Range(1f, 10f)]
    [SerializeField] private float jumpVelocity;
    private float fallMultiplier = 2.2f;
    private float lowJumpMultiplier = 1.8f;
    private bool isHolding = false;

    [HideInInspector]
    public Vector3 respawnPosition;
    


    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        controls = new InputAction();
    }
   
    void Update()
    {
        
        transform.position += speed * Time.deltaTime * new Vector3(direction.x, 0, 0);

        //flip
        

    

       

        //fallJump
        if (rb.velocity.y < 0)
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
        
       

    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (context.started && isGrounded)
        {
            isHolding = true;
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            isGrounded = false;

        }

        if (context.canceled && !context.started)
        {
            isHolding = false;
        }
        if (context.performed && !context.started)
        {
            isHolding = false;
        }




    }

    public void AddJumpForce()
    {
        if (isHolding)
        {
            rb.AddForce(new Vector3(speed, 0,0), ForceMode.Impulse);
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
