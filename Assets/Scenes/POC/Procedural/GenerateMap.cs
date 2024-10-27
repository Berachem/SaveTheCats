using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCity : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject[] vegetation; // Liste d'objets de v�g�tation
    public float buildingSpacing = 0.5f; // Espace r�duit entre les b�timents
    public float vegetationChance = 0.3f; // Probabilit� d'ajouter de la v�g�tation

    void Start()
    {
        Perlin surface = new Perlin();
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        float scalex = this.transform.localScale.x;
        float scalez = this.transform.localScale.z;

        for (int v = 0; v < vertices.Length; v++)
        {
            float perlinValue = surface.Noise(vertices[v].x * 2 + 0.1365143f, vertices[v].z * 2 + 1.21688f) * 10;
            perlinValue = Mathf.Round(Mathf.Clamp(perlinValue, 0, buildings.Length - 1));

            Vector3 buildingPosition = new Vector3(
                vertices[v].x * scalex * buildingSpacing,
                0, // Fixe la position en y � z�ro
                vertices[v].z * scalez * buildingSpacing
            );
            Instantiate(buildings[(int)perlinValue], buildingPosition, buildings[(int)perlinValue].transform.rotation);


            // Ajout de v�g�tation autour des b�timents
            if (vegetation.Length > 0 && Random.value < vegetationChance)
            {
                int randomVegetationIndex = Random.Range(0, vegetation.Length);
                Vector3 vegetationPosition = buildingPosition + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
                Instantiate(vegetation[randomVegetationIndex], vegetationPosition, Quaternion.identity);
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
    }
}
