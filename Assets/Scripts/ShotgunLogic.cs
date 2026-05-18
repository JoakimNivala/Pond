using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ShotgunLogic : MonoBehaviour
{
    public List<GameObject> pellets;

    private float randomX;
    private float randomY;

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
        randomX = Random.Range(-20f, 20f);
        randomY = Random.Range(-20f, 20f);
        if (Input.GetKeyUp(KeyCode.Alpha1) && !FishingRod.activeInHierarchy)
        {
            isOn = !isOn;
            ShotGun.SetActive(isOn);
         
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && Ammo > 0 && isOn == true)
        {
            for (int i = 0; i < 8; i++)
            {
               
                Rigidbody rb = pellets[i].GetComponent<Rigidbody>();
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

               
                Vector3 worldPos = PelletSpread[0].position;
                pellets[i].transform.position = worldPos;

                Quaternion spread = Quaternion.Euler(
                Random.Range(-randomX, randomX),   
                Random.Range(-randomY, randomY),
                0f
            );
                pellets[i].transform.rotation = PelletSpread[0].rotation * spread;

                //no idea does this work
                pellets[i].SetActive(true);
                
                pellets[i].GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 50.4f, ForceMode.Impulse);





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
