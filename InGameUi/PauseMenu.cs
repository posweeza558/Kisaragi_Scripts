using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; // ใช้ Stack

public class PauseMenu : MonoBehaviour
{
    public GameObject[] menuPanels; // ลาก Panels ที่ต้องการใส่ใน Inspector
    private Stack<GameObject> menuStack = new Stack<GameObject>(); // เก็บเมนูที่เปิดอยู่

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // กด ESC
        {
            if (menuStack.Count > 0)
            {
                CloseLastMenu(); // ปิดเมนูล่าสุด
            }
            else
            {
                OpenMenu(0); // ถ้าไม่มีเมนูเปิด ให้เปิด Pause Menu
            }
        }
    }

    public void OpenMenu(int index)
    {
        if (index >= 0 && index < menuPanels.Length)
        {
            GameObject menu = menuPanels[index];

            if (!menuStack.Contains(menu)) // ป้องกันเปิดซ้ำ
            {
                menu.SetActive(true);
                menuStack.Push(menu);
                Time.timeScale = 0; // หยุดเกม
            }
        }
    }

    public void CloseLastMenu()
    {
        if (menuStack.Count > 0)
        {
            GameObject lastMenu = menuStack.Pop(); // ดึงเมนูล่าสุดออกมา
            lastMenu.SetActive(false); // ปิดเมนูที่เปิดล่าสุด

            if (menuStack.Count == 0)
            {
                Time.timeScale = 1; // ถ้าไม่มีเมนูเหลือ กลับมาเล่นเกม
            }
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainmenu");
    }
}
