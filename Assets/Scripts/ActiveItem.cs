using TMPro;
using UnityEngine;

[SelectionBase]
public class ActiveItem : Item
{
    [field: SerializeField] protected int Level { get; private set; }
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private SphereCollider _trigger;
    [SerializeField] private Animator _animator;
    [SerializeField] private Projection _projection;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] [Min(0)] private float _dropForce = 1.2f;

    public Rigidbody Rigidbody => _rigidbody;
    protected TMP_Text LevelText => _levelText;
    public Projection Projection => _projection;
    protected SphereCollider Collider => _collider;
    protected SphereCollider Trigger => _trigger;
    protected Animator Animator => _animator;
    public float Radius { get; protected set; }
    private bool IsDead { get; set; }

    protected virtual void Start()
    {
        _projection.Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(IsDead) return;
        
        ActiveItem otherItem = other.attachedRigidbody?.GetComponent<ActiveItem>();
        if (otherItem)
            if (!otherItem.IsDead && Level == otherItem.Level)
                CollapseManager.Instance.Collapse(this, otherItem);
    }

    [ContextMenu("IncreaseLevel")]
    public void IncreaseLevel()
    {
        Level++;
        SetLevel(Level);
        _trigger.enabled = false;
        Invoke(nameof(EnableTrigger), 0.08f);
        _animator.SetTrigger(nameof(IncreaseLevel));
    }

    public virtual void SetLevel(int level)
    {
        Level = level;

        int number = (int) Mathf.Pow(2, level + 1);
        string numberString = number.ToString();
        _levelText.text = numberString;
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
    
    public virtual void DoEffect(){}

    private void EnableTrigger() =>
        _trigger.enabled = true;
}