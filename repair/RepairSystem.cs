using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RepairSystem : MonoBehaviour
{
    public float interactDistance = 3f;
    public float repairTime = 3f; // เวลาที่ใช้ซ่อม
    public Image progressBar;

    private RepairableObject currentTarget;
    private HotbarSystem hotbar;
    private bool isRepairing = false;
    private int repairCount = 0;
    public int totalRepairsNeeded = 3;
    public bool isFullyRepaired = false;

    private float currentProgress = 0f;
    private bool isHoldingKey = false;

    private ProgressBarController progressBarController;

    void Start()
    {
        hotbar = FindAnyObjectByType<HotbarSystem>();
        progressBarController = progressBar.GetComponent<ProgressBarController>();

        if (hotbar == null) Debug.LogError("❌ HotbarSystem not found!");
        if (progressBarController == null) Debug.LogError("❌ ProgressBarController not found!");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E)) // กดปุ่ม E ค้าง
        {
            isHoldingKey = true;
            TryRepair();
        }
        else
        {
            isHoldingKey = false;
            if (isRepairing)
            {
                StopRepair();
            }
        }
    }

    void TryRepair()
    {
        // ใช้กล้องหลักเป็นจุดเริ่มต้นของ Raycast
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactDistance, Color.red, 0.1f);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance))
        {
            Debug.Log($"✅ Raycast hit: {hit.collider.gameObject.name}");

            if (hit.collider.CompareTag("Repairable"))
            {
                currentTarget = hit.collider.GetComponentInParent<RepairableObject>();
                if (currentTarget == null)
                {
                    Debug.LogError("❌ RepairableObject component not found!");
                    return;
                }
                if (currentTarget.IsRepaired)
                {
                    Debug.Log("❌ This object is already repaired.");
                    return;
                }
                Debug.Log("✅ RepairableObject component found!");

                if (!currentTarget.IsRepaired && IsHoldingCorrectItem())
                {
                    Debug.Log("✅ Correct item is being held, starting repair.");
                    if (!isRepairing)
                    {
                        StartCoroutine(RepairSequence());
                    }
                }
                else
                {
                    Debug.Log("❌ ต้องถือไอเท็มที่ถูกต้องก่อนซ่อม หรือวัตถุถูกซ่อมแล้ว!");
                }
            }
            else
            {
                Debug.Log("❌ Object hit is NOT Repairable.");
            }
        }
        else
        {
            Debug.Log("❌ Raycast did not hit anything.");
        }
    }

    bool IsHoldingCorrectItem()
    {
        if (currentTarget == null || currentTarget.GetRequiredItem() == null)
        {
            Debug.LogError("❌ currentTarget หรือ requiredItem เป็น null");
            return false;
        }
        GameObject currentItem = hotbar.GetSelectedItemObject();
        if (currentItem == null)
        {
            Debug.Log("❌ ไม่ได้ถือไอเท็มใดๆ ใน Hotbar");
            return false;
        }
        bool isCorrect = currentItem.GetInstanceID() == currentTarget.GetRequiredItem().GetInstanceID();
        Debug.Log($"🔍 Checking item match: Holding {currentItem.name}, Required {currentTarget.GetRequiredItem().name}, Match: {isCorrect}");
        return isCorrect;
    }

    IEnumerator RepairSequence()
    {
        isRepairing = true;
        progressBarController.StartProgress();
        Debug.Log("🚀 Repair started!");

        // ลูปเพิ่มค่าความคืบหน้าจนถึง 1 (100%)
        while (currentProgress < 1f)
        {
            if (isHoldingKey)
            {
                currentProgress += Time.deltaTime / repairTime;
            }
            else
            {
                // เมื่อปล่อยมือ ค่าความคืบหน้าจะลดลงอย่างช้าๆ
                currentProgress -= Time.deltaTime / (repairTime * 2);
            }
            currentProgress = Mathf.Clamp01(currentProgress);
            progressBarController.SetProgress(currentProgress);
            Debug.Log($"🔄 Repair progress: {currentProgress * 100}%");
            yield return null;
        }

        if (currentProgress >= 1f)
        {
            Debug.Log("✅ Repair completed!");
            currentProgress = 0f;
            progressBarController.ResetProgress();

            if (currentTarget.hasGrate)
            {
                if (!currentTarget.isGrateOpened)
                {
                    currentTarget.OpenGrate();
                    Debug.Log("🔧 Grate opened!");
                }
                else
                {
                    currentTarget.CompleteRepair();
                    Debug.Log("✅ Object fully repaired!");
                }
            }
            else
            {
                currentTarget.CompleteRepair();
                Debug.Log("✅ Object fully repaired!");
            }

            repairCount++;
            if (repairCount >= totalRepairsNeeded)
            {
                isFullyRepaired = true;
                Debug.Log("🎉 All objects repaired!");
            }
        }

        isRepairing = false;
        progressBarController.StopProgress();
        Debug.Log("🛑 Repair stopped.");
    }

    void StopRepair()
    {
        isRepairing = false;
        progressBarController.StopProgress();
        Debug.Log("🛑 Repair canceled.");
    }
}
