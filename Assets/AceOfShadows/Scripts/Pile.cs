using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pile : MonoBehaviour
{
    [SerializeField] private float padding = 0.1f;
    [SerializeField] private TextMeshPro textMeshPro;

    private readonly List<Card> cards = new List<Card>();


    public Vector3 GetNextCardPositionWorld()
    {
        return transform.TransformPoint(GetNextCardPosition());
    }

    public void Clear()
    {
        foreach (var card in cards)
        {
            card.Dispose();
        }
        cards.Clear();
        UpdateText();
    }

    public void AddCard(Card card)
    {
        card.transform.SetParent(transform);
        card.transform.localPosition = GetNextCardPosition();
        cards.Add(card);
        UpdateText();
    }

    public Card PopCard()
    {
        if (cards.Count == 0)
        {
            return null;
        }

        var index = cards.Count - 1;
        var card = cards[index];
        cards.RemoveAt(index);
        card.transform.SetParent(null);
        UpdateText();
        return card;
    }

    private Vector3 GetNextCardPosition()
    {
        return new Vector3(0, -cards.Count * padding, -0.01f * cards.Count);
    }

    private void UpdateText()
    {
        textMeshPro.text = cards.Count.ToString();
    }
}