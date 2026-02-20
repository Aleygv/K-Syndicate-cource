using System;
using UnityEngine;

namespace _Project.Logic.Enemy
{
    public class CheckAttackRange : MonoBehaviour
    {
        public Attack Attack;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += OnTriggerEnter;
            TriggerObserver.TriggerExit += OnTriggerExit;

            Attack.DisableAttack();
        }

        private void OnTriggerEnter(Collider obj)
        {
            Attack.EnableAttack();
        }

        private void OnTriggerExit(Collider obj)
        {
            Attack.DisableAttack();
        }
    }
}