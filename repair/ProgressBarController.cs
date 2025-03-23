using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Image progressBar; // Radial Progress Bar
    public CanvasGroup progressCanvas; // Canvas Group สำหรับ fade in/out

    private bool isActive = false;

    void Start()
    {
        progressCanvas.alpha = 0f; // ซ่อน Progress Bar ตั้งแต่เริ่มเกม
        progressBar.gameObject.SetActive(false); // ปิดการแสดงผล Progress Bar
    }

    void Update()
    {
        // ควบคุม fade in/out โดยไม่เปลี่ยนค่า progress เอง
        if (isActive)
        {
            progressCanvas.alpha = Mathf.Lerp(progressCanvas.alpha, 1f, Time.deltaTime * 5f);
        }
        else
        {
            progressCanvas.alpha = Mathf.Lerp(progressCanvas.alpha, 0f, Time.deltaTime * 2f);
            if (progressCanvas.alpha <= 0.01f)
            {
                progressBar.gameObject.SetActive(false);
            }
        }
    }

    // ฟังก์ชันเริ่มแสดง Progress Bar
    public void StartProgress()
    {
        isActive = true;
        progressBar.gameObject.SetActive(true);
    }

    // ฟังก์ชันหยุดแสดง Progress Bar
    public void StopProgress()
    {
        isActive = false;
    }

    // ฟังก์ชันอัปเดต Progress Bar ด้วยค่า progress ที่ส่งมา
    public void SetProgress(float value)
    {
        progressBar.fillAmount = Mathf.Clamp01(value);
    }

    // รีเซ็ตค่า Progress Bar
    public void ResetProgress()
    {
        SetProgress(0f);
    }
}
