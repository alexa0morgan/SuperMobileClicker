using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlantClickHandler : MonoBehaviour
{
    private void OnMouseDown()
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
}
