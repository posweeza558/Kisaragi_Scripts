using UnityEngine;

public class RepairableObject : MonoBehaviour
{
    [Tooltip("ใส่รหัสประจำวัตถุที่ต้องไม่ซ้ำกัน")]
    public string objectID; // รหัสประจำวัตถุ สำหรับใช้ในระบบเซฟ

    public bool hasGrate = false;
    public bool isGrateOpened = false;
    private bool isRepaired = false;

    [SerializeField] private GameObject requiredItem; // ไอเท็มที่ต้องใช้ซ่อม

    public GameObject GetRequiredItem() => requiredItem;

    public bool IsRepaired => isRepaired; // ตรวจสอบว่าวัตถุถูกซ่อมแล้วหรือยัง

    // ฟังก์ชันสำหรับเปิดตะแกรง
    public void OpenGrate()
    {
        if (!isRepaired)
        {
            isGrateOpened = true;
            Debug.Log("ตะแกรงเปิดแล้ว! สามารถซ่อมได้");
        }
    }

    // ฟังก์ชันสำหรับทำเครื่องหมายว่าสำเร็จแล้ว
    public void CompleteRepair()
    {
        if (!isRepaired)
        {
            isRepaired = true;
            Debug.Log("ซ่อมเสร็จแล้ว!");
            // บันทึก ID วัตถุที่ซ่อมแล้วในระบบเซฟ (ถ้ามี SaveLoadSystem)
            if (SaveLoadSystem.Instance != null && !SaveLoadSystem.Instance.repairedObjectIDs.Contains(objectID))
            {
                SaveLoadSystem.Instance.repairedObjectIDs.Add(objectID);
            }
        }
    }

    // เมธอดสำหรับตั้งค่าวัตถุให้อยู่ในสถานะซ่อมแล้ว (ใช้เมื่อโหลดเกม)
    public void SetAsRepaired()
    {
        isRepaired = true;
        Debug.Log($"{objectID} ตั้งค่าเป็นซ่อมแล้ว (Load)");
        // ปรับสถานะการแสดงผลของวัตถุ เช่น ปิด Collider หรือเปลี่ยนสี
    }
}
