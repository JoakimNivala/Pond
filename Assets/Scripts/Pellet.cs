using NUnit.Framework;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Pellet : MonoBehaviour
{

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1.2f, ForceMode.Impulse);
        
    }

    IEnumerator DestroyBullet()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
