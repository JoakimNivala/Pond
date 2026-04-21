using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveData
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 playerPosition;
    public int ammo;
    public int extraAmmo;
    public int Fishes;
    public List<InventorySaveData> inventorySaveData;
   
}
