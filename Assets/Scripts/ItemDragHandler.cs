using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Transform OriginalParent;
    CanvasGroup CanvasGroup;
    void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        OriginalParent = transform.parent;
        transform.SetParent(transform.root);
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1f;
        Slot Dropslot = eventData.pointerEnter?.GetComponent<Slot>();
        if (Dropslot == null)
        {
            GameObject item = eventData.pointerEnter;
            if (item != null)
            {
                Dropslot = item.GetComponentInParent<Slot>();
            }
        }
        Slot OriginalSlot = OriginalParent.GetComponent<Slot>();
        if (Dropslot != null)
        {

            if (Dropslot.currentItem != null)
            {

                Dropslot.currentItem.transform.SetParent(OriginalSlot.transform);
                OriginalSlot.currentItem = Dropslot.currentItem;
                Dropslot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

            }
            else
            {
                OriginalSlot.currentItem = null;
            }
            transform.SetParent(Dropslot.transform);
            Dropslot.currentItem = gameObject;
        }
        else
        {

            transform.SetParent(OriginalParent);
            if (!isWithInInventory(eventData.position))
            {
                Debug.Log("????");
                DropItem(OriginalSlot);
            }
            else
            {
                transform.SetParent(OriginalParent);
            }

        }
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;



        // Start is called once before the first execution of Update after the MonoBehaviour is created


        // Update is called once per frame

    }

    bool isWithInInventory(Vector3 mousePosition)
    {
       
        RectTransform inventoryPanel = OriginalParent.root.GetComponent<RectTransform>();
        if (EventSystem.current.IsPointerOverGameObject())
            {
            return true;
            }
        return false;
    }

    void DropItem(Slot originalSlot)
    {
      
        Destroy(originalSlot.currentItem);
        originalSlot.currentItem = null;
    }
}

