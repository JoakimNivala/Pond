using System.Collections;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public GameObject Barrel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Barrel = GameObject.Find("Barrel");
        transform.position = Barrel.transform.position;
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Rigidbody>().AddForce(Barrel.transform.forward * 100f);
        
    }

    IEnumerator DestroyBullet()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
