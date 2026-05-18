using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ShotgunLogic : MonoBehaviour
{
    public List<GameObject> pellets;
    

    [SerializeField]
    public int Ammo;

    [SerializeField]
    private int FullBarrel;

    [SerializeField]
    public int ExtraAmmo;

    public bool isOn;

    [SerializeField]
    private GameObject ShotGun;

    [SerializeField]
    private List<Transform> PelletSpread;

    [SerializeField]
    private GameObject FishingRod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) && !FishingRod.activeInHierarchy)
        {
            isOn = !isOn;
            ShotGun.SetActive(isOn);
         
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && Ammo > 0 && isOn == true)
        {
            for (int i = 0; i < 8; i++)
            {
                if (pellets[i].activeInHierarchy)
                {
                    pellets[i].SetActive(false);
                }
                float randomX = Random.Range(-5f, 5f);
                float randomY = Random.Range(-5f, 5f);
                Quaternion randomRotation = Quaternion.Euler(randomX, randomY, 0f);
                Vector3 worldPos = PelletSpread[0].position;
                //no idea does this work
                pellets[i].SetActive(true);
                pellets[i].transform.position = worldPos;
                pellets[i].GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 10.4f, ForceMode.Impulse);
                pellets[i].transform.rotation = PelletSpread[0].rotation * randomRotation;
               


            }
            Ammo--;
        }
        if (Input.GetKeyUp(KeyCode.R) && ExtraAmmo > 0 && Ammo != 2)
        {
           int maxAmmo = FullBarrel - Ammo;
           int Reloading = Mathf.Min(maxAmmo, ExtraAmmo);

            Ammo += Reloading;
            ExtraAmmo -= Reloading;
          
        }
    }
}
