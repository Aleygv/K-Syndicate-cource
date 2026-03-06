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
        public UniqueId UniqueId;

        public event Action OnLootCollected;

        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;
        private ScoreManager _scoreManager;
        private LootPickupTracker _pickupTracker;

        public void Construct(WorldData worldData, ScoreManager scoreManager, LootPickupTracker tracker)
        {
            _worldData = worldData;
            _scoreManager = scoreManager;
            _pickupTracker = tracker;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        public void SetId(string id)
        {
            UniqueId.Id = id;
        }

        public void SetPosition(Vector3Data position)
        {
            transform.position = position.AsUnityVector();
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