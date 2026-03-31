using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ShotgunLogic : MonoBehaviour
{
    public GameObject pellet;
    public float speed;

    [SerializeField]
    private int Ammo;

    [SerializeField]
    private int FullBarrel;

    [SerializeField]
    private int ExtraAmmo;

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
                Vector3 worldPos = PelletSpread[i].position;
                Instantiate(pellet, worldPos, PelletSpread[i].rotation);
               
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
