using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveLoadGameLogic : MonoBehaviour
{
    public static Data data = new Data();
    private static bool isLoaded = false;
    private const string SaveFileName = "save.json";

    void NewGame()
    {
        data.money = 1000;
        data.plants = new List<PlantRepresentation>();
        MyPlantsLogic.plants = data.plants;
        data.dateTimeLastExhibition = DateTime.Now.ToString();
        data.dateTimeLastWatering = DateTime.Now.ToString();
        data.dateTimeLastFertilizer = DateTime.Now.ToString();
        data.dateTimeLastLamp = DateTime.Now.ToString();
        data.dateTimeQuit = DateTime.Now.ToString();
}

    void Load()
    {
        string savePath = Application.persistentDataPath + "/" + SaveFileName;
        try
        {
            string saveData = System.IO.File.ReadAllText(savePath);
            data = JsonUtility.FromJson<Data>(saveData);
            MyPlantsLogic.plants = data.plants;

            AddOfflineMoney();
        }
        catch
        {
            NewGame();
        }
    }

    public void AddOfflineMoney()
    {
        DateTime dateTimeQuit;
        DateTime.TryParse(data.dateTimeQuit, out dateTimeQuit);

        data.money += MyPlantsLogic.getCurrentCoinsPerSecond() * (int) (DateTime.Now - dateTimeQuit).TotalSeconds;
    }

    void Save()
    {
        string savePath = Application.persistentDataPath + "/" + SaveFileName;
        string saveData = JsonUtility.ToJson(data);
        data.plants = MyPlantsLogic.plants;

        System.IO.File.WriteAllText(savePath, saveData);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isLoaded)
        {
            Load();
            isLoaded = true;
        }
    }

    private void OnApplicationQuit()
    {
        data.dateTimeQuit = DateTime.Now.ToString();
        Save();
    }

    [Serializable]
    public class Data
    {
        public int money;
        public string dateTimeLastExhibition;
        public string dateTimeLastWatering;
        public string dateTimeLastFertilizer;
        public string dateTimeLastLamp;
        public string dateTimeQuit;
        public List<PlantRepresentation> plants;
    }
}
