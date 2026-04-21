using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    // Start is called before the fir
    // st frame update

    //Rigidbody rb

    public Transform orientation;
    public float sensitivity;
    public GameObject Player;

    public float sensX;
    public float sensY;

    private float yRotation;
    private float xRotation;
    public Vector3 CameraOffset;

    [SerializeField]
    private GameObject Menu;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        

    }
  

    // Update is called once per frame
    void Update()
    {
        CameraOffset = Player.transform.position - new Vector3(0, 0, 0.3f);
        transform.position = CameraOffset;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            return;
        }
        if (Menu.activeSelf)
        {
            return;
        }
        MouseMovement();
    }

    public void MouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX;

        float mouseY = Input.GetAxis("Mouse Y")  * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
  
}
