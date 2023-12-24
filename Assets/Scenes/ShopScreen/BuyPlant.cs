using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPlant : MonoBehaviour
{
    public void Buy (Plant plant)
    {
        if (SaveLoadGameLogic.data.plants.Count < 9 && SaveLoadGameLogic.data.money >= plant.price)
        {
            SaveLoadGameLogic.data.money -= plant.price;
            SaveLoadGameLogic.data.plants.Add(MyPlantsLogic.getPlantNewRepresentationByName(plant.name));
        }
    }
    public void BuyFicus()
    {
        Plant plant = new Ficus();
        Buy(plant);
    }
    public void BuyCactus()
    {
        Plant plant = new Cactus();
        Buy(plant);
    }
    public void BuyCyclamen()
    {
        Plant plant = new Cyclamen();
        Buy(plant);
    }
}
