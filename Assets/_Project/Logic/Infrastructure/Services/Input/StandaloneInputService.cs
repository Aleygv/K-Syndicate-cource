using UnityEngine;

#pragma warning disable SA1300
namespace _Project.Logic.Infrastructure.Services.Input
#pragma warning restore SA1300
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();

                if (axis == Vector2.zero)
                {
                    axis = UnityAxis();
                }

                return axis;
            }
        }

        private static Vector2 UnityAxis() => new (UnityEngine.Input.GetAxis(HORIZONTAL), UnityEngine.Input.GetAxis(VERTICAL));

#pragma warning disable CS0108, CS0114
        public void Dispose()
#pragma warning restore CS0108, CS0114
        {
            Debug.Log("Dispose from Standalone");
        }
    }
}