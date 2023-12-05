using UnityEngine;
using System.Collections.Generic;

public class WinMenuController : UIController
{
    [SerializeField] private float _duration = 1f;

    public void Start()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }
    }
}
