using UnityEngine;

public class Heels : MonoBehaviour
{
    [SerializeField] private MeshGenerator _meshManager;
    [SerializeField] private Transform _rightLeg;
    [SerializeField] private Transform _leftLeg;

    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;

    private void OnEnable()
    {
        _meshManager.MeshCreated += OnMeshCreated;
    }

    private void OnDisable()
    {
        _meshManager.MeshCreated -= OnMeshCreated;
    }

    private void OnMeshCreated(GameObject heel)
    {
        heel.transform.SetParent(_rightLeg);
        heel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        heel.transform.localPosition = new Vector3(_xOffset, _yOffset, 0);

        var secondLine = Instantiate(heel, _leftLeg.position, Quaternion.identity, _leftLeg);
        secondLine.transform.localPosition = new Vector3(_xOffset, _yOffset, 0);
    }
}
