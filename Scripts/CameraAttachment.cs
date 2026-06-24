using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraAttachment : MonoBehaviour
{
    private bool isAttached;
    public Rigidbody2D mainSubject;


    private void Start()
    {
        if (mainSubject == false)
        {
            mainSubject = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        attach(Input.GetKeyDown("g"));
    }

    private void LateUpdate()
    {
        if (isAttached) {
            Vector3 vector = mainSubject.transform.position;
            vector.z = -8;
            transform.position = vector;
        }
    }

    private void attach(bool value)
    {
        if (value)
        {
            if (isAttached)
            {
                isAttached = false;
                transform.position = new Vector3Int(0, 0, -10);
            }
            else {
                isAttached = true;
            }
            
        }

    }


}
