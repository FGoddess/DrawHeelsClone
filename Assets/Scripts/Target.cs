using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [SerializeField] private RayCastPoint _rayCastPoint;

    [SerializeField] private Transform _parent;
    [SerializeField] private PlayerMover _player;

    [SerializeField] private float _offset;

    [SerializeField] private float _timer;

    private float _speed;
    private float _heelsOffset = 0.2f;
    private float _currentY;
    private Vector3 _moveVector;

    public event UnityAction PositionChanged;


    private void Start()
    {
        transform.position = new Vector3(_parent.position.x + _offset, _parent.position.y, _parent.position.z);

        if (Physics.Raycast(_rayCastPoint.transform.position, Vector3.down, out RaycastHit hit))
        {
            Debug.DrawLine(_rayCastPoint.transform.position, hit.point, Color.red);

            float newPoint = hit.point.y + _heelsOffset;

            if (hit.collider.CompareTag("Obstacle") && newPoint != _currentY)
            {
                _currentY = newPoint;
                transform.position = new Vector3(transform.position.x, _currentY, transform.position.z);
                PositionChanged?.Invoke();
            }
        }

        _speed = _player.Speed * 2f;
        _moveVector = Vector3.right;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < 1f)
        {
            transform.Translate(_moveVector * Time.deltaTime * _speed);
        }
        if (_timer > 2f)
        {
            _timer = 0;
        }
    
        if (Physics.Raycast(_rayCastPoint.transform.position, Vector3.down, out RaycastHit hit))
        {
            Debug.DrawLine(_rayCastPoint.transform.position, hit.point, Color.red);

            float newPoint = hit.point.y + _heelsOffset;

            if (hit.collider.CompareTag("Obstacle") && newPoint != _currentY)
            {
                _currentY = newPoint;
                StartCoroutine(ChangeYPosition(_currentY));
            }

            if(hit.collider.TryGetComponent(out EndPoint endPoint))
            {
                endPoint.RestartLevel();
            }
        }
    }

    private IEnumerator ChangeYPosition(float targetY)
    {
        if (transform.position.y < targetY)
        {
            _moveVector = new Vector3(1f, targetY, 0);
            while (transform.position.y <= targetY)
            {
                PositionChanged?.Invoke();
                yield return null;
            }
        }
        else
        {
            _moveVector = new Vector3(1f, -targetY, 0);
            while (transform.position.y >= targetY)
            {
                PositionChanged?.Invoke();
                yield return null;
            }
        }

        _moveVector = Vector3.right;
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    /*private IEnumerator TestCo(float targetY)
    {
        //Debug.Log($"{transform.position.y} : {targetY}");

        if (transform.position.y < targetY)
        {
            _moveVector = new Vector3(1f, targetY, 0);
            yield return new WaitUntil(() => transform.position.y >= targetY);
            _moveVector = Vector3.right;
            PositionChanged?.Invoke();
        }
        else
        {
            _moveVector = new Vector3(1f, -targetY, 0);
            yield return new WaitUntil(() => transform.position.y <= targetY);
            _moveVector = Vector3.right;
            PositionChanged?.Invoke();
        }

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }*/
}
