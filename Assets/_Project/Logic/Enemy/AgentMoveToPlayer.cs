using _Project.Logic.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Logic.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float MINIMAL_DISTANCE = 1;

        public NavMeshAgent Agent;

        private Transform _heroTransform;

        private IGameFactory _gameFactory;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void Update()
        {
            if (_heroTransform && HeroNotReached())
            {
                Agent.destination = _heroTransform.position;
            }
        }

        private bool HeroNotReached()
        {
            return Vector3.Distance(Agent.transform.position, _heroTransform.position) >= MINIMAL_DISTANCE;
        }
    }
}