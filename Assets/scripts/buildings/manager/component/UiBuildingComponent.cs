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


[RequireComponent(typeof(BuildingInfoComponent))]
public class UiBuildingComponent : MonoBehaviour
{

    [SerializeField] GameObject sectionContainer;
    [SerializeField] GameObject sectionButton;
    [SerializeField] GameObject buildingButton;
    [SerializeField] GameObject triggerManager;
    [SerializeField] GameObject buildingSelection;
    [SerializeField] GameObject prefabBuildingUI;

    TriggerManagerComponent triggerComponent;
    BuildingInfoComponent buildingInfo;
    RectTransform sectionRect;
    List<(string, Transform, List<(string, Transform)>)> SectionShowed = new List<(string, Transform, List<(string, Transform)>)>();
    List<GameObject> sections = new List<GameObject>();

    private void Start()
    {
        buildingInfo = GetComponent<BuildingInfoComponent>();
        triggerComponent = triggerManager.GetComponent<TriggerManagerComponent>();

        sectionRect = sectionContainer.GetComponent<Image>().rectTransform;

        //set all active section in a list of section unlocked
        List<BuildingSectionSerialized> sectionsUnlock = buildingInfo.GetActiveBuildingSection();

        SetSections(sectionsUnlock);
        sectionContainer.SetActive(false);

        Button button = buildingButton.GetComponent<Button>();
    }

    //set trigger on a new section and on other section that are there
    void setSectionTriggers(string sectionName, GameObject newObject)
    {
        List<SubTriggerSerialized> triggers = new List<SubTriggerSerialized>();
        SubTriggerSerialized trigger = new SubTriggerSerialized();
        trigger.name = sectionName;
        trigger.obj = newObject;
        trigger.type = TriggerType.Activate;
        triggers.Add(trigger);

        //trigger to desactivate 
        List<SubTriggerSerialized> SubTriggers = new List<SubTriggerSerialized>();
        SubTriggerSerialized subTrigger = new SubTriggerSerialized();
        subTrigger.name = sectionName;
        subTrigger.obj = newObject;
        subTrigger.type = TriggerType.Desactivate;
        SubTriggers.Add(subTrigger);
        for (int i = 0; i < sections.Count; ++i)
        {
            if (sections[i].name != sectionName)
            {
                //desactivate all other menu that are already there
                trigger = new SubTriggerSerialized();
                trigger.name = sections[i].name;
                trigger.obj = sections[i];
                trigger.type = TriggerType.Desactivate;
                triggers.Add(trigger);

                //desactivate this menu when entering other menu that are there
                triggerComponent.AddSubTriggers(SectionShowed[i].Item1, SubTriggers);
            }
        }

        triggerComponent.AddTrigger(sectionName, triggers, false);
    }

    bool CreateNewSections(List<BuildingSectionSerialized> sectionUnlocked)
    {
        bool unlockedSection = false;
        //loop and create new section that are meant to be created
        for (int i = 0; i < sectionUnlocked.Count(); ++i)
        {

            if (!SectionShowed.Exists((obj) => obj.Item1 == sectionUnlocked[i].name))
            {
                //create the button game object
                GameObject clone = Instantiate(sectionButton, sectionContainer.transform);
                clone.name = sectionUnlocked[i].name;
                Transform child = clone.transform.Find("text");
                child.GetComponent<Text>().text = sectionUnlocked[i].name;//set the text on the image buttoN


                //create the building section link to it
                GameObject buildingSelection1 = new GameObject(sectionUnlocked[i].name);
                buildingSelection1.transform.parent = buildingSelection.transform;
                sections.Add(buildingSelection1);
                buildingSelection1.SetActive(false);

                //set button trigger
                setSectionTriggers(sectionUnlocked[i].name, buildingSelection1);
                ClickEventComponent clickEventComponent = clone.GetComponent<ClickEventComponent>();
                clickEventComponent.triggerManagerComponent = triggerComponent;
                int i1 = i;
                clone.GetComponent<Button>().onClick.AddListener(() =>
                     clickEventComponent.OnClick(sectionUnlocked[i1].name));

                //transform the button
                RectTransform transform = clone.GetComponent<Image>().rectTransform;
                SectionShowed.Add((sectionUnlocked[i].name, transform, new List<(string, Transform)>()));
                unlockedSection = true;
            }
            UpdateBuildings(sectionUnlocked[i].name);
        }
        return unlockedSection;
    }
    //update the buildings
    void UpdateBuildings(string sectionName)
    {
        CreateNewBuildings(sectionName);
        SetBuilding(sectionName);
    }

    //set a trigger to build a new building
    void SetBuildingTrigger(string buildingName, string sectionName)
    {
        List<SubTriggerSerialized> triggers = new List<SubTriggerSerialized>();
        //settings for the trigger
        SubTriggerSerialized trigger = new SubTriggerSerialized();
        List<string> arguments = new List<string>();
        arguments.Add(buildingName);
        arguments.Add(sectionName);
        string buildingButton = buildingName + "_button";
        trigger.name = buildingButton;
        trigger.obj = gameObject;
        trigger.type = TriggerType.Function;
        trigger.componentName = "PlacingBuildingComponent";
        trigger.functionName = "PlacingBuilding";
        trigger.arguments = arguments;
        triggers.Add(trigger);

        //add the trigger
        triggerComponent.AddTrigger(buildingButton, triggers, false);

    }

    //change the position of the building
    void SetBuilding(string sectionName)
    {
        const int sectionHeight = 30;

        List<BuildingSerialized> buildings = buildingInfo.buildingsSections.Find((obj) => obj.name == sectionName).buildingsSerialized;
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
        List<BuildingSerialized> buildings = buildingInfo.buildingsSections.Find((obj) => obj.name == sectionName).buildingsSerialized;
        for (int i = 0; i < buildings.Count; ++i)
        {
            (string, Transform, List<(string, Transform)>) showedBuildingList = SectionShowed.Find((obj) => obj.Item1 == sectionName);
            string buildingName = buildings[i].name;
            if (buildings[i].unlocked &&
                !showedBuildingList.Item3.Exists((obj) => obj.Item1 == buildingName))
            {
                //create a new building button
                GameObject clone = Instantiate(prefabBuildingUI, sections.Find((obj) => obj.name == sectionName).transform,
                    showedBuildingList.Item2);
                clone.name = buildingName;
                clone.transform.Find("image").GetComponent<RawImage>().texture = buildings[i].buildingImage;
                clone.transform.Find("text").GetComponent<Text>().text = buildingName;
                showedBuildingList.Item3.Add((buildingName, clone.transform));

                //set triggers for the building
                SetBuildingTrigger(buildingName, sectionName);
                ClickEventComponent clickEventComponent = clone.GetComponent<ClickEventComponent>();
                clickEventComponent.triggerManagerComponent = triggerComponent;
                string buildingButton = buildingName + "_button";
                clone.GetComponent<Button>().onClick.AddListener(() =>
                     clickEventComponent.OnClick(buildingButton));
            }
        }
    }

    void SetSections(List<BuildingSectionSerialized> sectionUnlocked)
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
