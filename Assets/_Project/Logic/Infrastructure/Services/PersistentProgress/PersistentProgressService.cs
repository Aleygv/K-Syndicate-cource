using _Project.Logic.Data;
using UnityEngine;

namespace _Project.Logic.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }

        public void Dispose()
        {
            // Progress = null;
            Debug.Log("Dispose from PersistentProgress");
        }
    }
}