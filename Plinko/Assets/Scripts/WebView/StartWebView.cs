using UnityEngine;
using OPS;
using UniWebViewNamespace;

[OPS.Obfuscator.Attribute.DoNotObfuscateClass]
public class StartWebView : MonoBehaviour
{
    [SerializeField] private GameObject _webView;

    [SerializeField] private RectTransform _webViewRectTransform;

    UniWebView uniWebView;
    GameObject webView;

    private void Start(){
        webView = Instantiate(_webView);

        uniWebView = webView.GetComponent<UniWebView>();

        uniWebView.ReferenceRectTransform = _webViewRectTransform;
        Invoke(nameof(ShowWebView), 0.3f);
        
    }

    private void ShowWebView(){
        uniWebView.Show();
        uniWebView.Load(ChooseWhichToLoad.URLToShow);
    }
}
