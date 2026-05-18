using NUnit.Framework;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent(typeof(Rigidbody))]
public class Pellet : MonoBehaviour
{

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    void Update()
    {
       
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            gameObject.SetActive(false);
        }
    }

   
}
