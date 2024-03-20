using UnityEditor;
using UnityEngine;

public class AddColliders : ScriptableObject
{
    [MenuItem("Tools/Add MeshCollider To Selected Objects")]
    static void AddMeshColliders()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (obj.GetComponent<Collider>() == null) // V�rifie si l'objet n'a pas d�j� un collider
            {
                obj.AddComponent<MeshCollider>(); // Ajoute un BoxCollider
            }
        }
    }
}
