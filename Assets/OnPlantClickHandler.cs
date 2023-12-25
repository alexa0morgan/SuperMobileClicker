using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlantClickHandler : MonoBehaviour
{
    public DateTime mouseWasHold;
    public int secondsToHoldToDelete = 5;

    public void MouseHold()
    {
        mouseWasHold = DateTime.Now;
    }
    public void MouseUnhold()
    {
        if ((DateTime.Now - mouseWasHold).TotalSeconds >= secondsToHoldToDelete)
        {
            GameObject place = gameObject;

            int index = MyPlantsLogic.getPlantIndexByPlace(place);
            SaveLoadGameLogic.data.plants.RemoveAt(index);

            MyPlantsLogic.redrawPlants();
        } else
        {
            clickOnPlant();
        }
    }
    public void clickOnPlant()
    {
        GameObject place = gameObject;
        GameObject plant = place.transform.GetChild(0).gameObject;

        SaveLoadGameLogic.data.money += MyPlantsLogic.getCoinsOnClickByTag(plant.tag);

        PlantRepresentation plantRepresentation = MyPlantsLogic.getPlantByPlace(place);
        plantRepresentation.clicks += 1;

        Phase plantPhase = MyPlantsLogic.getPlantByRepresentation(plantRepresentation).phases[plantRepresentation.phase];
        if (plantPhase.clicksToUpgrade <= plantRepresentation.clicks)
        {
            plantRepresentation.phase += 1;
            MyPlantsLogic.redrawPlants();

        }

        SetScore.RedrawScore();
    }
    public void OnMouseDown()
    {
        
    }
}
