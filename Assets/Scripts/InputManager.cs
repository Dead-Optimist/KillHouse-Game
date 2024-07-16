using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnGroundActions onGround;
    private PlayerMotor motor;
    private PlayerLook look;

    void Awake()
    {
        playerInput = new PlayerInput();
        onGround = playerInput.OnGround;
        motor = GetComponent<PlayerMotor>();
        onGround.Jumping.performed += ctx => motor.Jump();
        look = GetComponent<PlayerLook>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onGround.Walking.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(onGround.Looking.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onGround.Enable();
    }
    private void OnDisable()
    {
        onGround.Disable();
    }
}
