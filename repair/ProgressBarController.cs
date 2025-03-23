using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Image progressBar; // Radial Progress Bar
    public CanvasGroup progressCanvas; // Canvas Group ����Ѻ fade in/out

    private bool isActive = false;

    void Start()
    {
        progressCanvas.alpha = 0f; // ��͹ Progress Bar ������������
        progressBar.gameObject.SetActive(false); // �Դ����ʴ��� Progress Bar
    }

    void Update()
    {
        // �Ǻ��� fade in/out ���������¹��� progress �ͧ
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

    // �ѧ��ѹ������ʴ� Progress Bar
    public void StartProgress()
    {
        isActive = true;
        progressBar.gameObject.SetActive(true);
    }

    // �ѧ��ѹ��ش�ʴ� Progress Bar
    public void StopProgress()
    {
        isActive = false;
    }

    // �ѧ��ѹ�ѻവ Progress Bar ���¤�� progress �������
    public void SetProgress(float value)
    {
        progressBar.fillAmount = Mathf.Clamp01(value);
    }

    // ���絤�� Progress Bar
    public void ResetProgress()
    {
        SetProgress(0f);
    }
}
