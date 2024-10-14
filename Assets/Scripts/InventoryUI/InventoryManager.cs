using InventorySystem.Model.Inventory;
using InventorySystem.UI.Inventory;
using InventorySystem.UI.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace InventorySystem.Controller
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private UI_Inventory inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PreapareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty) continue;
                inventoryData.AddItem(item);
            }
        }

        private string PrepareDescription(InventoryItem item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(item.item.Description);
            sb.AppendLine();
            for (int i = 0; i < item.itemState.Count; i++)
            {
                sb.Append($"{item.itemState[i].itemParameter.ParameterName}" +
                    $": {item.itemState[i].value} / " +
                    $" {item.item.ParametersList[i].value}");
            }
            return sb.ToString();
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {                
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PreapareUI()
        {
            inventoryUI.Initialize();
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            inventoryUI.OnItemSwap += HandleSwapItem;
            inventoryUI.OnItemSelected += HandleItemSelection;
        }

        private void HandleItemSelection(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty && inventoryData.GetItemAt(32).IsEmpty) return;
            inventoryData.CreateMouseSelectItem(itemIndex);
        }

        private void HandleSwapItem(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!inventoryUI.isActiveAndEnabled)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
        }

        void OnDestroy()
        {
            inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
            inventoryUI.OnItemActionRequested -= HandleItemActionRequest;
            inventoryUI.OnItemSwap -= HandleSwapItem;
            inventoryUI.OnItemSelected -= HandleItemSelection;
        }
    }
}