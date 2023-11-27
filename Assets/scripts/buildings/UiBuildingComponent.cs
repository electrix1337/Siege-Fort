using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(buildingInfoComponent))]
public class UiBuildingComponent : MonoBehaviour
{

    [SerializeField] GameObject sectionContainer;
    [SerializeField] GameObject sectionButton;
    [SerializeField] GameObject buildingButton;
    [SerializeField] GameObject triggerManager;
    [SerializeField] GameObject buildingSelection;
    [SerializeField] GameObject prefabBuildingUI;

    triggerManagerComponent triggerComponent;
    buildingInfoComponent buildingInfo;
    RectTransform sectionRect;
    List<(string, Transform, List<(string, Transform)>)> SectionShowed = new List<(string, Transform, List<(string, Transform)>)>();
    List<GameObject> sections = new List<GameObject>();

    private void Start()
    {
        buildingInfo = GetComponent<buildingInfoComponent>();
        triggerComponent = triggerManager.GetComponent<triggerManagerComponent>();

        sectionRect = sectionContainer.GetComponent<Image>().rectTransform;

        //set all active section in a list of section unlocked
        List<buildingSectionSerialized> sectionsUnlock = buildingInfo.GetActiveBuildingSection();

        SetSections(sectionsUnlock);
        sectionContainer.SetActive(false);

        Button button = buildingButton.GetComponent<Button>();
    }

    bool CreateNewSections(List<buildingSectionSerialized> sectionUnlocked)
    {
        bool unlockedSection = false;
        //loop and create new section that are meant to be created
        for (int i = 0; i < sectionUnlocked.Count(); ++i)
        {

            if (!SectionShowed.Exists((obj) => obj.Item1 == sectionUnlocked[i].name))
            {
                //create the button game object
                GameObject clone = Instantiate(sectionButton, sectionContainer.transform);
                Transform child = clone.transform.GetChild(0);
                child.GetComponent<Text>().text = sectionUnlocked[i].name;//set the text on the image buttoN

                //set button trigger
                clone.GetComponent<ClickEventComponent>().triggerManagerComponent = triggerComponent;
                int i1 = i;
                clone.GetComponent<Button>().onClick.AddListener(() =>  
                     triggerComponent.ClickEventSection(sectionUnlocked[i1].name));

                //transform the button
                RectTransform transform = clone.GetComponent<Image>().rectTransform;
                SectionShowed.Add((sectionUnlocked[i].name, transform, new List<(string, Transform)>()));
                unlockedSection = true;

                //create the building section link to it
                GameObject buildingSelection1 = new GameObject(sectionUnlocked[i].name);
                buildingSelection1.transform.parent = buildingSelection.transform;
                sections.Add(buildingSelection1);
                //buildingSelection1.SetActive(false);
            }
            UpdateBuildings(sectionUnlocked[i].name);
        }
        return unlockedSection;
    }
    //update the buildings
    void UpdateBuildings(string sectionName)
    {
        CreateNewBuildings(sectionName);
        PlaceBuilding(sectionName);
    }

    //change the position of the building
    void PlaceBuilding(string sectionName)
    {
        const int sectionHeight = 30;

        List<buildingInfoSerialized> buildings = buildingInfo.buildingsSections.Find((obj) => obj.name == sectionName).buildingsSerialized;
        (string, Transform, List<(string, Transform)>) showedBuildingSection = SectionShowed.Find((obj) => obj.Item1 == sectionName);
        for (int i = 0; i < buildings.Count; ++i)
        {
            if (buildings[i].unlocked)
            {
                Transform recTransform =
                   showedBuildingSection.Item3.Find((obj) => obj.Item1 == buildings[i].name).Item2;

                recTransform.localPosition = new Vector3(150 + 40 + Mathf.Floor(i / 2) * 80, 80 + (i % 2) * 100);
            }
        }
    }
    //create a new building button if you have a new building unlocked
    void CreateNewBuildings(string sectionName)
    {
        List<buildingInfoSerialized> buildings = buildingInfo.buildingsSections.Find((obj) => obj.name == sectionName).buildingsSerialized;
        for (int i = 0; i < buildings.Count; ++i)
        {
            (string, Transform, List<(string, Transform)>) showedBuildingList = SectionShowed.Find((obj) => obj.Item1 == sectionName);
            if (buildings[i].unlocked &&
                !showedBuildingList.Item3.Exists((obj) => obj.Item1 == buildings[i].name))
            {
                
                GameObject clone = Instantiate(prefabBuildingUI, sections.Find((obj) => obj.name == sectionName).transform,
                    showedBuildingList.Item2);
                clone.name = buildings[i].name;
                clone.transform.GetChild(1).GetComponent<RawImage>().texture = buildings[i].buildingImage;
                clone.transform.GetChild(0).GetComponent<Text>().text = buildings[i].name;
                showedBuildingList.Item3.Add((buildings[i].name, clone.transform));
            }
        }
    }

    void SetSections(List<buildingSectionSerialized> sectionUnlocked)
    {
        //loop and create new section that are meant to be created
        bool unlockedSection = CreateNewSections(sectionUnlocked);

        if (unlockedSection)
        {
            SetSectionTransform();
        } 
    }
    void SetSectionTransform()
    {
        const int sectionHeight = 30;
        //this for loop is repeated to make sure that all section always stay in the same order (It can be more
        //efficient but the order wont be the same on multiple runs
        int nbChange = 0;
        for (int i = 0; i < buildingInfo.Count; ++i)
        {
            if (buildingInfo.buildingsSections[i].active)
            {
                //if the section exist in the active section list
                Transform recTransform = 
                    SectionShowed.First(obj => obj.Item1 == buildingInfo.buildingsSections[i].name).Item2;

                recTransform.localPosition = new Vector3(0, -(SectionShowed.Count * 30 / 2 - 15) + sectionHeight * nbChange);
                nbChange++;
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
