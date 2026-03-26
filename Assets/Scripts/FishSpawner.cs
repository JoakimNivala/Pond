using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject Fish;
    public GameObject Water;
    public List<GameObject> FishList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Player"))
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject fish =  Instantiate(Fish, transform.position, Quaternion.identity);
                FishList.Add(fish);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            for (int i = FishList.Count - 1; i >= 0; i--)
            {
                GameObject Fish = FishList[i];

                if (Fish != null)
                {
                    if (Fish.transform.parent == null)
                    {
                        Destroy(Fish);
                    }
                }

                FishList.RemoveAt(i);
            }



        }
        }

  
    }

