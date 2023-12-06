using System;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    private void Awake(){
        SetLevelOpened(0);
    }

    public static bool GetLevelOpened(int index){
        return Convert.ToBoolean(PlayerPrefs.GetInt("Level Opened " + index.ToString(), 0));
    }

    public static void SetLevelOpened(int index){
        PlayerPrefs.SetInt("Level Opened " + index.ToString(), 1);
    }

    public static bool GetLevelWin(int index){
        return Convert.ToBoolean(PlayerPrefs.GetInt("Level Win " + index.ToString(), 0));
    }

    public static void SetLevelWin(int index){
        PlayerPrefs.SetInt("Level Win " + index.ToString(), 1);
    }

    public static int GetCurrentLevel(){
        return PlayerPrefs.GetInt("Current Level", 0);
    }

    public static void SetCurrentLevel(int index){
        PlayerPrefs.SetInt("Current Level", index);
    }
}
