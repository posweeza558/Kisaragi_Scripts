using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    [Header("Camera Effects")]
    public Camera mainCamera; 
    public float normalFOV = 60f; 
    public float sprintFOV = 50f;
    public float fovChangeSpeed = 5f;
    private bool isSprinting = false;
    private Vector3 originalPos; // ตำแหน่งเริ่มต้นของกล้อง
       void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        float targetFOV = isSprinting ? sprintFOV : normalFOV;
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * fovChangeSpeed);
        
    }

    public void SetSprinting(bool sprinting)
    {
        isSprinting = sprinting;
    }
}
