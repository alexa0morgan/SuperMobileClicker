using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenExhibition : MonoBehaviour
{
    static private int exhibitionIntervalInHours = 1;
    private bool isButtonShown = false;
    static DateTime lastExhibition;


    public void Scene1()
    {
        SceneManager.LoadScene("ExhibitionScreen");
        SceneManager.sceneLoaded += HideLoseWindowHandler;
        SceneManager.sceneLoaded += HideWinWindowHandler;
        string dateTimeLastExhibition = SaveLoadGameLogic.data.dateTimeLastExhibition;
        DateTime.TryParse(SaveLoadGameLogic.data.dateTimeLastExhibition, out lastExhibition);
    }

    public static bool CanParticipate()
    {
        return (DateTime.Now - lastExhibition).TotalHours >= exhibitionIntervalInHours;
    }

    public void Update()
    {
        GameObject[] timers = GameObject.FindGameObjectsWithTag("ExhibitionTime");
        if (timers.Length == 0)
        {
            return;
        }

        GameObject timer = timers[0];
        var text = timer.GetComponent<TextMeshProUGUI>();

        if (text != null)
        {
            TimeSpan interval = lastExhibition.AddHours(1) - DateTime.Now;
            if (interval <= TimeSpan.Zero)
            {
                text.text = "00:00:00";
            }
            else
            {
                text.text = interval.Hours.ToString() + ":" + interval.Minutes.ToString() + ":" + interval.Seconds.ToString();
            }
        }

        if (CanParticipate() && !isButtonShown)
        {
            GameObject.FindGameObjectWithTag("ExhibitionButtonInactive").transform.localScale = Vector3.zero;
            isButtonShown = true;
        } else if (!CanParticipate() && isButtonShown)
        {
            GameObject.FindGameObjectWithTag("ExhibitionButtonInactive").transform.localScale = Vector3.one;
            isButtonShown = false;
        }
    }

    public static void HideLoseWindow()
    {
        GameObject loseWindow = GameObject.FindGameObjectWithTag("lose window");
        loseWindow.transform.localScale = Vector3.zero;
    }

    public static void HideWinWindow()
    {
        GameObject winWindow = GameObject.FindGameObjectWithTag("win window");
        winWindow.transform.localScale = Vector3.zero;
    }

    public static void ShowLoseWindow()
    {
        GameObject loseWindow = GameObject.FindGameObjectWithTag("lose window");
        loseWindow.transform.localScale = Vector3.one;
    }

    public static void ShowWinWindow()
    {
        GameObject winWindow = GameObject.FindGameObjectWithTag("win window");
        winWindow.transform.localScale = Vector3.one;
    }
    public static void ShowLoseWindowHandler(Scene scene, LoadSceneMode mode)
    {
        ShowLoseWindow();
    }
    public static void ShowWinWindowHandler(Scene scene, LoadSceneMode mode)
    {
        ShowWinWindow();
    }
    public static void HideLoseWindowHandler(Scene scene, LoadSceneMode mode)
    {
        HideLoseWindow();
    }
    public static void HideWinWindowHandler(Scene scene, LoadSceneMode mode)
    {
        HideWinWindow();
    }
}