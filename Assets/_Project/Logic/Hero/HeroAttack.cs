using System;
using _Project.Logic.Data;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.Input;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.Logic.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public HeroAnimator Animator;
        public CharacterController CharacterController;
        private IInputService _input;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private static int _layerMask;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp() && !Animator.IsAttacking)
            {
                Animator.PlayAttack();
            }
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, CharacterController.center.y / 2, transform.position.z);
        }
    }
}