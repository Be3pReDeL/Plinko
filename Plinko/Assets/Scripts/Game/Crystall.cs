using UnityEngine;
using System.Collections;

public class Crystall : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    [SerializeField] private GameObject _shineVFX, _collectVFX;

    private void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.tag == "Ball"){
            StartCoroutine(DestroyAfterTime());

            _audioSource.Play();

            ValueManager.ChangeValueCount(2);
        }
    }

    private IEnumerator DestroyAfterTime(){
        _spriteRenderer.enabled = false;
        _shineVFX.SetActive(false);
        Instantiate(_collectVFX, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
