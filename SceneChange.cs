using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public RepairSystem repairSystem;  // รับค่า RepairSystem
    public SelectableItem selectableItem;  // รับค่า SelectableItem

    void Start()
    {
        // หากไม่ได้ตั้งค่าใน Inspector ให้ลองหาจากออบเจ็กต์ที่มีชื่อ RepairSystem และ SelectableItem
        if (repairSystem == null)
            repairSystem = FindObjectOfType<RepairSystem>();
        if (selectableItem == null)
            selectableItem = FindObjectOfType<SelectableItem>();

        // ตรวจสอบว่าทั้งสองตัวแปรถูกอ้างอิงแล้วหรือยัง
        if (repairSystem == null || selectableItem == null)
        {
            Debug.LogError("❌ repairSystem หรือ SelectableItem ยังไม่ได้รับการอ้างอิง");
            return;
        }
    }

    void Update()
    {
        // ตรวจสอบการกดปุ่ม P
        if (Input.GetKeyDown(KeyCode.P))
        {
            // เช็คว่า ซ่อมครบหรือไม่
            if (repairSystem.isFullyRepaired)
            {
                // ถ้าซ่อมครบแล้ว เช็คว่าเลือกครบหรือไม่
                if (SelectableItem.isAllSelected)
                {
                    // ถ้าเงื่อนไขครบ (ซ่อมครบและเลือกครบ) ไปยังฉาก GoodEnd
                    SceneManager.LoadScene("GoodEnd");
                }
                else
                {
                    // ถ้าไม่ครบ (เก็บของไม่ครบ) ไปยังฉาก Ending
                    SceneManager.LoadScene("Ending");
                }
            }
            else
            {
                // ถ้าซ่อมไม่ครบ แค่แสดงข้อความ Debug.Log
                Debug.Log("❌ ซ่อมไม่ครบ ยังไม่สามารถไปยังฉากต่อไปได้");
            }
        }
    }
}
