using UnityEngine;

public class Heels : MonoBehaviour
{
    [SerializeField] private MeshGenerator _meshManager;
    [SerializeField] private Transform _rightLeg;
    [SerializeField] private Transform _leftLeg;

    private GameObject _rightLegContainer;
    private GameObject _leftLegContainer;

    private void Start()
    {
        InstantiateContainers();
    }

    private void OnEnable()
    {
        _meshManager.MeshCreated += OnMeshCreated;
    }

    private void OnDisable()
    {
        _meshManager.MeshCreated -= OnMeshCreated;
    }

    private void InstantiateContainers()
    {
        _rightLegContainer = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        _leftLegContainer = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
    }

    private void OnMeshCreated(GameObject rightHeel, GameObject leftHeel)
    {
        TryDestroyOldHeels();

        _rightLegContainer.transform.position = rightHeel.GetComponent<Renderer>().bounds.center;
        _leftLegContainer.transform.position = leftHeel.GetComponent<Renderer>().bounds.center;

        rightHeel.transform.SetParent(_rightLegContainer.transform);
        leftHeel.transform.SetParent(_leftLegContainer.transform);

        _rightLegContainer.transform.SetParent(_rightLeg);
        _leftLegContainer.transform.SetParent(_leftLeg);

        _rightLegContainer.transform.localPosition = Vector3.zero;
        _leftLegContainer.transform.localPosition = Vector3.zero;

        _rightLegContainer.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _leftLegContainer.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    private void TryDestroyOldHeels()
    {
        if(_rightLeg.childCount == 0)
        {
            return;
        }

        for(int i = 0; i < _rightLeg.childCount; i++)
        {
            Destroy(_rightLeg.GetChild(0).gameObject);
            Destroy(_leftLeg.GetChild(0).gameObject);
        }

        InstantiateContainers();
    }
}