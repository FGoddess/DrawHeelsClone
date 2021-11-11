using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Line _linePrefab;
    private Line _currentLine;

    //private float _smooth = 0.1f;

    private Camera _camera;

    private float _zOffset = 3;

    public event UnityAction<List<Vector3>> LineCreated;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zOffset));

        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            Time.timeScale = 0;
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
        }

        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject())
        {
            _currentLine.SetPosition(new Vector3(mousePos.x, mousePos.y, transform.position.z + _zOffset - 1));
        }

        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject())
        {
            LineCreated?.Invoke(_currentLine.Points);
            _currentLine.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
