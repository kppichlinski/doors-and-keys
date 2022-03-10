using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector2 panLimit;

    private InputMaster.CameraActions controls;
    private float rotateInput;
    private Vector2 cameraCenter;

    private void Awake()
    {
        controls = new InputMaster().Camera;
    }

    private void Update()
    {
        MoveCamera();
        RotateCamera();
    }

    private void Start()
    {
        cameraCenter = new Vector2(transform.position.x, transform.position.z);
    }

    private void MoveCamera()
    {
        if (controls.Movement.IsPressed())
        {
            Vector3 position = transform.position;
            Vector2 movementInput = controls.Movement.ReadValue<Vector2>();
            Vector3 direction = transform.TransformDirection(new Vector3(movementInput.x, 0f, movementInput.y));
            direction.y = 0f;
            position += direction.normalized * movementSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, -panLimit.x + cameraCenter.x, panLimit.x + cameraCenter.x);
            position.z = Mathf.Clamp(position.z, -panLimit.y + cameraCenter.y, panLimit.y + cameraCenter.y);
            transform.position = position;
        }
    }

    private void RotateCamera()
    {
        if (controls.Rotation.IsPressed())
        {
            rotateInput = controls.Rotation.ReadValue<float>();
            transform.eulerAngles += new Vector3(0f, rotateInput, 0f).normalized * rotationSpeed * Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
