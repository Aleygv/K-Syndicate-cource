using System;
using System.Linq;
using _Project.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace _Project.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId)target;

            Generate(uniqueId);

            if (IsPrefab(uniqueId))
            {
                return;
            }

            if (string.IsNullOrEmpty(uniqueId.Id))
            {
                Generate(uniqueId);
            }
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

                if (uniqueIds.Any(other=> other != uniqueId && other.Id == uniqueId.Id))
                {
                    Generate(uniqueId);
                }
            }
        }

        private bool IsPrefab(UniqueId uniqueId)
        {
            return uniqueId.gameObject.scene.rootCount == 0;
        }

        private void Generate(UniqueId uniqueId)
        {
            CreateIdWithSceneName(uniqueId);

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }

        private static void CreateIdWithSceneName(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";
        }
    }
}