using Unity.Mathematics;
using UnityEngine;

[SelectionBase]
public class Stone : PassiveItem
{
    [SerializeField] private ParticleSystem _dieEffect;
    [Range(0, 2)] [SerializeField] private int _level = 2;
    [SerializeField] private Transform _visualTransfor;
    [SerializeField] private Stone _stonePrefab;

    public override void OnAffect()
    {
        base.OnAffect();
        if (_level > 0)
            for (int i = 0; i < 2; i++)
                CreateChildRock(_level - 1);
        Die();
    }

    public void SetLevel(int level)
    {
        _level = level;
        float scale = 1;
        if (_level == 2)
            scale = 1f;
        else if (level == 1)
            scale = .7f;
        else if (level == 0)
            scale = .45f;

        _visualTransfor.localScale = Vector3.one * scale;
    }

    private void CreateChildRock(int level)
    {
        Stone newRock = Instantiate(_stonePrefab, transform.position, quaternion.identity);
        newRock.SetLevel(level);
    }

    private void Die()
    {
        Instantiate(_dieEffect, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}