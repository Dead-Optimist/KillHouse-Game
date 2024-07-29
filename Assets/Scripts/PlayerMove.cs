using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{ //create var
    private CharacterController controller;
    private Vector3 playerFall;
    private bool isGrounded;
    private float gravity = -50f;
    private float speed = 4.0f;
    [Header("DashSetting")]
    [SerializeField] private float dashspeed = 15f;
    [SerializeField] private float dashDuration = 1f;
    private Vector3 velocity;
    private bool lerpCrounch;
    private bool crouching;
    private bool running;
    private float crouchTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrounch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            if (crouching) 
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);
            if (p > 1)
            {
                lerpCrounch = false;
                crouchTimer = 0f;
            }
        }
        



    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero; // create a blank vector 3
        moveDirection.x = input.x;   //assign value from input
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        // Spliting x,z and y because the falling speed need to be independent of the movement speed
        playerFall.y += gravity * Time.deltaTime; //player velocity when falling
        if (isGrounded && playerFall.y < 0)
            playerFall.y = -2f;  //reset player gravity when on ground
        controller.Move(playerFall * Time.deltaTime); //make player fall based on velocity
    }
    public IEnumerator Dash() // this used to be jumping but i deemed it uncessesary for this game
    {
        if (isGrounded)
        {
            velocity = new Vector3(transform.forward.x * dashspeed, 0f, transform.forward.z * dashspeed);
            yield return new WaitForSeconds(dashDuration);
            velocity = Vector3.zero;
        }
    }
    public void Crouch()
    {
        if (running)
        {
            // If running, disable running before crouching
            running = false;
            speed = 4; // Set to default speed when not running
        }
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrounch = true;
    }
    public void Run()
    {
        if (crouching)
        {
            // If crouching, disable crouching before running
            crouching = false;
        }
        running = !running;
        speed = running ? 8f : 4f;  //if running = true, speed =8 and =4 otherwise
    }
}