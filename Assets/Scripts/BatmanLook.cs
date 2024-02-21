using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatmanLook : MonoBehaviour
{
    GameObject head;

    float xCameraRotation = 0;

    [SerializeField]
    Vector2 sensitvity = Vector2.one;

    [SerializeField]
    float angleLimiter = 80;

    private void Awake(){
        head = GetComponentInChildren<Camera>().gameObject;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
}