using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public float pickupRange = 2.0f;
    public LayerMask itemLayer;
    public HotbarSystem hotbar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange, itemLayer))
            {
                Placeholder placeholder = hit.collider.GetComponent<Placeholder>();
                if (placeholder != null)
                {
                    // ตรวจสอบว่าไอเท็มนี้อยู่ใน Hotbar หรือไม่
                    bool itemExistsInHotbar = hotbar.HotbarItems.Exists(item => item.itemName == placeholder.requiredItem);
                    if (itemExistsInHotbar)
                    {
                        Debug.Log("ไอเท็มนี้อยู่ใน Hotbar แล้ว!");
                        return;
                    }

                    // หยิบไอเท็มเข้า Hotbar
                    bool added = hotbar.AddItem(new ItemData { itemName = placeholder.requiredItem, itemSprite = placeholder.itemModel.GetComponent<ItemObject>().itemData.itemSprite });
                    if (added)
                    {
                        placeholder.PickUpItem(); // ทำให้โปร่งใสแทนที่จะหายไป
                    }
                    else
                    {
                        Debug.Log("Hotbar เต็มแล้ว!");
                    }
                }
            }
        }
    }
}

