using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreElement : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _currentScore;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _iconTransform;
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private int _level;
    [SerializeField] private GameObject _flyingIconPrefab;

    public ItemType ItemType => _itemType;
    public int CurrentScore => _currentScore;
    public int Level => _level;
    public GameObject FlyingIconPrefab => _flyingIconPrefab;
    public Transform IconTransform => _iconTransform;

    [ContextMenu("AddOne")]
    public void AddOne()
    {
        _currentScore--;
        if (_currentScore < 0)
            _currentScore = 0;

        _text.text = _currentScore.ToString();

        StartCoroutine(AddAnimation());
        //ScoreManager.Instance.CheckWin();
    }

    public virtual void Setup(Task task)
    {
        _currentScore = task.Number;
        _text.text = task.Number.ToString();
        _level = task.Level;
    }

    private IEnumerator AddAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime * 1.8f)
        {
            float scale = _scaleCurve.Evaluate(t);
            _iconTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        _iconTransform.localScale = Vector3.one;
    }
}