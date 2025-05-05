using DG.Tweening;
using UnityEngine;
public class CardMover
{
    private const float ANIMATION_DURATION = 1;
    private readonly Pile pileOrigin;
    private readonly Pile pileDestination;
    private readonly Transform transformTemporary;
    private Tween[] tweens = new Tween[2];
    private Card card;

    public CardMover(Pile pileOrigin, Pile pileDestination, Transform transformTemporary)
    {
        this.pileOrigin = pileOrigin;
        this.pileDestination = pileDestination;
        this.transformTemporary = transformTemporary;
    }

    ~CardMover()
    {
        foreach (var tween in tweens)
        {
            tween?.Kill();
        }
        card?.Dispose();
    }
    
    public void StartTransfer()
    {
        SelectCardAndAnimate();
    }

    private void SelectCardAndAnimate()
    {
        card = pileOrigin.PopCard();
        if (card == null)
        {
            SignalBus.Fire(new SignalCardsTransitionFinished());
            return;
        }
        
        card.transform.SetParent(transformTemporary);

        var targetPosition = pileDestination.GetNextCardPositionWorld();

        // Using DOTween to animate the card movement
        var cardTransform = card.transform;
        tweens[0] = cardTransform.DOMoveX(targetPosition.x, ANIMATION_DURATION)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                pileDestination.AddCard(card);
                card = null;
                SelectCardAndAnimate();
            });

        tweens[1] = cardTransform.DOMoveY(targetPosition.y, ANIMATION_DURATION)
            .SetEase(Ease.InBack);

    }
}