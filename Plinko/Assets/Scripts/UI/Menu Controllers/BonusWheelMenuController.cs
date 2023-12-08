using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;

public class BonusWheelMenuController : UIController
{
    [SerializeField] private float _duration = 1f;

    [SerializeField] private RectTransform _fortuneWheel;

    [SerializeField] private Vector2[] _winZones, _minusTenZones, _plusThirtyZones, _youLooseZones;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _plusThirtySound, _minusTenSound;

    [SerializeField] private BigSpinBonusMenuController _bigSpinBonusMenuController;

    private enum WinType{
        nextlevel,
        minusten,
        plusthirty,
        youloose
    };

    private WinType _winType;

    private void OnEnable()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }
    }

    private void Awake(){
        _audioSource = GetComponent<AudioSource>();
    }

    //Это ужасно, но я устал :(
    public void RotateWheel(){
        float randomAngle = Random.Range(360f, 3600f);

        _fortuneWheel.DOLocalRotate(new Vector3(0f, 0f, randomAngle), 5f, RotateMode.FastBeyond360).onComplete += () =>{
            switch(CalculateWinType(_fortuneWheel.eulerAngles.z)){
                case WinType.nextlevel: 
                    StartCoroutine(NextLevel());
                    break;

                case WinType.minusten:
                    StartCoroutine(MinusTen());
                    break;

                case WinType.plusthirty:
                    StartCoroutine(PlusThirty());
                    break;

                case WinType.youloose:
                    StartCoroutine(YouLoose());
                    break;
            }
        };
    }

    private WinType CalculateWinType(float angle){
        foreach(Vector2 zone in _winZones){
            if(angle >= zone.x && angle <= zone.y)
                return WinType.nextlevel;
        }

        foreach(Vector2 zone in _minusTenZones){
            if(angle >= zone.x && angle <= zone.y)
                return WinType.minusten;
        }

        foreach(Vector2 zone in _plusThirtyZones){
            if(angle >= zone.x && angle <= zone.y)
                return WinType.plusthirty;
        }

        foreach(Vector2 zone in _youLooseZones){
            if(angle >= zone.x && angle <= zone.y)
                return WinType.youloose;
        }

        return WinType.plusthirty;
    }

    private void Close(){
        for(int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Disappear(_duration);
        }
    }

    private IEnumerator NextLevel(){
        _audioSource.PlayOneShot(_plusThirtySound);
        LevelData.SetLevelOpened(LevelData.GetCurrentLevel() + 1);
        LevelData.SetLevelWin(LevelData.GetCurrentLevel());
        LevelData.SetCurrentLevel(LevelData.GetCurrentLevel() + 1);
        
        yield return new WaitForSeconds(3f);

        LoadScene.LoadSceneByRelativeIndex(0);
    }

    private IEnumerator MinusTen(){
        _audioSource.PlayOneShot(_minusTenSound);
        ValueManager.ChangeValueCount(-10);
        
        yield return new WaitForSeconds(3f);

        _bigSpinBonusMenuController.No();
        Close();
    }

    private IEnumerator PlusThirty(){
        _audioSource.PlayOneShot(_plusThirtySound);
        ValueManager.ChangeValueCount(30);
        
        yield return new WaitForSeconds(3f);

        _bigSpinBonusMenuController.No();
        Close();
    }

    private IEnumerator YouLoose(){
        _audioSource.PlayOneShot(_minusTenSound);

        yield return new WaitForSeconds(3f);

        LoadScene.LoadSceneByRelativeIndex(0);
    }
}
