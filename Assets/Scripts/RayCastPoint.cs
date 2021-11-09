using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPoint : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _xOffset = 0.5f;

    private void Update()
    {
        transform.position = new Vector3(_target.position.x + _xOffset, transform.position.y, transform.position.z);
    }
}
