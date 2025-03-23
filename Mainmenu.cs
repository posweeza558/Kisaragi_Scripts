using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    // เริ่มเกมใหม่
    public void Newgame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // ออกจากเกม
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}
