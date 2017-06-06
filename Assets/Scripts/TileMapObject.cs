using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TileMapObject : MonoBehaviour
{
    public int Width = 10, Height = 10;
    public float TileSize = 1;
    
    private Vector3[] _vertices;
    private int[] _triangles;
    private Vector3[] _normals;
    private Vector2[] _uvs;
    private Mesh _mesh;

    void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
        _mesh.name = "TileMap Grid";
        // 4 vertices per tile
        _vertices = new Vector3[(Width * Height) * 4];
        // 1 normal per vertice
        _normals = new Vector3[_vertices.Length];
        // 2 triangles (3 vertices each) per tile (quad)
        _triangles = new int[(Width * Height) * 2 * 3];
        for (int v = 0, t = 0, y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x, v += 4, t += 2 * 3)
            {
                _vertices[v] = new Vector3(x, 0, y);
                _vertices[v+1] = new Vector3(x+TileSize, 0, y);
                _vertices[v+2] = new Vector3(x, 0, y+TileSize);
                _vertices[v+3] = new Vector3(x+TileSize, 0, y+TileSize);

                _triangles[t] = v+2;
                _triangles[t+1] = v+1;
                _triangles[t+2] = v;
                
                _triangles[t+3] = v+1;
                _triangles[t+4] = v+2;
                _triangles[t+5] = v+3;

                var normal = Vector3.up;
                _normals[v] = normal;
                _normals[v+1] = normal;
                _normals[v+2] = normal;
                _normals[v+3] = normal;
            }
        }
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.normals = _normals;
    }

    private void OnDrawGizmos()
    {
        if (_vertices == null) return;
        
        Gizmos.color = Color.black;
        foreach (var vertice in _vertices)
        {
            Gizmos.DrawSphere(vertice, 0.05f);
        }
    }
}