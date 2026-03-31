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

    [SerializeField]
    private QuestManager questManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
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
            playerMovement.enabled = !isOn;
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
            for (int i = 0; i < Bobber.transform.childCount; i++)
            {
                questManager.UpdateFishCount();
                GameObject fish = Bobber.transform.GetChild(i).gameObject;
                Destroy(fish);

            }
            
            Bobber.GetComponent<Collider>().enabled = true;

        }
    }
}
