using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Model.Item
{
    [CreateAssetMenu]
    public class ItemEquipableSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            throw new System.NotImplementedException();
        }
    }
}

