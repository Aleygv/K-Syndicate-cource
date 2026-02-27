using _Project.Logic.Infrastructure.Factory;
using _Project.Logic.Infrastructure.Services;
using UnityEngine;

namespace _Project.Logic.Enemy
{
    public class AgentRotateToHero : MonoBehaviour
    {
        public float Speed;

        private Transform _heroTransform;
        private Vector3 _positionToLook;

        private void Update()
        {
            if (Initialized())
                RotateTowardsHero();
        }

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
            Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 position) =>
            Quaternion.LookRotation(position);

        private float SpeedFactor() =>
            Speed * Time.deltaTime;

        private bool Initialized() =>
            _heroTransform = null;
    }
}