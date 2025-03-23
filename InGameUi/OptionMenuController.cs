using UnityEngine;
// หากต้องการโหลดฉาก MainMenu ให้เปิดคอมเมนต์และเพิ่ม using UnityEngine.SceneManagement;
// using UnityEngine.SceneManagement;

public class OptionMenuController : MonoBehaviour
{
    // กำหนด GameObject ของแต่ละ Panel ใน Inspector

    public GameObject mainMenuPanel;
    public GameObject optionsMenu;       // หน้า Options หลัก
    public GameObject keyMappingMenu;    // หน้า Key Mapping

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ถ้าอยู่ใน Key Mapping หรือ Sound Settings ให้กลับไปที่ Options Menu
            if (keyMappingMenu.activeSelf || optionsMenu.activeSelf)
            {
                keyMappingMenu.SetActive(false);
                optionsMenu.SetActive(false);
                mainMenuPanel.SetActive(true);
            }
        }
    }
}
