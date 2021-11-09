using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform _road;
    [SerializeField] private RayCastPoint _rayCastPoint;

    private void Update()
    {
        if(Physics.Raycast(_rayCastPoint.transform.position, Vector3.down, out RaycastHit hit))
        {
            Debug.DrawLine(_rayCastPoint.transform.position, hit.point, Color.red);

            if(hit.collider.transform.position.y != _road.position.y)
            {
                Debug.Log("da");
                transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            }
        }
    }
}
