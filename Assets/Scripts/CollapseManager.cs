using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CollapseManager : MonoBehaviour
{
    public UnityEvent OnCollapse;
    
    public static CollapseManager Instance;

    private void Awake() =>
        Instance = this;

    public void Collapse(ActiveItem itemA, ActiveItem itemB)
    {
        ActiveItem toItem;
        ActiveItem fromItem;

        if (Mathf.Abs(itemA.transform.position.y - itemB.transform.position.y) > 0.02f)
        {
            if (itemA.transform.position.y > itemB.transform.position.y)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }
        else
        {
            if (itemA.Rigidbody.velocity.magnitude > itemB.Rigidbody.velocity.magnitude)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }
        
        StartCoroutine(CollapseProcess(fromItem, toItem));
    }

    private IEnumerator CollapseProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        fromItem.Disable();

        if (fromItem.ItemType == ItemType.Ball || toItem.ItemType == ItemType.Ball)
        {
            Vector3 startPosition = fromItem.transform.position;
            for (float t = 0f; t < 1f; t += Time.deltaTime / 0.08f)
            {
                fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, t);
                yield return null;
            }
        }
        fromItem.transform.position = toItem.transform.position;

        if (fromItem.ItemType == ItemType.Ball && toItem.ItemType == ItemType.Ball)
        {
            fromItem.Die();
            toItem.DoEffect();
            ExplodeBall(toItem.transform.position,toItem.Radius + 0.1f);
        }
        else
        {
            if(fromItem.ItemType == ItemType.Ball)
                fromItem.Die();
            else
                fromItem.DoEffect();
            
            if (toItem.ItemType == ItemType.Ball) 
                toItem.Die();
            else
               toItem.DoEffect();
        }
        
        OnCollapse.Invoke();
    }

    private void ExplodeBall(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (var collider in colliders)
        {
            collider.attachedRigidbody?.GetComponent<PassiveItem>()?.OnAffect();
            collider.GetComponent<PassiveItem>()?.OnAffect();
        }
    }
}