using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private DrawManager _drawManager;

    [SerializeField] private Material _material;

    [SerializeField] private GameObject _emptyGameObject;

    [SerializeField] private GameObject _container;

    private float _linesOffset = 0.1f;

    public event UnityAction<GameObject, GameObject> MeshCreated;

    private void Awake()
    {
        gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = _material;
    }

    private void OnEnable()
    {
        _drawManager.LineCreated += OnLineCreated;
    }

    private void OnDisable()
    {
        _drawManager.LineCreated -= OnLineCreated;
    }

    private void OnLineCreated(List<Vector3> points)
    {
        _container = Instantiate(_emptyGameObject, gameObject.transform);
        _container.AddComponent<MeshFilter>();
        MeshRenderer containerMeshRenderer = _container.AddComponent<MeshRenderer>();
        containerMeshRenderer.material = _material;
        _container.name = "HeelsMesh";

        for (int i = 0; i < points.Count - 1; i++)
        {
            var temp = Instantiate(_emptyGameObject, _container.transform);
            CreateCube(points[i], points[i + 1], temp);
        }

        CombineMeshes();
        MeshCreated?.Invoke(_container, Instantiate(_container));
    }

    private void CreateCube(Vector3 v1, Vector3 v2, GameObject gameObject)
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = _material;
        var mesh = meshFilter.mesh;

        var line0 = new List<Vector3>() { new Vector3(v1.x, v1.y, v1.z), new Vector3(v2.x, v2.y, v2.z) };
        var line1 = new List<Vector3>() { new Vector3(v1.x, v1.y, v1.z + _linesOffset), new Vector3(v2.x, v2.y, v2.z + _linesOffset) };
        var line2 = new List<Vector3>() { new Vector3(v1.x, v1.y - _linesOffset, v1.z + _linesOffset), new Vector3(v2.x, v2.y - _linesOffset, v2.z + _linesOffset) };
        var line3 = new List<Vector3>() { new Vector3(v1.x, v1.y - _linesOffset, v1.z), new Vector3(v2.x, v2.y - _linesOffset, v2.z) };

        var vertices = new List<Vector3>();

        for (int i = 0; i < line0.Count; i++)
        {
            vertices.Add(line0[i]);
            vertices.Add(line3[i]);
            vertices.Add(line2[i]);
            vertices.Add(line1[i]);
        }

        var indices = new List<int>();
        int count = (vertices.Count / 2);

        for (int i = 0; i < count; i++)
        {
            int i1 = i;
            int i2 = (i1 + 1) % count;
            int i3 = i1 + count;
            int i4 = i2 + count;

            indices.Add(i2);
            indices.Add(i1);
            indices.Add(i3);

            indices.Add(i3);
            indices.Add(i4);
            indices.Add(i2);
        }

        indices.AddRange(new int[] { 2, 3, 0, 0, 1, 2, 5, 4, 7, 7, 6, 5 });

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.Optimize();
    }

    private void CombineMeshes()
    {
        MeshFilter[] meshFilters = _container.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.worldToLocalMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        _container.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        _container.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        _container.transform.gameObject.SetActive(true);
    }
}
