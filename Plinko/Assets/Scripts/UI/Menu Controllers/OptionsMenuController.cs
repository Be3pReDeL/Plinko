using UnityEngine;
using UnityEngine.iOS;

public class OptionsMenuController : UIController
{
    [SerializeField] private float _duration = 1f;

    [SerializeField] private string _shareURL = "https://google.com";
    [SerializeField] private GameObject _privacyPolicyWebView;

    private void OnEnable()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }
    }

    public void Back()
    {
        for(int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Disappear(_duration);
        }
    }

    public void ShareButton(){
        new NativeShare()
			.SetSubject("Plinker!").SetText("Check this cool Plinker game! " + _shareURL).SetUrl(_shareURL)
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
    }

    public void PrivacyPolicy(){
        Instantiate(_privacyPolicyWebView);
    }

    public void RateUs(){
        Device.RequestStoreReview();
    }
}
