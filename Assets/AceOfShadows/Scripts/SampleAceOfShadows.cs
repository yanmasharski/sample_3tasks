using UnityEngine;

public class SampleAceOfShadows : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private int initialCardCount;
    [SerializeField] private Pile pileOrigin;
    [SerializeField] private Pile pileDestination;

    private CardMover cardMover;

    private void RunSample()
    {
        // Object pool could be used here instead of destroying and instantiating
        pileOrigin.Clear();
        pileDestination.Clear();

        for (int i = 0; i < initialCardCount; i++)
        {
            var card = Instantiate(cardPrefab);
            card.Initialize(i);
            pileOrigin.AddCard(card);
        }

        cardMover = new CardMover(pileOrigin, pileDestination);
        cardMover.StartTransfer();
    }

    private void OnSampleRequested(SignalSampleRequestedAce signal)
    {
        RunSample();
    }

    private void Awake()
    {
        SignalBus.Subscribe<SignalSampleRequestedAce>(OnSampleRequested);
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<SignalSampleRequestedAce>(OnSampleRequested);
    }
}
