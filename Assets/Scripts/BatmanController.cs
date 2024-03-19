using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Vector2 inputVector = Vector2.zero;
    CharacterController characterController;

    [SerializeField]
    float movementSpeed = 5f;

    [SerializeField]
    float jumpForce = 20;

    [SerializeField]
    float gravityMultiplier = 4;

    [SerializeField]
    float dashSpeed = 20f;

    bool dashPressed = false;
    
    int dashTimerReset = 3;
    float dashTimer = 0;

    float velocityY = -1f;

    bool jumpPressed = false;

    void Awake(){
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 movement = transform.right * inputVector.x + transform.forward * inputVector.y;
        movement *= movementSpeed;

        if(characterController.isGrounded){
            velocityY = -1f;
            if(jumpPressed){
                velocityY = jumpForce;
            }
        }

        if(dashTimer > dashTimerReset && dashPressed){
            dashPressed = false;
            dashTimer = 0;
            movementSpeed = dashSpeed;
            characterController.Move(movement * Time.deltaTime * movementSpeed);
        }       


        velocityY += Physics.gravity.y * Time.deltaTime * gravityMultiplier;
        movement.y = velocityY;

        characterController.Move(movement * Time.deltaTime);
        
        dashTimer += Time.deltaTime;
        jumpPressed = false;
        dashPressed = false;
        movementSpeed = 5f;
        dashTimer++;
    }

    void OnMove(InputValue value) => inputVector = value.Get<Vector2>();
    void OnJump(InputValue value) => jumpPressed = true;
    void OnDash(InputValue value) => dashPressed = true;
}
