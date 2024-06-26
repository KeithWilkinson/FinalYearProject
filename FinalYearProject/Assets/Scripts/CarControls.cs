using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CarControls : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool _carCam = true;
    [SerializeField] private Camera _mainCam;
    public static int carFlag;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            // Switch to free cam
            if(_carCam == true)
            {
                _carCam = false;
                _mainCam.enabled = true;
                playerCamera.enabled = false;
            }
            // Switch to car cam
            else
            {
                _carCam = true;
                _mainCam.enabled = false;
                playerCamera.enabled = true;
            }
        }

        // Move car
        if(_carCam == true)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float curSpeedX = walkSpeed * Input.GetAxis("Vertical");
            float curSpeedY = walkSpeed * Input.GetAxis("Horizontal");
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            characterController.Move(moveDirection * Time.deltaTime);

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}

