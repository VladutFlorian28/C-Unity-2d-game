using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GrabObject : MonoBehaviour
{

    private LayerMask LayerOfMovableObjects; // The layer of the interactable objects
    private GameObject player;

    private const float k_RightRadius = 0.4f;    //the radius around the point of interactive object detecion
    public bool IsGrabbing;                //used to remember if F key was pressed
    private Vector3 m_OriginPos;
    private GameObject m_interactiveObject;     //used to remember the interactive object 

    // Start is called before the first frame update
    void Start()
    {
        //assign default layer as layer of movable objects
        int layer = LayerMask.NameToLayer("Default");
        
        if (layer != -1)
        {
            LayerOfMovableObjects = 1 << layer;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if the F key is pressed 
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("you pressed F");
            //check if near an interactive object
            if (DetectInteractiveObjects())
            {
                //toggle the past value of F_keyPressed
                IsGrabbing = !IsGrabbing;

                if (IsGrabbing)
                {
                    Debug.Log("Activated F while near a movable object");
                    dockToObject();


                }
                else
                {
                    Debug.Log("Deactivated F while near a movable object");
                    unDockFromObject();
                }
            }
        }
    }

    private void dockToObject()
    {
        m_interactiveObject.transform.SetParent(player.transform);

        if (player.transform.localScale.x < 0)
        {
            m_interactiveObject.transform.position = player.transform.position - new Vector3(1.5f, 0f);
        }
        else
            m_interactiveObject.transform.position = player.transform.position + new Vector3(1.5f, 0f);
        
        Debug.Log("I am docked");
    }

    private void unDockFromObject()
    {
        m_interactiveObject.transform.SetParent(null);
    }




// return true if a circlecast to the RightCheck position hits any object inside the "Interactive Objects" layer
    private bool DetectInteractiveObjects()
    {
        //making the circlecast that detects objects near to it
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, k_RightRadius, LayerOfMovableObjects);
        
        //take each object detected inside the circlecast 
        foreach (Collider2D detectedObject in detectedObjects)
        {
            //check if the detected object is inside the "Interactive Objects" layer
            //if it is, store it inside m_interactiveObject 
            if (detectedObject.gameObject.CompareTag("Interactive Object"))
            {
                m_interactiveObject = detectedObject.gameObject;
                return true;
            }
        }

        return false;
    }
    
    
}

