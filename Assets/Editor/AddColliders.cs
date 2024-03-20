using UnityEditor;
using UnityEngine;

public class AddColliders : ScriptableObject
{
    [MenuItem("Tools/Add MeshCollider To Selected Objects")]
    static void AddMeshColliders()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (obj.GetComponent<Collider>() == null) // Vérifie si l'objet n'a pas déjà un collider
            {
                obj.AddComponent<MeshCollider>(); // Ajoute un BoxCollider
            }
        }
    }
}
