using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite; // ตรวจสอบว่า Sprite นี้ถูกกำหนดไว้ใน Inspector
}