using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
}
