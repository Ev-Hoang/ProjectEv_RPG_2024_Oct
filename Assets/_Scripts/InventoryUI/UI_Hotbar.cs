using InventorySystem.UI.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hotbar : MonoBehaviour
{
    [SerializeField]
    private UI_Item _UI_Item;

    [SerializeField]
    List<UI_Item> _ItemsList = new List<UI_Item>();

    private int currentHighlightSlot = 0;

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

    //Select item in hotbar, only 1 can be selected, purpose with player system (holding item)
    public void SelectSlot(int index)
    {
        _ItemsList[currentHighlightSlot].ToggleBorder(true);
        _ItemsList[index].ToggleBorder(false);
        currentHighlightSlot = index;
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
