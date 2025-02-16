using InventorySystem.UI.Item;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RectTransform UI_Offset;
    [SerializeField]
    private UI_Item UI_Item;
    
    public Tooltip itemTooltip;
    public UI_Item mouseItem;



    public void Initialize()
    {
        UI_Item item = Instantiate(UI_Item, UI_Offset.position, Quaternion.identity);
        mouseItem = item;
        item.transform.SetParent(UI_Offset.transform);
        item.transform.localScale = Vector3.one;

        item.ToggleBorder(false);
        Toggle(false);
    }
    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
    }

    public UI_Item GetItem() { return mouseItem; }

    public void SetData(Sprite sprite, int quantity)
    {
        mouseItem.SetData(sprite, quantity);
    }

    void Update()
    {
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            null,
            out canvasPosition
        );
        transform.position = canvas.transform.TransformPoint(canvasPosition);

        //Make tooltip Always stays on screen.
        if ( itemTooltip.isActiveAndEnabled )
        {
            float tooltipPosX = itemTooltip.transform.GetComponent<RectTransform>().position.x + itemTooltip.contentRectTransform.rect.width * canvas.GetComponent<RectTransform>().localScale.x;
            float tooltipPosY = itemTooltip.transform.GetComponent<RectTransform>().position.y - itemTooltip.contentRectTransform.rect.height * canvas.GetComponent<RectTransform>().localScale.y;

            float offsetX = 0f, offsetY = 0f;

            float canvasScreenSizeX = canvas.GetComponent<RectTransform>().rect.width * canvas.GetComponent<RectTransform>().localScale.x;
            if (tooltipPosX > canvasScreenSizeX)
            {
                offsetX = canvasScreenSizeX - tooltipPosX;
            }
            if (tooltipPosY < 0f)
            {
                offsetY = 0f - tooltipPosY;
            }
            transform.position += new Vector3(offsetX, offsetY, 0);
        }
    }

    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }

    public void ToggleItemTooltip(bool value)
    {
        if(value) itemTooltip.showToolTip();
        else itemTooltip.hideTooltip();
    }
}
