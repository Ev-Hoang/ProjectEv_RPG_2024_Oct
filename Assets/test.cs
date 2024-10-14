using InventorySystem.Model.Inventory;
using InventorySystem.UI.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    private UI_Item item;

    private void Awake()
    {
        item.ResetData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Press E");
            item.ResetData();
        }
    }
}
