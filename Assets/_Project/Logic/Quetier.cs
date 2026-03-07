using System;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Project.Logic
{
    public class Quetier : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnApplicationQuit()
        {
            _saveLoadService.SaveProgress();
        }
    }
}