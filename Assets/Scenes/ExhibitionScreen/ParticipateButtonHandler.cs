using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticipateButtonHandler : MonoBehaviour
{
    public void Click()
    {
        if (!OpenExhibition.CanParticipate())
        {
            return;
        }

        int probability = 0;

        if (SaveLoadGameLogic.data.plants.Count > 0)
        {
            float sumPhases = (from plant in SaveLoadGameLogic.data.plants select plant.phase).Sum();
            float maxPhases = 2 * SaveLoadGameLogic.data.plants.Count;
            probability = (int) ((sumPhases / maxPhases) * 100);
        }
        System.Random gen = new System.Random();
        int prob = gen.Next(100);
        
        if (prob < probability)
        {
            OpenExhibition.ShowWinWindow();
            SaveLoadGameLogic.data.money += 1000;
        } else
        {
            OpenExhibition.ShowLoseWindow();
        }

        SaveLoadGameLogic.data.dateTimeLastExhibition = DateTime.Now.ToString();

    }
}
