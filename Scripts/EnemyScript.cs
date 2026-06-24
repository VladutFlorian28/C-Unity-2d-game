using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D rb;
    private Transform currentPoint;

    public float speed;
    private bool isSleeping = false;
    private float sleepDuration = 3f;
    private Vector2 sleepPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSleeping)
        {
            PatrolMovement();
        }
        else
        {
            transform.position = sleepPosition;
        }
    }

    public void PatrolMovement()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }
    
    public void GoToSleep()
    {
        if (!isSleeping)
        {
            isSleeping = true;
            sleepPosition = transform.position; // Capture the current position
            StartCoroutine(SleepCoroutine());
        }
    }
    
    private System.Collections.IEnumerator SleepCoroutine()
    {
        yield return new WaitForSeconds(sleepDuration);
        isSleeping = false;
    }
}
