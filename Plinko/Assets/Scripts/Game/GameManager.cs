using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _plinkButton;
    [SerializeField] private TextMeshProUGUI _currentScoreText, _scoredScoreText;
    [SerializeField] private TMP_FontAsset _greenFont, _redFont;
    [SerializeField] private GameObject _ball;
    [SerializeField] private Transform _portal;

    [SerializeField] private int[] _levels;

    [SerializeField] private GameObject _winMenu;

    private const float _DELTAX = 0.6f;

    private float _currentScore = 0;
    
    private bool _startCountTime = false;
    private bool StartCountTime{
        get { return _startCountTime; }
        set{
            _startCountTime = value;

            _plinkButton.interactable = !_startCountTime;
        }
    }

    [SerializeField ]private float _timeBetweenPlinks = 0.5f;
    private float timer = 0f;

    private void Awake(){
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

        _slider.maxValue = _levels[LevelData.GetCurrentLevel()];
    }

    private void Update(){
        if(StartCountTime){
            if(timer < _timeBetweenPlinks)
                timer += Time.deltaTime;

            else{
                StartCountTime = false;
                timer = 0;
            }
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void PlinkButton(){
        GameObject newBall = Instantiate(_ball, _portal.position + new Vector3(Random.Range(-_DELTAX, _DELTAX), 0f, 0f), Quaternion.identity);

        StartCountTime = true;
    }

    public void AddScore(float amount){
        if((_currentScore + amount) < 0){
            _currentScore = 0;
            _currentScoreText.text = _currentScore.ToString();

            _scoredScoreText.font = Mathf.Sign(amount) < 0 ? _redFont : _greenFont;
            _scoredScoreText.text = Mathf.Sign(amount) < 0 ? Mathf.Round(amount).ToString() : "+" + " " + Mathf.Round(amount).ToString();

            _slider.value = _currentScore;
        }
            
        else{
            _currentScore += Mathf.Round(amount);

            _currentScoreText.text = _currentScore.ToString();

            _scoredScoreText.font = Mathf.Sign(amount) < 0 ? _redFont : _greenFont;
            _scoredScoreText.text = Mathf.Sign(amount) < 0 ? Mathf.Round(amount).ToString() : "+" + " " + Mathf.Round(amount).ToString();

            _slider.value = _currentScore;

            if(_currentScore >= _levels[LevelData.GetCurrentLevel()])
                _winMenu.SetActive(true);
        }
    }
}
