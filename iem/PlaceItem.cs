using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    public float placeRange = 2.0f;
    public LayerMask placeholderLayer;
    public HotbarSystem hotbar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, placeRange, placeholderLayer))
            {
                Placeholder placeholder = hit.collider.GetComponent<Placeholder>();
                if (placeholder != null && hotbar.selectedIndex >= 0 && hotbar.selectedIndex < hotbar.HotbarItems.Count)
                {
                    ItemData selectedItem = hotbar.HotbarItems[hotbar.selectedIndex];
                    if (selectedItem != null && selectedItem.itemName == placeholder.requiredItem)
                    {
                        // วางไอเท็ม
                        placeholder.ShowItem(); // แสดงไอเท็มปกติ
                        hotbar.RemoveItem(hotbar.selectedIndex); // เอาไอเท็มออกจาก Hotbar
                        hotbar.DeselectItem(); // ยกเลิกการเลือกไอเท็ม
                        Debug.Log("วางไอเท็มสำเร็จ: " + placeholder.requiredItem);
                    }
                    else
                    {
                        Debug.Log("ไอเท็มไม่ตรงกับที่ Placeholder ต้องการ");
                    }
                }
            }
        }
    }
}