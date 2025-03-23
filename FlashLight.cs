using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight; // GameObject ของไฟฉาย
    public AudioSource turnOnSound; // เสียงเมื่อเปิดไฟฉาย
    public AudioSource turnOffSound; // เสียงเมื่อปิดไฟฉาย

    private bool isOn = false; // สถานะของไฟฉาย (เปิด/ปิด)

    void Start()
    {
        // ตั้งค่าเริ่มต้นให้ไฟฉายปิดอยู่
        if (flashlight != null)
        {
            flashlight.SetActive(false);
        }
        else
        {
            Debug.LogError("Flashlight GameObject is not assigned!");
        }
    }

    void Update()
    {
        // ตรวจสอบการกดปุ่ม F เพื่อเปิด/ปิดไฟฉาย
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Flashlight is " + (isOn ? "ON" : "OFF"));
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        // สลับสถานะไฟฉาย
        isOn = !isOn;

        // เปิด/ปิดไฟฉาย
        if (flashlight != null)
        {
            flashlight.SetActive(isOn);
        }

        // เล่นเสียงเมื่อเปิด/ปิดไฟฉาย
        if (isOn && turnOnSound != null)
        {
            turnOnSound.Play();
        }
        else if (!isOn && turnOffSound != null)
        {
            turnOffSound.Play();
        }

        // แสดงสถานะใน Console
        Debug.Log("Flashlight is " + (isOn ? "ON" : "OFF"));
    }
}