using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControlComponent : MonoBehaviour
{
    [SerializeField] InputActionAsset actionAsset;
    [SerializeField] float cameraSpeed = 10;
    [SerializeField] float speedUp = 10;

    float speed;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        InputActionMap actionMap = actionAsset.FindActionMap("Camera");
        InputAction moveCamera = actionAsset.FindAction("Move");
        InputAction speedUpCamera = actionAsset.FindAction("SpeedCamera");

        moveCamera.performed += Move;
        moveCamera.canceled += Move;

        speedUpCamera.performed += SpeedCamera;
        speedUpCamera.canceled += SpeedCamera;

        speed = cameraSpeed;
    }

    void SpeedCamera(InputAction.CallbackContext action)
    {
        speed = action.ReadValue<float>() == 0 ? cameraSpeed : cameraSpeed + speedUp;
        Debug.Log("salut");
    }

    void Move(InputAction.CallbackContext action)
    {
        movement = action.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        gameObject.transform.Translate(new Vector3(movement.x * time * speed, 0, movement.y * time * speed), Space.World);
    }
}
