using UnityEngine;

public class FishingRod : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject _FishingRod;
    public bool isOn = false;
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = -2.0F;
    public GameObject Bobber;
    public int FishCount;

    [SerializeField]
    private GameObject ShotGun;

    [SerializeField]
    private MovementScript playerMovement;

    private InventoryController inventoryController;

    [SerializeField]
    private GameObject FishInventoryObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
    }
    // Update is called once per frame
    void Update()
    {
        _FishingRod.transform.position = transform.position;
       
        if (Input.GetKeyUp(KeyCode.Alpha2) && !ShotGun.activeInHierarchy)
        {
            isOn = !isOn;
            _FishingRod.SetActive(isOn);
            CheckFishes();
            _FishingRod.transform.rotation = transform.rotation;
            playerMovement.activeRod = isOn;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            _FishingRod.transform.Rotate(v, h, 0);
            _FishingRod.transform.rotation = transform.rotation;
        }

    }

    private void CheckFishes()
    {
        if (Bobber.transform.childCount > 0)
        {
            Debug.Log("HELLO?");
            
                GameObject fish = Bobber.transform.GetChild(0).gameObject;
                Item item = FishInventoryObj.GetComponent<Item>();
                bool itemAdded = inventoryController.Additem(item.gameObject);
                Destroy(fish);

            
            
            Bobber.GetComponent<Collider>().enabled = true;

        }
    }
}
