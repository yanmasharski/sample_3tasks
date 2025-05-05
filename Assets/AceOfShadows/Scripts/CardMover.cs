using DG.Tweening;

public class CardMover
{
    private const float ANIMATION_DURATION = 1;
    private readonly Pile pileOrigin;
    private readonly Pile pileDestination;

    public CardMover(Pile pileOrigin, Pile pileDestination)
    {
        this.pileOrigin = pileOrigin;
        this.pileDestination = pileDestination;
    }
    
    public void StartTransfer()
    {
        SelectCardAndAnimate();
    }

    private void SelectCardAndAnimate()
    {
        var card = pileOrigin.PopCard();
        if (card == null)
        {
            SignalBus.Fire(new SignalSampleAceFinished());
            return;
        }

        var targetPosition = pileDestination.GetNextCardPositionWorld();

        // Using DOTween to animate the card movement
        var cardTransform = card.transform;
        cardTransform.DOMoveX(targetPosition.x, ANIMATION_DURATION)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                pileDestination.AddCard(card);
                SelectCardAndAnimate();
            });

        cardTransform.DOMoveY(targetPosition.y, ANIMATION_DURATION)
            .SetEase(Ease.InBack);

    }
}