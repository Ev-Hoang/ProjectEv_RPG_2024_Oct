using InventorySystem.UI.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenItem_UI : MonoBehaviour
{
    [SerializeField]
    private UI_Item _UI_Item;

    [SerializeField]
    List<UI_Item> _ItemsList = new List<UI_Item>();

    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }

    private void Awake()
    {
        Toggle(true);
    }

    public void Initialize()
    {
        //Item in hand
        for (int i = 0; i < 8; i++)
        {
            UI_Item item = Instantiate(_UI_Item, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(gameObject.transform);
            item.transform.localScale = Vector3.one;
            _ItemsList.Add(item);
        }
    }

    public void ResetAllItems()
    {
        for (int i = 0; i < _ItemsList.Count; i++)
        {
            _ItemsList[i].ResetData();
        }
    }
    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (_ItemsList.Count > itemIndex)
        {
            _ItemsList[itemIndex].SetData(itemImage, itemQuantity);
        }
    }


}
