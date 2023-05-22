
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    [Space]
    [Header("Position Check\n--------------")]
    [Space]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float speed = 8f;

    [Space]
    [Header("Player info\n--------------")]
    [Space]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector2 direction;



    private InputAction controls;
    private bool moveRight;
    private bool moveLeft;

    private void Awake()
    {
        controls = new InputAction();
    }
    void Start()
    {
        
    }

    
    void Update()
    {

       transform.position += speed * Time.deltaTime * new Vector3(direction.x, 0, 0);
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

        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            isGrounded = false;

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
