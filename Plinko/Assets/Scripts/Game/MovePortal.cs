using UnityEngine;
using DG;
using DG.Tweening;

public class MovePortal : MonoBehaviour
{
    private float _speed = 0.3f;
    [SerializeField] private float _maxX = 2f;

    private void Start(){
        transform.position = new Vector3(-_maxX, 3.25f, 0f);

        transform.DOMoveX(_maxX, 6f - _speed * LevelData.GetCurrentLevel()).SetLoops(-1, LoopType.Yoyo);
    }
}
