using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FicusBuy : MonoBehaviour
{
    public void Buy ()
    {
        Plant plant = new Ficus();
        if (SaveLoadGameLogic.data.plants.Count < 9 && SaveLoadGameLogic.data.money >= plant.price)
        {
            SaveLoadGameLogic.data.money -= plant.price;
            SaveLoadGameLogic.data.plants.Add(MyPlantsLogic.getPlantNewRepresentationByName(plant.name));
        }
    }
}
