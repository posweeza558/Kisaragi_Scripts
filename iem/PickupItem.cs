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
                    // ��Ǩ�ͺ���������������� Hotbar �������
                    bool itemExistsInHotbar = hotbar.HotbarItems.Exists(item => item.itemName == placeholder.requiredItem);
                    if (itemExistsInHotbar)
                    {
                        Debug.Log("������������� Hotbar ����!");
                        return;
                    }

                    // ��Ժ�������� Hotbar
                    bool added = hotbar.AddItem(new ItemData { itemName = placeholder.requiredItem, itemSprite = placeholder.itemModel.GetComponent<ItemObject>().itemData.itemSprite });
                    if (added)
                    {
                        placeholder.PickUpItem(); // ����������᷹��������
                    }
                    else
                    {
                        Debug.Log("Hotbar �������!");
                    }
                }
            }
        }
    }
}

