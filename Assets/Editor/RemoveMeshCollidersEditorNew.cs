using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class RemoveMeshCollidersEditor : EditorWindow
    {
        [MenuItem("Tools/Remove Mesh Colliders from Selected Objects")]
        private static void RemoveMeshCollidersFromSelection()
        {
            // Check if at least one GameObject is selected
            if (Selection.gameObjects.Length == 0)
            {
                EditorUtility.DisplayDialog("No Selection", "Please select at least one GameObject.", "OK");
                return;
            }

            // Iterate over all selected GameObjects
            foreach (GameObject selectedGameObject in Selection.gameObjects)
            {
                MeshCollider[] colliders = selectedGameObject.GetComponentsInChildren<MeshCollider>(true);
                foreach (var collider in colliders)
                {
                    Undo.DestroyObjectImmediate(collider); // Allows undoing the operation
                }

                Debug.Log("Removed all MeshColliders from " + selectedGameObject.name + " and its children.");
            }
        }
    }
}
