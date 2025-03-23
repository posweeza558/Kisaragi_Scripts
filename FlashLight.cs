using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight; // GameObject �ͧ俩��
    public AudioSource turnOnSound; // ���§������Դ俩��
    public AudioSource turnOffSound; // ���§����ͻԴ俩��

    private bool isOn = false; // ʶҹТͧ俩�� (�Դ/�Դ)

    void Start()
    {
        // ��駤������������俩�»Դ����
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
        // ��Ǩ�ͺ��á����� F �����Դ/�Դ俩��
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Flashlight is " + (isOn ? "ON" : "OFF"));
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        // ��Ѻʶҹ�俩��
        isOn = !isOn;

        // �Դ/�Դ俩��
        if (flashlight != null)
        {
            flashlight.SetActive(isOn);
        }

        // ������§������Դ/�Դ俩��
        if (isOn && turnOnSound != null)
        {
            turnOnSound.Play();
        }
        else if (!isOn && turnOffSound != null)
        {
            turnOffSound.Play();
        }

        // �ʴ�ʶҹ�� Console
        Debug.Log("Flashlight is " + (isOn ? "ON" : "OFF"));
    }
}