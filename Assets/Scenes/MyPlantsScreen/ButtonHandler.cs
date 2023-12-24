using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void Right()
    {
        int maxPage = 1;

        while (maxPage * OpenMyPlants.plantsOnPage < SaveLoadGameLogic.data.plants.Count)
        {
            maxPage++;
        }


        if (maxPage <= OpenMyPlants.currentPage)
        {
            return;
        }

        OpenMyPlants.currentPage += 1;
        OpenMyPlants.RedrawPlants();
    }

    public void Left()
    {
        if (OpenMyPlants.currentPage > 1)
        {
            OpenMyPlants.currentPage -= 1;
            OpenMyPlants.RedrawPlants();
        }
    }
}
