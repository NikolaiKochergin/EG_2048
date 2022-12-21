using UnityEngine;

[SelectionBase]
public class Box : PassiveItem
{
    [SerializeField] [Range(0,2)] private int _health = 1;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private ParticleSystem _breakEffectPrefab;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        SetHealth(_health);
    }

    [ContextMenu("OnAffect")]
    public override void OnAffect()
    {
        base.OnAffect();
        _health -= 1;
        Instantiate(_breakEffectPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        _animator.SetTrigger("Shake");
        if(_health < 0)
            Die();
        else
            SetHealth(_health);
    }

    private void SetHealth(int health)
    {
        for (int i = 0; i < _levels.Length; i++)
            _levels[i].SetActive(i <= health);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}