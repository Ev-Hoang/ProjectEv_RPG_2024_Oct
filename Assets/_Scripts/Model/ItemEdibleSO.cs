using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace InventorySystem.Model.Item
{

    [CreateAssetMenu]
    public class ItemEdibleSO : ItemSO, IDestroyableItem, IItemAction
    {
        [Header("Edible")]
        [field: SerializeField] public itemType type;
        [field: SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();

        public string ActionName => "Consume";

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifierData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        }
    }

    [Serializable] 
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}

public enum itemType
{
    Food,
    Fruit,
    Potion,
}

