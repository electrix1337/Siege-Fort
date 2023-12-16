using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class loadingComponent : MonoBehaviour
{
    [SerializeField] GameObject loadingCanvas;
    [SerializeField] GameObject ingameCanvas;
    [SerializeField] float barRightOffset;
    [SerializeField] float aroundOffset;
    RectTransform loadingBar;

    public void Awake()
    {
        //loadingCanvas.SetActive(true);
        loadingBar = loadingCanvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        SetLoadingBar(1);
    }
    public void SetLoadingBar(float percent)
    {
        float barSize = Screen.width - aroundOffset;
        loadingBar.offsetMax = new Vector2(-(barRightOffset + barSize - barSize * (percent / 100)), loadingBar.offsetMax.y);
    }
    public void GoIngame()
    {
        loadingCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
    }
}
