using System.Collections;
using UnityEngine;

public class Question : ActiveItem
{
    [Header("Question")]
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private Item[] _itemPrefabs;

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
    }

    private IEnumerator AffectProcess()
    {
        Animator.enabled = true;
        yield return new WaitForSeconds(1f);

        if (_itemPrefabs.Length > 0)
        {
            int index = Random.Range(0, _itemPrefabs.Length);
            Instantiate(_itemPrefabs[index], transform.position, Quaternion.identity);
        }
        
        Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
