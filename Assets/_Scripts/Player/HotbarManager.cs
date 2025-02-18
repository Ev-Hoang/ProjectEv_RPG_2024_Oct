using InventorySystem.Model.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    [SerializeField]
    private UI_Hotbar hotbarUI;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private HandHeldSystem playerHand;

    //Keycode from 1->9 (not pad version obv)
    private KeyCode[] keyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
    };

    // Start is called before the first frame update
    void Start()
    {
        SelectItem(0);
    }

    void SelectItem(int index)
    {
        //Implement - UI : Show item selected on hotbar
        hotbarUI.SelectSlot(index);
        //Implement - Player: Show player holding item
        InventoryItem hotbarItem = inventoryData.GetItemAt(index);
        playerHand.HoldingItem(hotbarItem.item);
    }

    // Update is called once per frame
    void Update()
    {
        if (hotbarUI.isActiveAndEnabled)
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    SelectItem(i);
                }
            }
        }
    }
}
