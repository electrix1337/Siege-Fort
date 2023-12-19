
using UnityEngine;
using UnityEngine.UIElements;

public class activateOnRotation : MonoBehaviour
{
    public void ActivateOnRotation(Quaternion rotation)
    {
        gameObject.GetComponent<RectTransform>().rotation = rotation;
    }
}
