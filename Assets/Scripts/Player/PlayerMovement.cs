
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public PlayerState playerState;
    
    public Transform groundCheck;
    public LayerMask groundMask;
    public new Transform camera;
    
    private bool isInputEnabled = true;
    private bool isGrounded;
    private CharacterController characterController;
    private Animator animator;
    private Vector3 moveDirection;
    private Vector3 cameraRotation,playerRotation;
    private Vector3 velocity;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.OnMoveInput+=HandleMoveInput;
        EventManager.OnJumpInput+=HandleJumpInput;
        EventManager.OnRotateInput+=HandleRotationInput;
        EventManager.OnEnablePlayerInput += EnableInput;
        EventManager.OnDisablePlayerInput += DisableInput;
    }

    private void OnDisable()
    {
        EventManager.OnMoveInput-=HandleMoveInput;
        EventManager.OnJumpInput-=HandleJumpInput;
        EventManager.OnRotateInput-=HandleRotationInput;
        EventManager.OnEnablePlayerInput -= EnableInput;
        EventManager.OnDisablePlayerInput -= DisableInput;
    }

    private void HandleMoveInput(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    private void HandleJumpInput()
    {
        if (isGrounded && playerState.canJump)
        {
            velocity.y = Mathf.Sqrt(playerState.jumpHeight * -2f * playerState.gravity);
        }
    }

    private void HandleRotationInput(Vector3 cameraRotation,Vector3 playerRotation)
    {
        this.cameraRotation=cameraRotation;
        this.playerRotation=playerRotation;
    }
    private void EnableInput() => isInputEnabled = true;
    private void DisableInput() => isInputEnabled = false;

    private void Update()
    {
        if (!isInputEnabled) return;
        
        isGrounded = Physics.CheckSphere(groundCheck.position, playerState.groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        
        if (moveDirection != Vector3.zero)
            animator.SetBool("CanWalk", true);
        else
            animator.SetBool("CanWalk", false);
        characterController.Move(moveDirection * (playerState.Movespeed * Time.deltaTime));
        
        camera.transform.localRotation = Quaternion.Euler(cameraRotation);
        transform.Rotate(playerRotation);
        
        velocity.y += playerState.gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
