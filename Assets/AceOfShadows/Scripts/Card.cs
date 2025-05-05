using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro textMeshPro;

    public void Dispose()
    {
        Destroy(gameObject);
    }

    public void Initialize(int value)
    {
        textMeshPro.text = (value + 1).ToString("000");
    }
}
