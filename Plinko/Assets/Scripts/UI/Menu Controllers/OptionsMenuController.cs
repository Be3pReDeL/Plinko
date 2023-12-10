using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Networking;
using UniWebViewNamespace;

public class OptionsMenuController : UIController
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private GameObject _privacyPolicyWebView;

    [SerializeField] private string _privacyPolicyURL;
    [SerializeField] private string _appID;

    private void OnEnable()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void Back()
    {
        for(int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Disappear(_duration);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void ShareButton(){
        new NativeShare()
			.SetSubject("Plinker!").SetText("Check this cool Plinker game!").SetUrl("https://apps.apple.com/us/app/id" + _appID)
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void RateUs(){
        Device.RequestStoreReview();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void PrivacyPolicy(){
        GameObject webView = Instantiate(_privacyPolicyWebView);

        UniWebView uniWebView = webView.GetComponent<UniWebView>();

        StartCoroutine(GetText(uniWebView));
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    private IEnumerator GetText(UniWebView uniWebView) {
        UnityWebRequest www = UnityWebRequest.Get(_privacyPolicyURL);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);

        else {
            string pageText = www.downloadHandler.text.Replace("\"", "");

            uniWebView.Show();
            uniWebView.Load(pageText);
        }
    }
}
