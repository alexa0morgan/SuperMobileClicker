using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenMyPlants : MonoBehaviour
{
    public static int currentPage;
    public static int plantsOnPage = 4;

    public void Scene1()
    {
        SceneManager.LoadScene("MyPlantsScreen");
        currentPage = 1;
        SceneManager.sceneLoaded += RedrawPlantsHandler;
    }

    public static void RedrawPlants()
    {
        int totalPlantsCount = SaveLoadGameLogic.data.plants.Count;
        int start = plantsOnPage * (currentPage - 1);
        int end = Math.Min(start + plantsOnPage, totalPlantsCount);

        for (int i = start, j = 1; i < end; i++, j++)
        {
            GameObject panel = GameObject.FindGameObjectWithTag("plant-" + j);
            panel.transform.localScale = Vector3.one;

            foreach( Transform element in panel.GetComponentInChildren<Transform>())
            {
                SetElementValue(element, SaveLoadGameLogic.data.plants[i]);
            }
        }

        // Remove redundant panels (If number of flowers is greated than number of panel on the page)
        if (start + plantsOnPage > totalPlantsCount)
        {
            int panelsToDelete = start + plantsOnPage - totalPlantsCount;
            for (int i = 4; i > plantsOnPage - panelsToDelete; i--)
            {
                GameObject panel = GameObject.FindGameObjectWithTag("plant-" + i);
                panel.transform.localScale = Vector3.zero;
            }
        }
    }

    private static void SetElementValue(Transform element, PlantRepresentation plant)
    {
        var text = element.GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            return;
        }

        switch (element.tag)
        {

            case "PlantTitle":
                text.text = plant.name;
                break;
            case "PlantPhase":
                text.text = "Phase: " + (plant.phase + 1).ToString() + "/3";
                break;
            case "PlantTotalClicks":
                text.text = "Total clicks: " + plant.clicks.ToString();
                break;
            case "PlantTime":
                text.text = MyPlantsLogic.getPlantByRepresentation(plant).phases[plant.phase].coinsPerSecond.ToString() + "/sec";
                break;

        }
    }

    public static void RedrawPlantsHandler(Scene scene, LoadSceneMode mode)
    {
        RedrawPlants();
    }
}