using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenMainScreen : MonoBehaviour
{
    static int bonusIntervalInHours = 1;
    static int lampIntervalInMinutes = 5;

    static DateTime dateTimeLastWatering;
    static DateTime dateTimeLastFertilizer;
    static DateTime dateTimeLastLamp;

    static DateTime dateTimeLampActivated;

    static bool isWaterButtonShown = true;
    static bool isLampButtonShown = true;
    static bool isFertilizerButtonShown = true;

    public void Scene1()
    {
        SceneManager.LoadScene("MainScreen");
    }

    private void Update()
    {
        if (!CanBeWatered() && isWaterButtonShown)
        {
            HideWater();
            isWaterButtonShown = false;
        }
        if (CanBeWatered() && !isWaterButtonShown)
        {
            ShowWater();
            isWaterButtonShown = true;
        }

        if (!CanBeFertilized() && isFertilizerButtonShown)
        {
            HideFertilizer();
            isFertilizerButtonShown = false;
        }
        if (CanBeFertilized() && !isFertilizerButtonShown)
        {
            ShowFertilizer();
            isFertilizerButtonShown = true;
        }

        if (!CanBeLamped() && isLampButtonShown)
        {
            HideLamp();
            isLampButtonShown = false;
        }
        if (CanBeLamped() && !isLampButtonShown)
        {
            ShowLamp();
            isLampButtonShown = true;
        }
    }

    public void clickWater()
    {
        dateTimeLastWatering = DateTime.Now;
        SaveLoadGameLogic.data.dateTimeLastWatering = dateTimeLastWatering.ToString();

        foreach (PlantRepresentation plant in SaveLoadGameLogic.data.plants)
        {
            if (plant.phase < 2)
            {
                plant.phase++;
            }
        }

        MyPlantsLogic.redrawPlants();
    }

    public void clickLamp()
    {
        dateTimeLastLamp = DateTime.Now;
        SaveLoadGameLogic.data.dateTimeLastLamp = dateTimeLastLamp.ToString();

        dateTimeLampActivated = DateTime.Now;
        InvokeRepeating("AddMoney", 1f, 1f);
    }

    public void AddMoney()
    {
        if ((DateTime.Now - dateTimeLampActivated).TotalMinutes >= lampIntervalInMinutes)
        {
            return;
        }
        SaveLoadGameLogic.data.money += MyPlantsLogic.getCurrentCoinsPerSecond();
    }

    public void clickFertilizer()
    {
        dateTimeLastFertilizer = DateTime.Now;
        SaveLoadGameLogic.data.dateTimeLastFertilizer = dateTimeLastFertilizer.ToString();

        SaveLoadGameLogic.data.money += (
            from plant
            in SaveLoadGameLogic.data.plants
            select MyPlantsLogic.getPlantByRepresentation(plant).phases[plant.phase].coinsPerSecond
        ).Sum() * 3600;
    }

    public static bool CanBeWatered()
    {
        DateTime.TryParse(SaveLoadGameLogic.data.dateTimeLastWatering, out dateTimeLastWatering);
        return (DateTime.Now - dateTimeLastWatering).TotalHours >= bonusIntervalInHours;
    }
    public static bool CanBeLamped()
    {
        DateTime.TryParse(SaveLoadGameLogic.data.dateTimeLastLamp, out dateTimeLastLamp);
        return (DateTime.Now - dateTimeLastLamp.AddMinutes(lampIntervalInMinutes)).TotalHours >= bonusIntervalInHours;
    }
    public static bool CanBeFertilized()
    {
        DateTime.TryParse(SaveLoadGameLogic.data.dateTimeLastFertilizer, out dateTimeLastFertilizer);
        return (DateTime.Now - dateTimeLastFertilizer).TotalHours >= bonusIntervalInHours;
    }

    public void HideWater()
    {
        GameObject.FindGameObjectWithTag("Water").transform.localScale = Vector3.zero;
    }
    public void HideFertilizer()
    {
        GameObject.FindGameObjectWithTag("Fertilizer").transform.localScale = Vector3.zero;
    }
    public void HideLamp()
    {
        GameObject.FindGameObjectWithTag("Lamp").transform.localScale = Vector3.zero;
    }
    public void ShowWater()
    {
        GameObject.FindGameObjectWithTag("Water").transform.localScale = Vector3.one;
    }
    public void ShowFertilizer()
    {
        GameObject.FindGameObjectWithTag("Fertilizer").transform.localScale = Vector3.one;
    }
    public void ShowLamp()
    {
        GameObject.FindGameObjectWithTag("Lamp").transform.localScale = Vector3.one;
    }
}