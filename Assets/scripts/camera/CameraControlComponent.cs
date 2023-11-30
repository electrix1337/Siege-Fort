using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControlComponent : MonoBehaviour
{
    [SerializeField] InputActionAsset actionAsset;
    [SerializeField] float cameraSpeed = 10;
    [SerializeField] float scrollSpeed = 50;
    [SerializeField] float speedUp = 10;
    [SerializeField] Vector2 rotationSpeed;
    [SerializeField] float minHeightPosition;
    [SerializeField] float maxHeightPosition;
    [SerializeField] float minVerticalRotation;
    [SerializeField] float maxVerticalRotation;

    float speed;
    Vector2 movement;
    bool isRotating;
    float x;
    float y;
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
    }

    void Move(InputAction.CallbackContext action)
    {
        movement = action.ReadValue<Vector2>();
    }

    public void ActiveRotation(bool active)
    {
        isRotating = active;
    }

    // Update is called once per frame
    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (isRotating)
        {
            //i copied this part of the code in this website: https://discussions.unity.com/t/how-to-rotate-my-camera/164349/2
            y = Input.GetAxis("Mouse X");
            x = Input.GetAxis("Mouse Y");

            Debug.Log(transform.eulerAngles.x - x * rotationSpeed.x);
            if (transform.eulerAngles.x - x * rotationSpeed.x < maxVerticalRotation && transform.eulerAngles.x - x * rotationSpeed.x > minVerticalRotation)
            {
                transform.eulerAngles = transform.eulerAngles - new Vector3(x * rotationSpeed.x, y * rotationSpeed.y * -1, 0);
            }

        }

        float time = Time.deltaTime;
        gameObject.transform.Translate(Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) *
            new Vector3(movement.x * time * speed, 0, movement.y * time * speed), Space.World);

        Vector3 oldPosition = gameObject.transform.position;
        gameObject.transform.Translate(new Vector3(0, 0, scroll * scrollSpeed), Space.Self);
        if (scroll != 0)
        {
            Debug.Log(scroll);
        }
        if ((gameObject.transform.position.y < minHeightPosition && scroll > 0) || (gameObject.transform.position.y > maxHeightPosition && scroll < 0))
        {
            gameObject.transform.position = oldPosition;
        }
    }
}