using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CrystallsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _dots;

    [SerializeField] private GameObject[] _crystalls;

    private List<GameObject> _spawnedCrystalls = new List<GameObject>();

    private void Start(){
        StartCoroutine(GenerateLooped());
    }

    private IEnumerator GenerateLooped(){
        while(true){
            int randomCount = Random.Range(0, 6);

            for(int i = 0; i < randomCount; i++){
                int randomIndex = Random.Range(0, _dots.Length);

                GameObject newCrystall = Instantiate(_crystalls[Random.Range(0, _crystalls.Length)], _dots[randomIndex].transform.position, Quaternion.identity);
                _dots[randomIndex].SetActive(false);

                _spawnedCrystalls.Add(newCrystall);
            }

            yield return new WaitForSeconds(10f);

            DestroyAllCrystalls();
            SetAllDotsActive();
        }
    }

    private void SetAllDotsActive(){
        for(int i = 0; i < _dots.Length; i++)
            _dots[i].SetActive(true);
    }

    private void DestroyAllCrystalls(){
        for(int i = 0; i < _spawnedCrystalls.Count; i++)
            Destroy(_spawnedCrystalls[i]);

        _spawnedCrystalls.Clear();
    }
}
