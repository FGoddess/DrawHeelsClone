using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private List<Vector3> _points;
    public List<Vector3> Points => _points;

    private void Awake()
    {
        _points = new List<Vector3>();
    }

    public void SetPosition(Vector3 position)
    {
        if (!TryAppend(position))
        {
            return;
        }

        _points.Add(position);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
    }

    private bool TryAppend(Vector2 position)
    {
        return _lineRenderer.positionCount == 0 || Vector3.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), position) > 0.2f;
    }
}
