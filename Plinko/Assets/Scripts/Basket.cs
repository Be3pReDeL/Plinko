using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private BasketType _basketType;
    public enum BasketType{
        green,
        red,
        supergame
    };

    public void GiveScore(float score){
        switch(_basketType){
            case BasketType.green:
                GameManager.Instance.AddScore(score * 10f);
                break;

            case BasketType.red:
                GameManager.Instance.AddScore(score * -5f);
                break;

            case BasketType.supergame:
                break;
        } 
    }
}
