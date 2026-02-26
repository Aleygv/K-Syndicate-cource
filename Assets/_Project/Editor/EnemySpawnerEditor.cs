using _Project.Logic;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}