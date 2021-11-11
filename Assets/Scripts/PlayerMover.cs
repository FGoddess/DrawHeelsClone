using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] private Target _target;

    private void OnEnable()
    {
        _target.PositionChanged += OnPosChanged;
    }
    private void OnDisable()
    {
        _target.PositionChanged -= OnPosChanged;
    }

    private void OnPosChanged(float y)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

}
