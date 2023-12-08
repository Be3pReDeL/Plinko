using UnityEngine;

public class StartWebView : MonoBehaviour
{
    [SerializeField] private GameObject _webView;

    [SerializeField] private RectTransform _webViewRectTransform;

    private void Start(){
        GameObject webView = Instantiate(_webView);

        UniWebView uniWebView = webView.GetComponent<UniWebView>();

        uniWebView.ReferenceRectTransform = _webViewRectTransform;
        uniWebView.Show();
        uniWebView.Load(ChooseWhichToLoad.URLToShow);
    }
}
