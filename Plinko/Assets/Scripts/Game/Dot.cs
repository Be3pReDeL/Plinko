using UnityEngine;
using System.Collections;
using CandyCoded.HapticFeedback;
public class Dot : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _redDotSprite;
    private Sprite _originalSprite;

    private const float SCOREGIVEN = 0.1f, TIMEDOTHAVETOBERED = 1f;

    private AudioSource _audioSource;

    private void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        _originalSprite = _spriteRenderer.sprite;
    }
    private void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.tag == "Ball"){
            _audioSource.Play();
            collision2D.gameObject.GetComponent<Ball>().Score += SCOREGIVEN;

            StartCoroutine(MakeDotRedForTime(TIMEDOTHAVETOBERED));
        }
    }

    private IEnumerator MakeDotRedForTime(float time){
        if(PlayerPrefs.GetInt("Vibrate", 0) == 1)
            HapticFeedback.LightFeedback();

        _spriteRenderer.sprite = _redDotSprite;

        yield return new WaitForSeconds(time);

        _spriteRenderer.sprite = _originalSprite;
    }
}
