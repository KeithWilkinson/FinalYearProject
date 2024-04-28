using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    public float sensitivity;
    public float camMoveSpeed;
    float currentSpeed;
    [SerializeField]private GameObject _car;

    void Update()
    {
        // Hide cursor and allows camera movement and rotation
        if (Input.GetMouseButton(1) && Menu._hasGameStarted == true && _car.GetComponent<CarControls>().playerCamera.isActiveAndEnabled == false) 
        {
           // Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            Movement();
            Rotation();
        }
    }

    // Rotate camera
    public void Rotation()
    {
        Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        transform.Rotate(mouseInput * sensitivity);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    // Move camera
    public void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        currentSpeed = camMoveSpeed;
        
        transform.Translate(input * currentSpeed * Time.deltaTime);
    }
}
