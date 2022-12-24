using Unity.Mathematics;
using UnityEngine;

[SelectionBase]
public class Barrel : PassiveItem
{
    [SerializeField] private ParticleSystem _barrelExplosion;
    
    public override void OnAffect()
    {
        base.OnAffect();
        Die();
    }

    [ContextMenu("Die")]
    private void Die()
    {
        Instantiate(_barrelExplosion, transform.position, quaternion.Euler(-90,0,0));
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(ItemType, transform.position);
    }
}