using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MenuCanvas;

    [SerializeField]
    private GameObject QuestUI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!MenuCanvas.activeSelf && PauseController.isGamePaused)
            {
                return;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            MenuCanvas.SetActive(!MenuCanvas.activeSelf);
            PauseController.isPaused(MenuCanvas.activeSelf);
            QuestUI.SetActive(!QuestUI.activeSelf);
            if (!MenuCanvas.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        
    }
}
