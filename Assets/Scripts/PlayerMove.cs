using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
//move
public class PlayerMove : MonoBehaviour
{ //create var
    private CharacterController controller;
    private Vector3 playerFall;
    private Vector3 moveDirection;
    private bool isGrounded;
    private float gravity = -50f;
    private float speed = 4.0f;
    [Header("DashSetting")]
    [SerializeField] private float dashspeed = 12f;
    [SerializeField] private float dashDuration = 0.25f;
    private Vector3 velocity;
    private bool lerpCrounch;
    private bool crouching;
    private bool running;
    private float crouchTimer;
    private bool isDashing = false;


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
        if (isDashing == false)     //stop reading moving input when dashing
        {
            moveDirection = Vector3.zero; // create a blank vector 3
            moveDirection.x = input.x;   //assign value from input
            moveDirection.z = input.y;
        }
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        // Spliting x,z and y because the falling speed need to be independent of the movement speed
        playerFall.y += gravity * Time.deltaTime; //player velocity when falling
        if (isGrounded && playerFall.y < 0)
            playerFall.y = -2f;  //reset player gravity when on ground
        controller.Move(playerFall * Time.deltaTime); //make player fall based on velocity
    }
    public void OnDash()
    {
        if (isGrounded)
        {
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash() // this used to be jumping but i deemed it uncessesary for this game
    {
        isDashing = true;
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            controller.Move(transform.TransformDirection(moveDirection) * dashspeed * Time.deltaTime);

            yield return null;
        }
        isDashing = false;
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
        speed = running ? 6f : 4f;  //if running = true, speed =8 and =4 otherwise
    }
}