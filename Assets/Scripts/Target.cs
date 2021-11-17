using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [SerializeField] private RayCastPoint _rayCastPoint;

    [SerializeField] private Transform _parent;
    [SerializeField] private PlayerMover _player;

    [SerializeField] private float _offset;

    [SerializeField] private float _timer;
    [SerializeField] private float _speed;

    private Vector3 _currentY;

    public event UnityAction PositionChanged;


    private void Start()
    {
        transform.position = new Vector3(_parent.position.x + _offset, _parent.position.y, _parent.position.z);

        if (Physics.Raycast(_rayCastPoint.transform.position, Vector3.down, out RaycastHit hit))
        {
            _currentY = transform.position;
        }

        _speed = _player.Speed * 2;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < 1.5f)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
        if (_timer > 3f)
        {
            _timer = 0;
        }

        if (Physics.Raycast(_rayCastPoint.transform.position, Vector3.down, out RaycastHit hit))
        {
            Debug.DrawLine(_rayCastPoint.transform.position, hit.point, Color.red);

            float temp = hit.point.y + 0.2f;

            if (hit.collider.CompareTag("Obstacle") && temp != _currentY.y)
            {
                _currentY = new Vector3(transform.position.x, temp, transform.position.z);
                transform.position = _currentY;

                PositionChanged?.Invoke();
            }
        }
    }
}
