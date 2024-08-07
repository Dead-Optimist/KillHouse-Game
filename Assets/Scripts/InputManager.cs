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
        // assign var and call some method
        playerInput = new PlayerInput();
        onGround = playerInput.OnGround;
        move = GetComponent<PlayerMove>();
        look = GetComponent<PlayerLook>();
        onGround.Dashing.performed += ctx => move.OnDash();
        onGround.Crouching.performed += ctx => move.Crouch();
        onGround.Running.performed += ctx => move.Run();
    }
    // call the processing method with input vector from the new input system
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
        onGround.Disable();
    }
}
