using TMPro;
using UnityEngine;

public class Projection : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Transform _visualTransform;

    public void Setup(Material material, string numberText, float radius)
    {
        _renderer.material = material;
        _text.text = numberText;
        _visualTransform.localScale = Vector3.one * radius;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}