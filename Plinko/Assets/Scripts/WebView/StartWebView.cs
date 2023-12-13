using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UniWebViewNamespace;

public class StartWebView : MonoBehaviour {
    [SerializeField] private RectTransform _webViewRectTransform;

    private string _adsID;

    private void Awake() {
        if (PlayerPrefs.GetInt("Got Ads ID?", 0) != 0) {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { _adsID = advertisingId; });
        }
    }

    private void Start() {
        if (Application.internetReachability != NetworkReachability.NotReachable) {
            if (PlayerPrefs.GetString("URL", string.Empty) != string.Empty)
                StartCoroutine(LoadWebViewWithDelay(1.5f, PlayerPrefs.GetString("URL")));

            else
                StartCoroutine(ProcessOfferLink(ChooseWhichToLoad.URLToShow));
        }

        else
            LoadScene.LoadNextScene();
    }

    private void ShowWebView(string url, string naming = ""){
        UniWebView.SetAllowInlinePlay(true);

        UniWebView webView = gameObject.AddComponent<UniWebView>();
        
        webView.ReferenceRectTransform = _webViewRectTransform;

        webView.SetToolbarDoneButtonText("");

        switch (naming) {
            case "0":
                webView.SetShowToolbar(true, false, false, true);
                break;

            default:
                webView.SetShowToolbar(false);
                break;
        }

        webView.OnShouldClose += (view) =>
        {
            return false;
        };

        webView.SetSupportMultipleWindows(true, true);
        webView.OnMultipleWindowOpened += (view, windowId) =>
        {
            webView.SetShowToolbar(true);

        };
        webView.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (naming) {
                case "0":
                    webView.SetShowToolbar(true, false, false, true);
                    break;

                default:
                    webView.SetShowToolbar(false);
                    break;
            }
        };

        webView.SetAllowBackForwardNavigationGestures(true);

        webView.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("URL", string.Empty) == string.Empty)
                PlayerPrefs.SetString("URL", url);
        };

        webView.Load(url);
        webView.Show();
    }

    private IEnumerator LoadWebViewWithDelay(float delay, string link){
        yield return new WaitForSeconds(delay);

        ShowWebView(link);
    }

    private IEnumerator ProcessOfferLink(string url) {
        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
                LoadScene.LoadNextScene();

            int delay = 3;

            while (PlayerPrefs.GetString("glrobo", "") == "" && delay > 0) {
                yield return new WaitForSeconds(1);
                delay--;
            }

            try {
                if (www.result == UnityWebRequest.Result.Success)
                    ShowWebView(ChooseWhichToLoad.URLToShow + "?idfa=" + _adsID + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                    
                else
                    LoadScene.LoadNextScene();
            }

            catch {
                LoadScene.LoadNextScene();
            }
        }
    }
}
