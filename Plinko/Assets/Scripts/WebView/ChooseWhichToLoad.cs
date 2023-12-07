using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ChooseWhichToLoad : MonoBehaviour
{
    //https://dev-rcovvc6yf0j9213.api.raw-labs.com/applinks?key=privacy%20policy
    public static int SceneIndex {get; private set;} = 2;
    public static string URLToShow {get; private set;}

    [SerializeField] private string _url = "https://dev-rcovvc6yf0j9213.api.raw-labs.com/applinks?key=game";

    private void Start() {
        StartCoroutine(GetText());
    }
 
    private IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get(_url);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);

        else {
            string pageText = www.downloadHandler.text.Replace("\"", "");
            Debug.Log(pageText);

            if(pageText == "stop"){
                SceneIndex = 2;

                Destroy(this);
            }

            else{
                SceneIndex = 1;

                URLToShow = pageText;
            }
        }
    }
}
