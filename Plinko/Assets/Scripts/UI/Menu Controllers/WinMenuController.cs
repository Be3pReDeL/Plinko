using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class WinMenuController : UIController
{
    [SerializeField] private float _duration = 1f;

    [SerializeField] private TextMeshProUGUI _levelButton;

    private void OnEnable()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }

        if(LevelData.GetCurrentLevel() == 19)
            _levelButton.text = LevelData.GetCurrentLevel().ToString();
        else
            _levelButton.text = (LevelData.GetCurrentLevel() + 1).ToString();
    }

    public void HomeButton(){
        LevelData.SetLevelOpened(LevelData.GetCurrentLevel() + 1);
        LevelData.SetLevelWin(LevelData.GetCurrentLevel());

        LoadScene.LoadPreviousScene();
    }

    public void NextLevelButton(){
        if(LevelData.GetCurrentLevel() == 19){
            HomeButton();
        }

        else{
            LevelData.SetLevelOpened(LevelData.GetCurrentLevel() + 1);
            LevelData.SetLevelWin(LevelData.GetCurrentLevel());

            LevelData.SetCurrentLevel(LevelData.GetCurrentLevel() + 1);
            
            LoadScene.LoadSceneByRelativeIndex(0);
        }
    }
}
