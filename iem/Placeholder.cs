using UnityEngine;

public class Placeholder : MonoBehaviour
{
    public string requiredItem; // ชื่อไอเท็มที่สามารถวางได้
    public GameObject itemModel; // โมเดลไอเท็มจริงๆ
    private Renderer itemRenderer;
    private Material itemMaterial;
    private Vector3 originalPosition; // ตำแหน่งเดิมของไอเท็ม
    private Quaternion originalRotation; // การหมุนเดิมของไอเท็ม

    void Start()
    {
        itemRenderer = itemModel.GetComponent<Renderer>();
        if (itemRenderer != null)
        {
            itemMaterial = itemRenderer.material;
        }
        originalPosition = itemModel.transform.position;
        originalRotation = itemModel.transform.rotation;
    }

    public void PickUpItem()
    {
        // ทำให้โปร่งแสงเมื่อหยิบไอเท็มไปแล้ว
        SetTransparency(0.3f);
    }

    public void ShowItem()
    {
        // แสดงไอเท็มปกติเมื่อมองเพื่อวาง หรือเมื่อวางสำเร็จ
        SetTransparency(1.0f);
        itemModel.transform.position = originalPosition; // วางกลับไปที่ตำแหน่งเดิม
        itemModel.transform.rotation = originalRotation; // วางกลับไปที่การหมุนเดิม
    }

    private void SetTransparency(float alpha)
    {
        if (itemMaterial != null)
        {
            Color color = itemMaterial.color;
            color.a = alpha;
            itemMaterial.color = color;
        }
    }
}