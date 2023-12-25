using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetScore : MonoBehaviour
{

    private void Start()
    {
        RedrawScore();
        SetScoreOnUpdate();
    }

    public static void RedrawScore()
    {
        GameObject nameText = GameObject.FindGameObjectWithTag("Score");
        if (nameText == null) return;
        var text = nameText.GetComponent<TextMeshProUGUI>();

        if (text != null)
        {
            string currentMoney = SaveLoadGameLogic.data.money.ToString();
            text.text = currentMoney;
        }
    }

    public void UpdateScore()
    {
        SaveLoadGameLogic.data.money += MyPlantsLogic.getCurrentCoinsPerSecond();
        RedrawScore();
    }

    public void SetScoreOnUpdate()
    {
        InvokeRepeating("UpdateScore", 1, 1);
    }
}
