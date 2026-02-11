using System;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Project.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        public BoxCollider Collider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();

            Debug.Log("Progress saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!Collider)
            {
                return;
            }

            Gizmos.color = new Color(30, 255, 0, 130);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }
    }
}