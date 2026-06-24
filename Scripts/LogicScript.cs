using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))  //If you press R
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Load the current scene
        } 
    }

    
}
