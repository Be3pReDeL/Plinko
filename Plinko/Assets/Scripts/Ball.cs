using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Score {get; set;} 

    private void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.tag == "Basket"){
            collision2D.gameObject.GetComponent<Basket>().GiveScore(Score);

            Destroy(gameObject);
        }
    }
}
