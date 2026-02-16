using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    GameObject player;
    public Transform orientation;

    private float playerSpeed = 5.0f;
    private float gravityValue = 0.15f;
    private RaycastHit hit;
    private GameObject FishingRod;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    public bool grounded;
    public bool m_jumping;
    public Camera cam;
    public float jumpTime;
    CharacterController controller;
    private float ScrollSpeed = 1f;
    private Quaternion RodPosition;
    public float mouse;
    public bool activeRod = true;
    




    // Start is called before the first frame update
    void Start()
    {
      
        player = GetComponent<GameObject>();
        //player = GameObject.FindGameObjectWithTag("Player");
        //rb = player.GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        orientation = GetComponent<Transform>();
        cam = Camera.main;
        FishingRod = GameObject.Find("FishingRod");
        RodPosition = FishingRod.transform.rotation;
    }


    private void Update()
    {
        mouse += Input.mouseScrollDelta.y * ScrollSpeed;
        FishingRod.transform.localRotation = Quaternion.Euler(mouse, transform.rotation.y, transform.rotation.z);

        if (Input.GetKeyUp(KeyCode.Space) && m_jumping == false && grounded)
        {
            gravityValue = -gravityValue;
            m_jumping = true;
            grounded = false;

        }
        if (m_jumping)
        {
            jumpTime += Time.deltaTime;
            if (jumpTime >= 0.5f)
            {
                gravityValue = -gravityValue;
                m_jumping = false;
                jumpTime = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            activeRod = !activeRod;
            FishingRod.SetActive(activeRod);
        }
            
    }
    // Update is called once per frame
    void FixedUpdate()
    {
      
        playerGravity();
        MovePlayer();
        MyInput();
      





    }

    //    if(Input.GetKey(KeyCode.W))
    //    {
    //        rb.AddRelativeForce(1 * 10f, 0, 0);
    //    }

    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        rb.AddRelativeForce(1 * -10f, 0, 0);
    //    }

    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        rb.AddTorque(0, -10f * 1, 0 );
    //    }

    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        rb.AddTorque(0, 10f * 1, 0);
    //    }
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        rb.AddForce(0, 10f, 0);
    //    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        verticalInput = Input.GetAxisRaw("Vertical");

     
      
        
       
     
    }

    private void MovePlayer()
    {
        Vector3 cameraForward = cam.transform.forward;

        cameraForward.y = 0f;
        cameraForward = cameraForward.normalized;

        moveDirection = cameraForward * verticalInput + cam.transform.right * horizontalInput;
        moveDirection.y = 0f; 

        controller.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);

        Quaternion yRotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
        transform.rotation = yRotation;
       
    }

    private void playerGravity()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
          
            controller.Move(Vector3.down * gravityValue);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f))
            {
             grounded = true;
            }
        }
    }


}

