using UnityEngine;

public class FishingRod : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject _FishingRod;
    public bool isOn = true;
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = -2.0F;
    public GameObject Bobber;
    public int FishCount;
    private Quaternion StartingRot;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _FishingRod = GameObject.Find("FishingRod");
        StartingRot = _FishingRod.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        _FishingRod.transform.position = transform.position;
        if (Input.GetKeyUp(KeyCode.E))
        {
            isOn = !isOn;
            _FishingRod.SetActive(isOn);
            CheckFishes();
            _FishingRod.transform.rotation = StartingRot;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            _FishingRod.transform.Rotate(v, h, 0);
        }

    }

    private void CheckFishes()
    {
        if (Bobber.transform.childCount > 0)
        {
            for (int i = 0; i < Bobber.transform.childCount; i++)
            {
                FishCount++;
                GameObject fish = Bobber.transform.GetChild(i).gameObject;
                Destroy(fish);

            }
            
            Bobber.GetComponent<Collider>().enabled = true;

        }
    }
}
