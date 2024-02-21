using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BatmanController : MonoBehaviour
{
    Vector2 inputVector = Vector2.zero;
    CharacterController characterController;

    [SerializeField]
    float movementSpeed = 5f;

    [SerializeField]
    float jumpForce = 20;

    [SerializeField]
    float gravityMultiplier = 4;

    float velocityY = -1f;

    bool jumpPressed = false;

    GameObject head;

    float xCameraRotation = 0;

    [SerializeField]
    Vector2 sensitvity = Vector2.one;

    [SerializeField]
    float angleLimiter = 30;

    void Awake(){
        characterController = GetComponent<CharacterController>();

        head = GetComponentInChildren<Camera>().gameObject;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

        velocityY += Physics.gravity.y * Time.deltaTime * gravityMultiplier;
        movement.y = velocityY;

        characterController.Move(movement * Time.deltaTime);
        
        jumpPressed = false;

        if(movement.magnitude > 0){
            GetComponent<Animator>().SetBool("Walking", true);
        }
        else{
            GetComponent<Animator>().SetBool("Walking", false);
        }

        GetComponent<CharacterController>().Move(movement);
    }

    void OnLook(InputValue value){
        Vector2 lookVector = value.Get<Vector2>();
        
        float degreesY = lookVector.x * sensitvity.x * Time.smoothDeltaTime;
        transform.Rotate(Vector3.up, degreesY);

        float degreesX = -lookVector.y * sensitvity.y * Time.smoothDeltaTime;
        xCameraRotation += degreesX;
        xCameraRotation = Mathf.Clamp(xCameraRotation, -angleLimiter, angleLimiter);
        head.transform.localEulerAngles = new(xCameraRotation, 0, 0);
    }

    void OnMove(InputValue value) => inputVector = value.Get<Vector2>();
    void OnJump(InputValue value) => jumpPressed = true;
}


