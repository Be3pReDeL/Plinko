using UnityEngine;
using TMPro;

public class ValueManager : MonoBehaviour
{
    private static TextMeshProUGUI _valueCountText;

    private static int _value;
    public static int Value{
        get { return _value; }
        set{
            _value = value;

            _valueCountText.text = Value.ToString();
        }
    }

    private void Awake(){
        _valueCountText = GetComponent<TextMeshProUGUI>();

        Value = PlayerPrefs.GetInt("Value", 0);
    }

    public static void ChangeValueCount(int amount){
        if((Value + amount) < 0)
            Value = 0;
        else
            Value += amount;

        PlayerPrefs.SetInt("Value", _value);
    }

    public static int GetValueCount(){
        return _value;
    }  
}
