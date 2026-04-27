using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : MonoBehaviour
{
    public GameObject _FlashLight;
    public bool isOn = true;
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = -2.0F;

    [SerializeField]
    private GameObject Camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _FlashLight = GameObject.Find("FlashLight");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            isOn = !isOn;
            _FlashLight.SetActive(isOn);
            _FlashLight.transform.rotation = Camera.transform.rotation;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
           
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            _FlashLight.transform.Rotate(v, h, 0);
        }
        
    }
}
