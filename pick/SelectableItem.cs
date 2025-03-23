using UnityEngine;
using TMPro;

public class SelectableItem : MonoBehaviour
{
    public string objectID; // กำหนด ID ให้แต่ละไอเท็มไม่ซ้ำกัน
    private static int selectedCount = 0;
    public static int maxSelected = 6;
    public static bool isAllSelected = false;
    private bool isSelected = false;

    public static TextMeshProUGUI countText; // กำหนด UI TextMeshPro ผ่าน Inspector หรือโค้ดอื่น

    public void Select()
    {
        if (isSelected || isAllSelected) return;

        isSelected = true;
        selectedCount++;
        gameObject.SetActive(false);
        UpdateUI();
        Debug.Log($"OBJECT : {selectedCount}/{maxSelected}");

        if (selectedCount >= maxSelected)
        {
            isAllSelected = true;
            Debug.Log("🎉 ครบจำนวนที่กำหนดแล้ว!");
        }

        // บันทึก ID ของไอเท็มที่เลือกแล้วในระบบเซฟ
        if (!SaveLoadSystem.Instance.collectedItemIDs.Contains(objectID))
        {
            SaveLoadSystem.Instance.collectedItemIDs.Add(objectID);
        }
    }

    public static void UpdateUI()
    {
        if (countText != null)
        {
            countText.text = $"OBJECT : {selectedCount}/{maxSelected}";
        }
    }

    // เมธอดนี้ใช้เรียกเมื่อโหลดเกมแล้วพบว่าไอเท็มนี้ถูกเลือกไปแล้ว
    public void SetAsCollected()
    {
        gameObject.SetActive(false);
    }
}
