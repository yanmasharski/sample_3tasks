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

        cardMover = new CardMover(pileOrigin, pileDestination);
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

    private void OnSampleAceFinished(SignalSampleAceFinished signal)
    {
        labelFinishMessage.enabled = true;
    }

    private void Awake()
    {
        SignalBus.Subscribe<SignalSampleRequestedAce>(OnSampleRequested);
        SignalBus.Subscribe<SignalSampleReset>(OnSampleReset);
        SignalBus.Subscribe<SignalSampleAceFinished>(OnSampleAceFinished);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<SignalSampleRequestedAce>(OnSampleRequested);
        SignalBus.Unsubscribe<SignalSampleReset>(OnSampleReset);
        SignalBus.Unsubscribe<SignalSampleAceFinished>(OnSampleAceFinished);
    }
}
