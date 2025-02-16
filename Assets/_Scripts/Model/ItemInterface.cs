using InventorySystem.Model.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyableItem
{

}

public interface IItemAction
{
    public string ActionName { get; }
    bool PerformAction(GameObject character, List<ItemParameter> itemState);
}
