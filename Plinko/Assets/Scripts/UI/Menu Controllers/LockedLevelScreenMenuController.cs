using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class LockedLevelScreenMenuController : UIController
{
    [SerializeField] private float _duration = 1f;

    [SerializeField] private GameObject _lockedLevelScreen;
    [SerializeField] private TextMeshProUGUI _lockedLevelScreenCost;

    private void OnEnable()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OKButton()
    {
        for(int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Disappear(_duration);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void ShowCantBuyDialog(int cost){
        _lockedLevelScreen.SetActive(true);

        _lockedLevelScreenCost.text = cost.ToString();
    }
}
