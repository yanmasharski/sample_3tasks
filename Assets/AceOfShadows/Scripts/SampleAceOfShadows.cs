using UnityEngine;
using TMPro;
public class SampleAceOfShadows : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private int initialCardCount;
    [SerializeField] private Pile pileOrigin;
    [SerializeField] private Pile pileDestination;
    [SerializeField] private TextMeshPro labelFinishMessage;

    private CardMover cardMover;

    public void Initialize()
    {
        SignalBus.Subscribe<SignalSampleRequestedAce>(OnSampleRequested);
        SignalBus.Subscribe<SignalSampleReset>(OnSampleReset);
        SignalBus.Subscribe<SignalCardsTransitionFinished>(OnSampleAceFinished);
        gameObject.SetActive(false);
    }

    private void OnSampleRequested(SignalSampleRequestedAce signal)
    {
        labelFinishMessage.enabled = false;
        gameObject.SetActive(true);

        // Object pool could be used here instead of destroying and instantiating
        pileOrigin.Clear();
        pileDestination.Clear();

        for (int i = 0; i < initialCardCount; i++)
        {
            var card = Instantiate(cardPrefab);
            card.Initialize(i);
            pileOrigin.AddCard(card);
        }

        cardMover = new CardMover(pileOrigin, pileDestination, transform);
        cardMover.StartTransfer();
    }

    private void OnSampleReset(SignalSampleReset signal)
    {
        pileOrigin.Clear();
        pileDestination.Clear();
        cardMover = null;
        gameObject.SetActive(false);
        labelFinishMessage.enabled = false;
    }

    private void OnSampleAceFinished(SignalCardsTransitionFinished signal)
    {
        labelFinishMessage.enabled = true;
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<SignalSampleRequestedAce>(OnSampleRequested);
        SignalBus.Unsubscribe<SignalSampleReset>(OnSampleReset);
        SignalBus.Unsubscribe<SignalCardsTransitionFinished>(OnSampleAceFinished);
    }
}
