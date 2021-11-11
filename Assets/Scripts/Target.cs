using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [SerializeField] private Vector3 _road;
    [SerializeField] private RayCastPoint _rayCastPoint;

    [SerializeField] private bool _flag = true;

    public event UnityAction<float> PositionChanged;

    private void Start()
    {
        if (Physics.Raycast(_rayCastPoint.transform.position, Vector3.down, out RaycastHit hit))
        {
            _road = hit.point;
        }
    }

    private void Update()
    {
        if(Physics.Raycast(_rayCastPoint.transform.position, Vector3.down, out RaycastHit hit))
        {
            Debug.DrawLine(_rayCastPoint.transform.position, hit.point, Color.red);

            Debug.Log("_flag " + _flag);
            Debug.Log("hit != _road " + (hit.point.y != _road.y));

            if(hit.point.y != _road.y && _flag)
            {
                Debug.Log("da");
                _flag = false;
                transform.position = new Vector3(transform.position.x, transform.position.y + (hit.point.y - _road.y), transform.position.z);
                PositionChanged?.Invoke(hit.point.y - _road.y);
            }
        }
    }
}
