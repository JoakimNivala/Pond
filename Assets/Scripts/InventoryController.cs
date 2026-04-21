using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] ItemPrefabs;
    [SerializeField]
    private ItemDictionary itemDictionary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDictionary = FindFirstObjectByType<ItemDictionary>();
        //for (int i = 0; i < slotCount; i++)
        //{
        //    Slot slot = Instantiate(slotPrefab, InventoryPanel.transform).GetComponent<Slot>();
        //    if (i < ItemPrefabs.Length)
        //    {
        //        GameObject item = Instantiate(ItemPrefabs[i], slot.transform);
        //        item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        //        slot.currentItem = item;
        //    }
        //}
    }

    public bool Additem(GameObject itemPrefab)
    {
        foreach (Transform slotTransform in InventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                slot.currentItem = newItem;
                return true;
            }

        }
        return false;
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> InvData = new List<InventorySaveData>();
        foreach (Transform slotTransform in InventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                InvData.Add(new InventorySaveData { itemID = item.id, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }
        // Update is called once per frame
        return InvData;
    }
    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        foreach(Transform child in InventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < slotCount;  i++)
        {
            Instantiate(slotPrefab, InventoryPanel.transform);
        }
        foreach(InventorySaveData data in inventorySaveData)
            if(data.slotIndex < slotCount)
            {
                Slot slot = InventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    slot.currentItem = item;
                }
            }
    }
}
