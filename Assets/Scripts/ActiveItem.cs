using TMPro;
using UnityEngine;

public class ActiveItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private SphereCollider _trigger;

    private int _level;
    private float _radius;

    [ContextMenu("IncreaseLevel")]
    public void IncreaseLevel()
    {
        _level++;
        SetLevel(_level);
    }

    public void SetLevel(int level)
    {
        _level = level;

        int number = (int) Mathf.Pow(2, level + 1);
        string numberString = number.ToString();
        _levelText.text = numberString;

        _radius = Mathf.Lerp(0.4f, 0.7f, _level / 10f);
        Vector3 ballScale = Vector3.one * _radius * 2f;
        _visualTransform.localScale = ballScale;
        _collider.radius = _radius;
        _trigger.radius = _radius + 0.1f;
    }
}