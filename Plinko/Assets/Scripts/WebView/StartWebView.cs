using UnityEngine;

public class StartWebView : MonoBehaviour
{
    [SerializeField] private GameObject _webView;

    private void Start(){
        GameObject webView = Instantiate(_webView);

        UniWebView uniWebView = webView.GetComponent<UniWebView>();

        uniWebView.Show();
        uniWebView.Load(ChooseWhichToLoad.URLToShow);
    }
}
