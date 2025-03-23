using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSystem : MonoBehaviour
{
    public Image[] hotbarSlots;
    public Sprite emptySlotSprite;
    public GameObject[] itemObjects; // เก็บ GameObject ของไอเท็ม
    private List<ItemData> hotbarItems = new List<ItemData>();
    public int selectedIndex = -1;

    public List<ItemData> HotbarItems => hotbarItems;

    void Start()
    {
        UpdateHotbarUI();
        foreach (GameObject item in itemObjects) if (item != null) item.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
    }

    public bool AddItem(ItemData itemData)
    {
        if (hotbarItems.Count >= hotbarSlots.Length) return false;
        if (hotbarItems.Contains(itemData)) return false;

        hotbarItems.Add(itemData);
        UpdateHotbarUI();
        return true;
    }

    public void RemoveItem(int index)
    {
        if (index < 0 || index >= hotbarItems.Count) return;
        hotbarItems.RemoveAt(index);
        UpdateHotbarUI();
    }

    void SelectSlot(int index)
    {
        if (selectedIndex == index) return;
        selectedIndex = index;
        UpdateHotbarUI();
        UpdateSelectedItem();
    }

    void UpdateHotbarUI()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i < hotbarItems.Count)
                hotbarSlots[i].sprite = hotbarItems[i].itemSprite;
            else
                hotbarSlots[i].sprite = emptySlotSprite;
        }
    }

    void UpdateSelectedItem()
    {
        foreach (GameObject item in itemObjects) if (item != null) item.SetActive(false);

        if (selectedIndex >= 0 && selectedIndex < hotbarItems.Count)
        {
            GameObject itemObject = FindItemObject(hotbarItems[selectedIndex].itemName);
            if (itemObject != null) itemObject.SetActive(true);
        }
    }

    GameObject FindItemObject(string itemName)
    {
        foreach (GameObject item in itemObjects)
        {
            if (item != null && item.name == itemName) // ตรวจสอบว่า item ไม่เป็น null และชื่อตรงกัน
                return item;
        }
        return null;
    }

    public bool IsHoldingCorrectItem(GameObject requiredItem)
    {
        return GetSelectedItemObject() == requiredItem;
    }

    public void DeselectItem()
    {
        selectedIndex = -1;
        UpdateHotbarUI();
        UpdateSelectedItem();
    }

    public GameObject GetSelectedItemObject()
    {
        if (selectedIndex >= 0 && selectedIndex < hotbarItems.Count)
        {
            return FindItemObject(hotbarItems[selectedIndex].itemName); // ค้นหา GameObject จาก itemName
        }
        return null;
    }

    public GameObject GetCurrentItem()
    {
        return GetSelectedItemObject(); // ใช้ GetSelectedItemObject() เพื่อให้สอดคล้องกัน
    }
}