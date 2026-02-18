using UnityEngine;

public class FishingRod : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject _FishingRod;
    public bool isOn = true;
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = -2.0F;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _FishingRod = GameObject.Find("FishingRod");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            isOn = !isOn;
            _FishingRod.SetActive(isOn);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("LOL");
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            _FishingRod.transform.Rotate(v, h, 0);
        }

    }
}
