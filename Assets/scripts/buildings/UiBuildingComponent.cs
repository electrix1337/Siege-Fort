using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(buildingInfoComponent))]
public class UiBuildingComponent : MonoBehaviour
{

    [SerializeField] GameObject sectionContainer;
    [SerializeField] GameObject sectionButton;
    [SerializeField] GameObject buildingButton;

    RectTransform sectionRect;
    List<(string, RectTransform)> SectionShowed = new List<(string, RectTransform)>();
    buildingInfoComponent buildingInfo;

    private void Start()
    {
        buildingInfo = GetComponent<buildingInfoComponent>();

        sectionRect = sectionContainer.GetComponent<Image>().rectTransform;

        //set all active section in a list of section unlocked
        List<buildingSectionSerialized> sectionsUnlock = buildingInfo.GetActiveBuildingSection();

        SetSections(sectionsUnlock);
        sectionContainer.SetActive(false);

        Button button = buildingButton.GetComponent<Button>();

    }

    bool UnlockNewSection(List<buildingSectionSerialized> sectionUnlocked)
    {
        bool unlockedSection = false;
        //loop and create new section that are meant to be created
        for (int i = 0; i < sectionUnlocked.Count(); ++i)
        {

            if (SectionShowed.Exists((obj) => obj.Item1 == sectionUnlocked[i].name))
            {
                GameObject clone = Instantiate(sectionButton, sectionContainer.transform);
                Transform child = clone.transform.GetChild(0);
                child.GetComponent<Text>().text = sectionUnlocked[i].name;//set the text on the image button

                RectTransform transform = clone.GetComponent<Image>().rectTransform;
                SectionShowed.Add((sectionUnlocked[i].name, transform));
                unlockedSection = true;
            }
        }
        return unlockedSection;
    }

    void SetSections(List<buildingSectionSerialized> sectionUnlocked)
    {
        //loop and create new section that are meant to be created
        bool unlockedSection = UnlockNewSection(sectionUnlocked);


        if (unlockedSection)
        {
            Debug.Log("salut");
            SetSectionTransform();
        } 
    }
    void SetSectionTransform()
    {
        const int sectionHeight = 30;
        //this for loop is repeated to make sure that all section always stay in the same order (It can be more
        //efficient but the order wont be the same on multiple runs
        for (int i = 0; i < buildingInfo.Count; ++i)
        {
            if (buildingInfo.buildingsSections[i].active)
            {
                //if the section exist in the active section list
                RectTransform recTransform = 
                    SectionShowed.First(obj => obj.Item1 == buildingInfo.buildingsSections[i].name).Item2;

                recTransform.localPosition = new Vector3(0, -(SectionShowed.Count * 30 / 2 - 15) + sectionHeight * i);
            }
        }

        ChangeSectionSize();
    }

    //change the size of the building section selector
    void ChangeSectionSize()
    {
        sectionRect.sizeDelta = new Vector2(150, SectionShowed.Count * 30);
        sectionRect.position = new Vector3(75, 30 + SectionShowed.Count * 30 / 2, 0);
    }
}
