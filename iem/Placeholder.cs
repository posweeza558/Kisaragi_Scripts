using UnityEngine;

public class Placeholder : MonoBehaviour
{
    public string requiredItem; // ����������������ö�ҧ��
    public GameObject itemModel; // �����������ԧ�
    private Renderer itemRenderer;
    private Material itemMaterial;
    private Vector3 originalPosition; // ���˹�����ͧ�����
    private Quaternion originalRotation; // �����ع����ͧ�����

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
        // ���������ʧ�������Ժ����������
        SetTransparency(0.3f);
    }

    public void ShowItem()
    {
        // �ʴ����������������ͧ�����ҧ ����������ҧ�����
        SetTransparency(1.0f);
        itemModel.transform.position = originalPosition; // �ҧ��Ѻ价����˹����
        itemModel.transform.rotation = originalRotation; // �ҧ��Ѻ价������ع���
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