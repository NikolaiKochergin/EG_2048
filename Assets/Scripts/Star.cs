using System.Collections;
using UnityEngine;

public class Star : ActiveItem
{
    [Header("Star")] 
    [SerializeField] private float _affectRadius = 1.5f;
    [SerializeField] private GameObject _affectArea;
    [SerializeField] private GameObject _effectPrefab;

    protected override void Start()
    {
        base.Start();
        _affectArea.SetActive(false);
    }

    private void OnValidate()
    {
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;
    }

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
    }

    private IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        Animator.enabled = true;
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius);
        foreach (var collider in colliders)
            collider.attachedRigidbody?.GetComponent<ActiveItem>()?.IncreaseLevel();
        
        Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}