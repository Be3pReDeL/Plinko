using UnityEngine;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private GameObject _lock, _star;
    [SerializeField] private TextMeshProUGUI _levelText;

    public int _levelIndex = 0;

    [SerializeField] private LockedLevelScreenMenuController _lockedLevelScreenMenuController;

    private int _cost = 50;

    private void Start(){
        _lock.SetActive(!LevelData.GetLevelOpened(_levelIndex));
        _star.SetActive(LevelData.GetLevelWin(_levelIndex));

        _levelText.text = _levelIndex.ToString();
    }

    public void LoadGame(){
        LevelData.SetCurrentLevel(_levelIndex);

        LoadScene.LoadNextScene();
    }

    public void BuyLevel(){
        if(ValueManager.GetValueCount() >= _cost){
            ValueManager.ChangeValueCount(-_cost);

            _lock.SetActive(false);
            LevelData.SetLevelOpened(_levelIndex);
        }
        else
            _lockedLevelScreenMenuController.ShowCantBuyDialog(_cost - ValueManager.GetValueCount());
    }
}
