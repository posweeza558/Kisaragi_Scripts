using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEditor.Rendering.LookDev;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float sprintSpeedMultiplier = 2f;
    private float currentSpeed;

    [Header("Jump & Gravity Settings")]
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float fallMultiplier = 2.5f;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaRegenRate = 15f;
    public float staminaDrainRate = 30f;
    public float jumpStaminaCost = 10f;
    public float staminaRegenDelay = 2f; // ดีเลย์ 2 วินาทีก่อนเริ่มฟื้นฟู
    private float stamina;
    private float staminaRegenDelayTimer = 3f;
    private bool isSprinting = false;// ตัวแปรนับเวลาดีเลย์
    private CameraEffects cameraEffects;
    [SerializeField] private RawImage staminaOverlayRawImage;  // เชื่อมโยง RawImage


    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraEffects = FindAnyObjectByType<CameraEffects>();
        stamina = maxStamina;
        currentSpeed = speed;
    }

    void Update()
    {
        isGrounded = IsGroundedByRaycast();
        ApplyGravity();
        RegenerateStamina();
        if (isSprinting && stamina >= 0)
        {
            stamina -= staminaDrainRate * Time.deltaTime;
            if (stamina <= 0)
            {
                stamina = 0;
                StopSprinting();
                isSprinting = false;
            }
            stamina = Mathf.Max(stamina, 0);
            Debug.Log("Sprinting! Current Stamina: " + stamina);
        }
        //Debug.Log("PlayerMotor initialized. Stamina: " + stamina);
        float staminaPercentage = stamina / maxStamina;
        staminaOverlayRawImage.color = new Color(0, 0, 0, 1 - staminaPercentage);
    }

    public void ProcessMove(Vector2 move)
    {
        Vector3 moveDirection = transform.TransformDirection(new Vector3(move.x, 0, move.y));
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }
    private bool IsGroundedByRaycast()
    {
        // ตั้งค่าความยาวของ Raycast ที่จะตรวจสอบ
        float rayLength = 1.1f; // ค่านี้อาจจะต้องปรับขึ้นอยู่กับขนาดของตัวละคร

        // ส่ง Raycast ลงไปที่ตำแหน่งของตัวละคร
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
        {
            // ถ้า Raycast ถูกกระทบที่พื้น (ไม่ใช่ Object อื่น)
            return true;
        }
        else
        {
            // ถ้าไม่มีการชนกับพื้น
            return false;
        }
    }
    private void ApplyGravity()
    {
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        else
        {
            playerVelocity.y += gravity * fallMultiplier * Time.deltaTime;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded && stamina >= jumpStaminaCost)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            stamina -= jumpStaminaCost;
            staminaRegenDelayTimer = 0f; // รีเซ็ตดีเลย์เมื่อกระโดด
        }
    }

    public void Sprint(bool sprinting)
    {
        if (sprinting)
        {
            if (stamina > 0)
            {
                isSprinting = true;
                currentSpeed = speed * sprintSpeedMultiplier;
                cameraEffects.SetSprinting(sprinting);
                Debug.Log("Started sprinting.");
            }
            else
            {
                StopSprinting(); // ถ้าสตามิน่าหมด ให้หยุด Sprint ทันที
            }
        }
        else
        {
            StopSprinting();
        }
    }
    private void StopSprinting()
    {
        isSprinting = false;
        currentSpeed = speed;
        Debug.Log("Stopped sprinting.");
        cameraEffects.SetSprinting(false);
    }


    private void RegenerateStamina()
    {
        if (stamina < maxStamina && currentSpeed == speed)
        {
            if (staminaRegenDelayTimer >= staminaRegenDelay)
            {
                // ฟื้นฟูสแตมินาหลังจากผ่านดีเลย์
                stamina += staminaRegenRate * Time.deltaTime;
                stamina = Mathf.Min(stamina, maxStamina);
                Debug.Log("Stamina regenerating. Current Stamina: " + stamina);
            }
            else
            {
                // นับเวลาดีเลย์
                staminaRegenDelayTimer += Time.deltaTime;
                Debug.Log("Waiting for stamina regen delay. Time left: " + (staminaRegenDelay - staminaRegenDelayTimer));
            }
        }
        else
        {
            // รีเซ็ตดีเลย์เมื่อเริ่มวิ่งหรือกระโดด
            staminaRegenDelayTimer = 0f;
        }
    }
}