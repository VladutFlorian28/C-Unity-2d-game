using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StarPointScript : MonoBehaviour
{
    private ShowScore addToScore;
    private CapsuleCollider2D player;

    private void Start()
    {
        
        addToScore = GameObject.FindGameObjectWithTag("Score").GetComponent<ShowScore>();       
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0 && player.transform == collision.transform)
        {
            Destroy(gameObject);
            addToScore.addStar(1);

        }
    }
}
