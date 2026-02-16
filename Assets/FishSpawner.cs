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

        if (other.transform.tag == "Player")
        {

            for (int i = 0; i < 5; i++)
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
            for (int i=FishList.Count - 1; i >= 0; i--)
            {
                {
                    GameObject fish = FishList[i];
                    FishList.RemoveAt(i);
                    Destroy(fish);
                }
            }

            }
        }
    }

