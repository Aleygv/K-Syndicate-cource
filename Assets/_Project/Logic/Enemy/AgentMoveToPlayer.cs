using System;
using _Project.Logic.Infrastructure.Factory;
using _Project.Logic.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Logic.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MINIMAL_DISTANCE = 1;

        public NavMeshAgent Agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += HeroCreated;
            }
        }

        private void Update()
        {
            if (Initialized() && HeroNotReached())
            {
                Agent.destination = _heroTransform.position;
            }
        }

        private bool Initialized()
        {
            return _heroTransform != null;
        }

        private void InitializeHeroTransform()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }

        private void HeroCreated()
        {
            InitializeHeroTransform();
        }

        private bool HeroNotReached()
        {
            return Vector3.Distance(Agent.transform.position, _heroTransform.position) >= MINIMAL_DISTANCE;
        }
    }
}