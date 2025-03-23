using UnityEngine;
using TMPro;

public class SelectSystem : MonoBehaviour
{
    public float interactDistance = 10f; // ระยะที่สามารถเลือกได้
    public TextMeshProUGUI countTextUI; // UI แสดงจำนวนที่เลือกแล้ว

    void Start()
    {
        SelectableItem.countText = countTextUI; // เชื่อม UI กับ SelectableItem
        SelectableItem.UpdateUI(); // อัปเดตค่าเริ่มต้น
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TrySelect();
        }
    }

    void TrySelect()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance))
        {
            // เลือกจาก root หรือ parent
            SelectableItem item = hit.collider.GetComponentInParent<SelectableItem>();

            if (item != null)
            {
                Debug.Log($"🔍 เจอวัตถุที่เลือกได้: {item.gameObject.name}");
                item.Select();
            }
            else
            {
                Debug.Log("❌ วัตถุนี้ไม่สามารถเลือกได้");
            }
        }
        else
        {
            Debug.Log("❌ ไม่พบวัตถุในระยะ");
        }
    }
}