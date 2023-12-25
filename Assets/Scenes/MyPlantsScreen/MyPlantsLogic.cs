using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Phase
{
    public int coinsPerClick { get; set; }
    public int coinsPerSecond { get; set; }
    public int clicksToUpgrade { get; set; }
}


[Serializable]
public class PlantRepresentation
{
    public string name;
    public int phase;
    public int clicks;

    public override bool Equals(object obj)
    {
        var item = obj as PlantRepresentation;

        if (item == null)
        {
            return false;
        }

        return name.ToLower().Equals(item.name.ToLower()) && phase.Equals(item.phase);
    }
}

public class Plant
{
    public string name;
    public int price;
    public Phase[] phases;
}


public class Ficus : Plant
{
    public Ficus()
    {
        name = "Ficus";
        price = 1000;
        phases = new Phase[3]
        {
            new Phase(){ coinsPerClick = 1, coinsPerSecond = 1, clicksToUpgrade = 301},
            new Phase(){ coinsPerClick = 2, coinsPerSecond = 2, clicksToUpgrade = 601},
            new Phase(){coinsPerClick = 3, coinsPerSecond = 3, clicksToUpgrade = int.MaxValue}
        };
    }
}

public class Cyclamen : Plant
{
    public Cyclamen()
    {
        name = "Cyclamen";
        price = 5000;
        phases = new Phase[3]
        {
            new Phase(){ coinsPerClick = 5, coinsPerSecond = 5, clicksToUpgrade = 1001},
            new Phase(){ coinsPerClick = 7, coinsPerSecond = 7, clicksToUpgrade = 2001},
            new Phase(){coinsPerClick = 10, coinsPerSecond = 10, clicksToUpgrade = int.MaxValue}
        };
    }
}


public class Cactus : Plant
{
    public Cactus()
    {
        name = "Cactus";
        price = 10_000;
        phases = new Phase[3]
        {
            new Phase(){ coinsPerClick = 12, coinsPerSecond = 12, clicksToUpgrade = 2501},
            new Phase(){ coinsPerClick = 15, coinsPerSecond = 15, clicksToUpgrade = 5001},
            new Phase(){coinsPerClick = 20, coinsPerSecond = 20, clicksToUpgrade = int.MaxValue}
        };
    }
}


public class MyPlantsLogic : MonoBehaviour
{
    public static List<PlantRepresentation> plants = new List<PlantRepresentation>();

    public static Plant getPlantByRepresentation(PlantRepresentation plant)
    {
        return getPlantByName(plant.name);
    }

    public static Plant getPlantByName(string plantName)
    {
        switch (plantName.ToLower())
        {
            case "ficus":
                return new Ficus();
            case "cyclamen":
                return new Cyclamen();
            case "cactus":
                return new Cactus();
        }
        return new Plant();
    }

    public static PlantRepresentation getPlantNewRepresentationByName(string name)
    {
        PlantRepresentation plantRepresentation = new PlantRepresentation();
        plantRepresentation.name = name;
        plantRepresentation.phase = 0;
        plantRepresentation.clicks = 0;

        return plantRepresentation;
    }

    public static int getCurrentCoinsPerSecond()
    {
        return plants.Select(
            plant =>
                getPlantByRepresentation(plant)
                .phases[plant.phase]
                .coinsPerSecond
        ).Sum();
    }

    public static GameObject getPrefabByPlant(PlantRepresentation plant)
    {
        string prefabName = "Common/" + plant.name + "/" + plant.name.ToLower() + (plant.phase + 1);
        return Resources.Load(prefabName) as GameObject;
    }

    public static int getPlantIndexByPlace(GameObject place)
    {
        string number = place.name.Replace("flower", "");
        int row = Int32.Parse(number.Split('_')[0]);
        int column = Int32.Parse(number.Split('_')[1]);

        return row * 3 + column;
    }

    public static PlantRepresentation getPlantByPlace(GameObject place)
    {
        return SaveLoadGameLogic.data.plants[getPlantIndexByPlace(place)];
    }

    public static void redrawPlants()
    {
        GameObject[] places = new GameObject[9]
        {
            GameObject.Find("flower0_0"),
            GameObject.Find("flower0_1"),
            GameObject.Find("flower0_2"),
            GameObject.Find("flower1_0"),
            GameObject.Find("flower1_1"),
            GameObject.Find("flower1_2"),
            GameObject.Find("flower2_0"),
            GameObject.Find("flower2_1"),
            GameObject.Find("flower2_2")
        };

        foreach (GameObject place in places)
        {
            if (place.transform.childCount > 0)
            {
                Destroy(place.transform.GetChild(0).gameObject);
            }
        }

        for (int i = 0; i < plants.Count(); i++)
        {
            GameObject parent = places[i];
            GameObject plant = Instantiate(getPrefabByPlant(plants[i]), parent.transform);
            plant.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            plant.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            plant.transform.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        }
    }

    public void addPlantToPlace(PlantRepresentation plant, GameObject place)
    {
        Instantiate(getPrefabByPlant(plant), place.transform);
    }

    public static int getCoinsOnClickByTag(string tag)
    {
        PlantRepresentation plantRepresentation = GetRepresentationByTag(tag);

        return getPlantByName(plantRepresentation.name).phases[plantRepresentation.phase].coinsPerClick;
    }

    public static PlantRepresentation GetRepresentationByTag(string tag)
    {
        string plantName = tag.Split('-')[0];
        int phase = Int32.Parse(tag.Split('-')[1]) - 1;

        PlantRepresentation plantRepresentation = new PlantRepresentation();
        plantRepresentation.name = plantName;
        plantRepresentation.phase = phase;

        return plantRepresentation;
    }



    void Start()
    {
        redrawPlants();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
