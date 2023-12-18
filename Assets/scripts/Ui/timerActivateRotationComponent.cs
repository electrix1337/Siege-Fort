using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(activateOnRotation))]
public class timerActivateRotationComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find(GameObjectPath.GetPath("Camera")).GetComponent<CameraControlComponent>().AddRotatingUI(
            gameObject.GetComponent<activateOnRotation>());
    }
}
