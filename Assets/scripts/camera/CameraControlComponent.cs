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

    List<activateOnRotation> activateOnRotationList = new List<activateOnRotation>();

    float speed;
    Vector2 movement;
    bool isRotating;
    float x;
    float y;

    private void Awake()
    {
        GameObjectPath.AddPath("Camera", gameObject);
    }
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

    public void AddRotatingUI(activateOnRotation activateOnRotation)
    {
        activateOnRotationList.Add(activateOnRotation);
        activateOnRotation.ActivateOnRotation(gameObject.transform.rotation);
    }

    public void RemoveRotatingUI(activateOnRotation activateOnRotation)
    {
        activateOnRotationList.Remove(activateOnRotation);
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

            if (transform.eulerAngles.x - x * rotationSpeed.x < maxVerticalRotation && transform.eulerAngles.x - x * rotationSpeed.x > minVerticalRotation)
            {
                transform.eulerAngles = transform.eulerAngles - new Vector3(x * rotationSpeed.x, y * rotationSpeed.y * -1, 0);
            }

            for  (int i = 0; i < activateOnRotationList.Count; i++)
            {
                activateOnRotationList[i].ActivateOnRotation(gameObject.transform.rotation);
            }
        }

        float time = Time.deltaTime;
        gameObject.transform.Translate(Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) *
            new Vector3(movement.x * time * speed, 0, movement.y * time * speed), Space.World);

        Vector3 oldPosition = gameObject.transform.position;
        gameObject.transform.Translate(new Vector3(0, 0, scroll * scrollSpeed), Space.Self);
        if ((gameObject.transform.position.y < minHeightPosition && scroll > 0) || (gameObject.transform.position.y > maxHeightPosition && scroll < 0))
        {
            gameObject.transform.position = oldPosition;
        }
    }
}
