using System;
using _Project.Logic.Enemy;

namespace _Project.Logic.Data
{
    [Serializable]
    public class LootSavedData
    {
        // public string Id;
        public Vector3Data Position;
        public Loot Loot;

        public LootSavedData(Vector3Data position, Loot loot)
        {
            Position = position;
            Loot = loot;
        }
    }
}