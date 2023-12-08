using UnityEngine;
using System.Collections;
public class Dot : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _redDotSprite;
    private Sprite _originalSprite;

    private const float SCOREGIVEN = 0.1f, TIMEDOTHAVETOBERED = 1f;

    private void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _originalSprite = _spriteRenderer.sprite;
    }
    private void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.tag == "Ball"){
            collision2D.gameObject.GetComponent<Ball>().Score += SCOREGIVEN;

            StartCoroutine(MakeDotRedForTime(TIMEDOTHAVETOBERED));
        }
    }

    private IEnumerator MakeDotRedForTime(float time){
        _spriteRenderer.sprite = _redDotSprite;

        yield return new WaitForSeconds(time);

        _spriteRenderer.sprite = _originalSprite;
    }
}
