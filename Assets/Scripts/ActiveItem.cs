using TMPro;
using UnityEngine;

[SelectionBase]
public class ActiveItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private SphereCollider _trigger;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] [Min(0)] private float _dropForce = 1.2f;

    private int _level;
    private float _radius;
    
    public Rigidbody Rigidbody => _rigidbody;
    public bool IsDead { get; private set; }
    
    
    public int Level => _level;

    private void OnTriggerEnter(Collider other)
    {
        if(IsDead) return;
        
        ActiveItem otherItem = other.attachedRigidbody?.GetComponent<ActiveItem>();
        if (otherItem)
            if (!otherItem.IsDead && _level == otherItem.Level)
                CollapseManager.Instance.Collapse(this, otherItem);
    }

    [ContextMenu("IncreaseLevel")]
    public void IncreaseLevel()
    {
        _level++;
        SetLevel(_level);
        _trigger.enabled = false;
        Invoke(nameof(EnableTrigger), 0.08f);
        _animator.SetTrigger(nameof(IncreaseLevel));
    }

    public virtual void SetLevel(int level)
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

    public void SetupToTube()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        _trigger.enabled = true;
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        transform.parent = null;
        _rigidbody.velocity = Vector3.down * _dropForce;
    }

    public void Disable()
    {
        _trigger.enabled = false;
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        IsDead = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void EnableTrigger() =>
        _trigger.enabled = true;
}