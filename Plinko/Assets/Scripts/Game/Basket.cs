using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private BasketType _basketType;
    [SerializeField] private GameObject _basketVFX;

    private AudioSource _audioSource;

    public enum BasketType{
        green,
        red,
        supergame
    };

    private void Awake(){
        _audioSource = GetComponent<AudioSource>();
    }

    public void GiveScore(float score){
        _audioSource.Play();
        Instantiate(_basketVFX, transform.position, Quaternion.identity);

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
