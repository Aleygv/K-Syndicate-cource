using System;

namespace _Project.Logic.Data
{
    [Serializable]
    public class LootSavedData
    {
        public string Id;
        public Vector3Data Position;

        public LootSavedData(string id, Vector3Data position)
        {
            Id = id;
            Position = position;
        }
    }
}