using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    [SerializeField] private string _textToShow;
    private TextMeshProUGUI _text;

    private void Awake(){
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start(){
        _text.text = _textToShow + LevelData.GetCurrentLevel().ToString();
    }
}
