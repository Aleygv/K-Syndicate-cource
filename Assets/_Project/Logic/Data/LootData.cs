using System;
using System.Collections.Generic;
using _Project.Logic.Enemy;
using UnityEngine.Serialization;

namespace _Project.Logic.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public Action Changed;
        public List<LootSavedData> RemainingLoot = new List<LootSavedData>();

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }
    }
}