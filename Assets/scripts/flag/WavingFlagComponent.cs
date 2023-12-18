using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WavingFlagComponent : MonoBehaviour
{
    [SerializeField] float Height;
    [SerializeField] float Width;
    [SerializeField] float nbVertexLarge;
    [SerializeField] float nbVertexHaut;
    public Material material;

    public float waveFrequency = 1.0f;
    public float waveAmplitude = 1.0f;
    public float waveSpeed = 1.0f;
    public float waveHeight = 1.0f;
    private Vector3[] originalVertices;
    private Mesh mesh;
    private MeshFilter mFilter;
    void Start()
    {
        mesh = new();
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.uv = GenerateUv();
        mesh.RecalculateNormals();
        mFilter = GetComponent<MeshFilter>();

        Renderer renderer = GetComponent<Renderer>();

        if (material != null)
        {
            renderer.material = material;
        }
        mFilter.mesh = mesh;
        originalVertices = mesh.vertices.Clone() as Vector3[];

    }

    //private Vector2[] GenerateUv()
    //{
    //    int cpt = 0;
    //    Vector2[] X = new Vector2[(int)(nbVertexHaut * nbVertexLarge)];
    //    float distanceX = 1.0f / (nbVertexLarge - 1);
    //    float distanceY = 1.0f / (nbVertexHaut - 1);

    //    for (float y = 0; y < nbVertexHaut; y++)
    //    {
    //        for (int x = 0; x < nbVertexLarge; x++)
    //        {
    //            X[cpt] = new Vector2(x * distanceX, 1 - y * distanceY);
    //            cpt++;
    //        }
    //    }

    //    return X;
    //}
    //private int[] GenerateTriangles()
    //{
    //    int numTriangles = (int)((nbVertexLarge - 1) * (nbVertexHaut - 1) * 6);
    //    int[] triangles = new int[numTriangles];
    //    int index = 0;

    //    for (int y = 0; y < nbVertexHaut - 1; y++)
    //    {
    //        for (int x = 0; x < nbVertexLarge - 1; x++)
    //        {
    //            int currentVertex = (int)(x + y * nbVertexLarge);
    //            triangles[index++] = currentVertex;
    //            triangles[index++] = (int)(currentVertex + nbVertexLarge);
    //            triangles[index++] = currentVertex + 1;

    //            triangles[index++] = currentVertex + 1;
    //            triangles[index++] = (int)(currentVertex + nbVertexLarge);
    //            triangles[index++] = (int)(currentVertex + nbVertexLarge + 1);
    //        }
    //    }

    //    return triangles;
    //}
    //private Vector3[] GenerateVertices()
    //{
    //    int cmpt = 0;
    //    float distanceX = Width / nbVertexLarge;
    //    float distanceY = Height / nbVertexHaut;
    //    Vector3[] X = new Vector3[(int)(nbVertexHaut * nbVertexLarge)];

    //    for (float y = nbVertexHaut; y > 0; y--)
    //    {
    //        for (int x = 0; x < nbVertexLarge; x++)
    //        {
    //            X[cmpt] = new Vector3(x * distanceX, y * distanceY);
    //            cmpt++;
    //        }
    //    }
    //    return X;
    //}


    private int[] GenerateTriangles()
    {
        int[] triangles = new int[]
        {
            2,1,0,
            2,3,1,


            5,6,7,
            4,6,5
        };

        return triangles;
    }

    private Vector2[] GenerateUv()
    {
        return new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,1),

            new Vector2(1,1),
            new Vector2(0,1),


            new Vector2(1,1),new Vector2(0,1),
        };
    }


    private Vector3[] GenerateVertices()
    {
        Vector3[] vertices = new Vector3[]
               {
          new Vector3(0,0,0),
          new Vector3(1,0,0),
          new Vector3(0,1,0),
          new Vector3(1,1,0),

          new Vector3(0,1,0),
          new Vector3(1,1,0),
                    new Vector3(0,0,0),
          new Vector3(1,0,0),


               };

        return vertices;
    }

    void Update()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 originalVertex = originalVertices[i];
            float waveOffset = Mathf.Sin(Time.time * waveSpeed + originalVertex.x) * waveHeight;
            vertices[i] = originalVertex + Vector3.up * waveOffset;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
