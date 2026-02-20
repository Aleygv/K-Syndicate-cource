using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Logic.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        public NavMeshAgent Agent;
        public EnemyAnimator Animator;
        private const float MINIMAL_VELOCITY = 0.1f;


        private void Update()
        {
            if (ShouldMove())
            {
                Animator.Move(Agent.velocity.magnitude);
            }
            else
            {
                Animator.StopMoving();
            }
        }

        private bool ShouldMove()
        {
            return Agent.velocity.magnitude > MINIMAL_VELOCITY && Agent.remainingDistance > Agent.radius;
        }
    }
}