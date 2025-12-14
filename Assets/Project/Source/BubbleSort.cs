using UnityEngine;
using UnityEngine.UI;

public class BubbleSort : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Button _button;
    [SerializeField] private float randomRadius = 10f;
    [SerializeField] private float maxScale = 5f;
    [SerializeField] private Vector3 direction = Vector3.up;
    [SerializeField] private float spacing = 2f;
    private GameObject[] _objects;

    private void Awake()
    {
        _button.onClick.AddListener(Sort);
    }

    private void Start()
    {
        _objects = new GameObject[10];
        for (int i = 0; i < _objects.Length; i++)
        {
            _objects[i] = Instantiate(_prefab);
            _objects[i].transform.position = new Vector3(Random.Range(-randomRadius, randomRadius), Random.Range(-randomRadius, randomRadius), 0f);
            _objects[i].transform.localScale = Vector3.one * Random.Range(1f, maxScale);
        }
    }

    private void Sort()
    {
        while (!IsCompletelySorted(_objects))
        {
            for (var i = 1; i < _objects.Length; i++)
            {
                var past = _objects[i - 1];
                var present = _objects[i];
                if (present.transform.localScale.magnitude < past.transform.localScale.magnitude)
                {
                    _objects[i - 1] = present;
                    _objects[i] = past;
                }
            }
        }

        for (var i = 0; i < _objects.Length; i++)
        {
            _objects[i].transform.position = direction * (i * maxScale) + direction * spacing;
        }
    }

    private bool IsCompletelySorted(GameObject[] objects)
    {
        for (var i = 1; i < objects.Length; i++)
        {
            var past = _objects[i - 1];
            var present = _objects[i];
            if (present.transform.localScale.magnitude < past.transform.localScale.magnitude) return false;
        }
        
        return true;
    }
}