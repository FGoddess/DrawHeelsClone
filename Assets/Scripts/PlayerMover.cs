using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] private Target _target;

    private float _distance = 0.2f;

    private Vector3 _moveVector;

    public float Speed => _speed;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, _target.transform.position.y - _distance, transform.position.z);
        _moveVector = Vector3.forward * _speed;
    }

    private void OnEnable()
    {
        _target.PositionChanged += OnPosChanged;
    }
    private void OnDisable()
    {
        _target.PositionChanged -= OnPosChanged;
    }

    private void OnPosChanged()
    {
        transform.position = new Vector3(transform.position.x, _target.transform.position.y - _distance, transform.position.z);
    }

    private void Update()
    {
        transform.Translate(_moveVector * Time.deltaTime);
    }
}