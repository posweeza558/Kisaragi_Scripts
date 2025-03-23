using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndSceneChange1 : MonoBehaviour
{
    public VideoPlayer videoPlayer2;

    void Awake() // เปลี่ยนจาก Start() เป็น Awake() เพื่อป้องกันการซ้ำ
    {
        if (videoPlayer2 == null)
        {
            videoPlayer2 = GetComponent<VideoPlayer>();
        }

        videoPlayer2.isLooping = false; // ปิดการเล่นซ้ำ
        videoPlayer2.loopPointReached -= HandleVideoFinished; // ลบ EventHandler ซ้ำ (ถ้ามี)
        videoPlayer2.loopPointReached += HandleVideoFinished; // เพิ่ม EventHandler ใหม่
    }

    void HandleVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("mainmenu"); // เปลี่ยนฉาก
    }
}
