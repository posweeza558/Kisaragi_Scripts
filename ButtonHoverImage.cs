using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image buttonImage;
    public Sprite defaultSprite;
    public Sprite hoverSprite;
    public static bool isOptionsOpen = false;

    private bool isHovered = false;

    void Start()
    {
        if (buttonImage == null) 
            buttonImage = GetComponent<Image>();
    }

    void Update()
    {
        // รีเซ็ตเป็น default เมื่อไม่ได้ hover
        if (!isOptionsOpen && !isHovered)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isOptionsOpen) return;
        isHovered = true;
        buttonImage.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isOptionsOpen) return;
        isHovered = false;
        buttonImage.sprite = defaultSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ล้างสถานะ Selected หลังจากคลิก
        EventSystem.current.SetSelectedGameObject(null);
        isHovered = false;
        buttonImage.sprite = defaultSprite;
    }
}
