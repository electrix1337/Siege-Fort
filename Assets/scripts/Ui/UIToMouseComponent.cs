using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToMouseComponent : MonoBehaviour
{
    [SerializeField] Vector2 position;
    RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        gameObject.transform.position = mouse + new Vector3(rectTransform.rect.width / 2 + position.x, rectTransform.rect.height / 2 + position.y, 10);
    }
}
