using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerComponent : MonoBehaviour
{
    [SerializeField] InputActionAsset asset;
    [SerializeField] float cancelTime = 0.5f;
    [SerializeField] GameObject buildingManager;
    [SerializeField] GameObject camera;
    List<ICancel> iCancels = new List<ICancel>();
    bool cancelPressed = false;
    float time = 0;
    bool RotationActive = false;
    CameraControlComponent cameraControl;

    private void Start()
    {
        iCancels.Add(buildingManager.GetComponent<ICancel>());
        cameraControl = camera.GetComponent<CameraControlComponent>();
    }

    //cancel all actions when pressing the right click
    void Cancel()
    {
        for (int i = 0;  i < iCancels.Count; i++)
        {
            iCancels[i].Cancel();
        }
    }

    private void Update()
    {
        //cancel button is right click
        if (Mouse.current.rightButton.isPressed)
        {
            if (time > cancelTime)
            {
                cancelPressed = false;
            }
            else
            {
                time += Time.deltaTime;
                cancelPressed = true;
            }
            if (!RotationActive)
            {
                cameraControl.ActiveRotation(true);
                RotationActive = true;
            }
        }
        else if (cancelPressed)
        {
            Cancel();
            time = 0;
            cancelPressed = false;
            cameraControl.ActiveRotation(false);
            RotationActive = false;
        }
        else if (time > 0)
        {
            time = 0;
            cameraControl.ActiveRotation(false);
            RotationActive = false;
        }
    }
}
