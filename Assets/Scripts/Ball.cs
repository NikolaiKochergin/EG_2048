using UnityEngine;

public class Ball : ActiveItem
{
    [SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _visualTransform;

    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        _renderer.material = _ballSettings.BallMaterials[level];

        Radius = Mathf.Lerp(0.4f, 0.7f, Level / 10f);
        Vector3 ballScale = Vector3.one * Radius * 2f;
        _visualTransform.localScale = ballScale;
        Collider.radius = Radius;
        Trigger.radius = Radius + 0.1f;
        
        Projection.Setup(_ballSettings.BallProjectionMaterials[level], LevelText.text, Radius * 2f);
    }

    public override void DoEffect()
    {
        base.DoEffect();
        IncreaseLevel();
    }
}