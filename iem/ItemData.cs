using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite; // ��Ǩ�ͺ��� Sprite ���١��˹����� Inspector
}