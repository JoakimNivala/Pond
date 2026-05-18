using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "SaveData.json");
        inventoryController = FindFirstObjectByType<InventoryController>();

        LoadGame();
       
    }

    // Update is called once per frame

    public void SaveGame()
    {
        
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            ammo = FindFirstObjectByType<ShotgunLogic>().Ammo,
            extraAmmo = FindFirstObjectByType<ShotgunLogic>().ExtraAmmo,
            inventorySaveData = inventoryController.GetInventoryItems()
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;

            GameObject.FindFirstObjectByType<ShotgunLogic>().Ammo = saveData.ammo; 
            GameObject.FindFirstObjectByType<ShotgunLogic>().ExtraAmmo = saveData.extraAmmo;
            inventoryController.SetInventoryItems(saveData.inventorySaveData);
        }
        else
        {
            inventoryController.SetInventoryItems(new List<InventorySaveData>());
            Debug.Log("????");
            SaveGame();
        }
    }

}