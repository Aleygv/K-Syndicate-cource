using _Project.Logic;
using _Project.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        private GUIStyle _labelStyle;

        [DrawGizmo(GizmoType.Active | GizmoType.Selected | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
        {
            // Рисуем сферу (твой существующий код)
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }

        private void OnSceneGUI()
        {
            SpawnMarker spawner = (SpawnMarker)target;

            // Получаем компоненты
            var uniqueId = spawner.GetComponent<UniqueId>();

            if (uniqueId == null) return;

            // Инициализация стиля (если нужно)
            if (_labelStyle == null)
            {
                _labelStyle = new GUIStyle(GUI.skin.box);
                _labelStyle.normal.textColor = Color.white;
                _labelStyle.fontSize = 12;
                _labelStyle.alignment = TextAnchor.MiddleCenter;
                _labelStyle.padding = new RectOffset(5, 5, 2, 2);
            }

            // Позиция над объектом
            Vector3 labelPosition = spawner.transform.position + Vector3.up * 0.7f;

            // Поворот к камере (billboard эффект)
            Quaternion rotation = Quaternion.LookRotation(
                labelPosition - SceneView.lastActiveSceneView.camera.transform.position
            );

            // Сохраняем матрицу
            Matrix4x4 originalMatrix = Handles.matrix;

            // Устанавливаем матрицу для поворота к камере
            Handles.matrix = Matrix4x4.TRS(labelPosition, rotation, Vector3.one);

            // Формируем текст
            string labelText = $"ID: {uniqueId.Id}\nType: {spawner.MonsterTypeId}";

            // Рисуем текст с фоном
            Handles.Label(Vector3.zero, labelText, _labelStyle);

            // Восстанавливаем матрицу
            Handles.matrix = originalMatrix;

            // Альтернативный способ (проще, но без фона):
            // Handles.color = Color.yellow;
            // Handles.Label(labelPosition, labelText);
        }
    }
}