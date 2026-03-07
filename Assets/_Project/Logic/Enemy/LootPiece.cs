using System;
using System.Collections;
using _Project.Logic.Data;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Logic.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject Skull;
        public GameObject PickupFxPrefab;
        public TextMeshPro LootText;
        public GameObject PickupPopup;

        public event Action<LootPiece> OnLootCollected;

        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;
        private ScoreManager _scoreManager;
        private LootSavedData _savedData;

        public void Construct(WorldData worldData, ScoreManager scoreManager)
        {
            _worldData = worldData;
            _scoreManager = scoreManager;
        }

        public void UpdateSavedData(Vector3Data position, Loot loot)
        {
            _savedData = new LootSavedData(position, loot);
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        public LootSavedData GetLootSavedData()
        {
            return _savedData;
        }

        private void OnTriggerEnter(Collider other)
        {
            PickUp();
        }

        private void PickUp()
        {
            if (_picked)
                return;

            _picked = true;

            OnLootCollected?.Invoke(this);

            AddScoreToManager();
            UpdateWorldData();
            HideSkull();
            PlayPickupFx();
            ShowText();

            StartCoroutine(StartDestroyTimer());
        }

        private void AddScoreToManager()
        {
            _scoreManager.AddScore(_loot.Value);
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HideSkull()
        {
            Skull.SetActive(false);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }

        private void PlayPickupFx()
        {
            Instantiate(PickupFxPrefab, transform.position, Quaternion.identity);
        }

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            PickupPopup.SetActive(true);
        }
    }
}