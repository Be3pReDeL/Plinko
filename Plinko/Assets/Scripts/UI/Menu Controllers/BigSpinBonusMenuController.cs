using UnityEngine;
using System.Collections.Generic;

public class BigSpinBonusMenuController : UIController
{
    [SerializeField] private float _duration = 1f;

    private void OnEnable()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void No()
    {
        for(int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Disappear(_duration);
        }
    }
}
