using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class ValueManager : MonoBehaviour
{
    private TextMeshProUGUI _valueCountText;

    private static int _value;
    public int Value{
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
        _value += amount;

        PlayerPrefs.SetInt("Value", _value);
    }

    public static int GetValueCount(){
        return _value;
    }  
}
