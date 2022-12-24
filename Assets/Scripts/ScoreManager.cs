using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private ScoreElement[] _scoreElementPrefabs;
    [SerializeField] private Transform _itemScoreParent;
    [SerializeField] private Camera _camera;
    
    private ScoreElement[] _scoreElements;
    public static ScoreManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _scoreElements = new ScoreElement[_level.Tasks.Count];
        for (int taskIndex = 0; taskIndex < _level.Tasks.Count; taskIndex++)
        {
            Task task = _level.Tasks[taskIndex];
            ItemType itemType = task.ItemType;
            for (int i = 0; i < _scoreElementPrefabs.Length; i++)
            {
                if (itemType == _scoreElementPrefabs[i].ItemType)
                {
                    ScoreElement newScoreElement = Instantiate(_scoreElementPrefabs[i], _itemScoreParent);
                    newScoreElement.Setup(task);
                    _scoreElements[taskIndex] = newScoreElement;
                }
            }
        }
    }

    public bool AddScore(ItemType itemType, Vector3 position, int level = 0)
    {
        for (int i = 0; i < _scoreElements.Length; i++)
        {
            if(_scoreElements[i].ItemType != itemType) continue;
            if(_scoreElements[i].CurrentScore == 0) continue;
            if(_scoreElements[i].Level != level) continue;

            StartCoroutine(AddScoreAnimation(_scoreElements[i], position));
            return true;
        }

        return false;
    }

    public void CheckWin()
    {
        for (int i = 0; i < _scoreElements.Length; i++)
            if(_scoreElements[i].CurrentScore != 0)
                return;
        Debug.Log("Win");
    }

    private IEnumerator AddScoreAnimation(ScoreElement scoreElement, Vector3 position)
    {
        GameObject icon = Instantiate(scoreElement.FlyingIconPrefab, position, Quaternion.identity);
        Vector3 a = position;
        Vector3 b = position + Vector3.back * 6.5f + Vector3.down * 5f;
        Vector3 screenPosition = new Vector3(scoreElement.IconTransform.position.x,
            scoreElement.IconTransform.position.y, -_camera.transform.position.z);
        Vector3 d = _camera.ScreenToWorldPoint(screenPosition);
        Vector3 c = d + Vector3.back * 6f;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            icon.transform.position = Bezier.GetPoint(a, b, c, d, t);
            yield return null;
        }
        Destroy(icon.gameObject);
        scoreElement.AddOne();
        CheckWin();
    }
}