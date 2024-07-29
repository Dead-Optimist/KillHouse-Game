using UnityEngine;

public class InputManager : MonoBehaviour
{
    // create var
    private PlayerInput playerInput;
    private PlayerInput.OnGroundActions onGround;
    private PlayerMove move;
    private PlayerLook look;

    void Awake()
    {
        // assign var
        playerInput = new PlayerInput();
        onGround = playerInput.OnGround;
        move = GetComponent<PlayerMove>();
        onGround.Dashing.performed += ctx => move.Dash();
        look = GetComponent<PlayerLook>();
        onGround.Crouching.performed += ctx => move.Crouch();
        onGround.Running.performed += ctx => move.Run();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        move.ProcessMove(onGround.Walking.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(onGround.Looking.ReadValue<Vector2>());
    }

    //Enable and Disable
    private void OnEnable()
    {
        onGround.Enable();
    }
    private void OnDisable()
    {
        onGround.Running.performed -= ctx => move.Run();
        onGround.Crouching.performed -= ctx => move.Crouch();
        onGround.Disable();
    }
}
