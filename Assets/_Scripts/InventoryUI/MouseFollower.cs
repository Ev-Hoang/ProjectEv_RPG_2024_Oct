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
    [SerializeField]
    private Tooltip itemTooltip;

    public UI_Item mouseItem;



    public void Initialize()
    {
        UI_Item item = Instantiate(UI_Item, UI_Offset.position, Quaternion.identity);
        mouseItem = item;
        item.transform.SetParent(UI_Offset);
        item.transform.localScale = Vector3.one;
        item.ToggleBorder(false);
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
            null,  // Set to null for Screen Space - Overlay
            out canvasPosition
        );
        transform.position = canvas.transform.TransformPoint(canvasPosition);
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
