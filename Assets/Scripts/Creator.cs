using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Creator : MonoBehaviour
{
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawner;
    [SerializeField] private ActiveItem _ballPrefab;

    private ActiveItem _itemInTube;
    private ActiveItem _itemInSpawner;

    private void Start()
    {
        CreateItemInTube();
        StartCoroutine(MoveToSpawner());
    }

    private void CreateItemInTube()
    {
        int itemLevel = Random.Range(0, 5);
        _itemInTube = Instantiate(_ballPrefab, _tube.position, quaternion.identity);
        _itemInTube.SetLevel(itemLevel);
        _itemInTube.SetupToTube();
    }

    private IEnumerator MoveToSpawner()
    {
        _itemInTube.transform.parent = _spawner;
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.3f)
        {
            _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }
        _itemInTube.transform.localPosition = Vector3.zero;
        _itemInSpawner = _itemInTube;
        _itemInTube = null;
        CreateItemInTube();
    }

    private void Update()
    {
        if (_itemInSpawner)
            if (Input.GetMouseButtonUp(0))
                Drop();
    }

    private void Drop()
    {
        _itemInSpawner.Drop();
        _itemInSpawner = null;
        if (_itemInTube)
            StartCoroutine(MoveToSpawner());
    }
}