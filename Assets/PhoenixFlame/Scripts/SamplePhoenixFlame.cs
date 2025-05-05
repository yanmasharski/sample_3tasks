using UnityEngine;
using DG.Tweening;
public class SamplePhoenixFlame : MonoBehaviour
{
    [SerializeField] private Material material;

    [SerializeField] private Color[] colors;
    [SerializeField] private Animator animator;

    public void Initialize()
    {
        SignalBus.Subscribe<SignalSampleRequestedFire>(OnSampleRequestedFire);
        SignalBus.Subscribe<SignalSampleReset>(OnSampleReset);
        SignalBus.Subscribe<SignalFireTrigger>(OnFireTrigger);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<SignalSampleRequestedFire>(OnSampleRequestedFire);
        SignalBus.Unsubscribe<SignalSampleReset>(OnSampleReset);
        SignalBus.Unsubscribe<SignalFireTrigger>(OnFireTrigger);
    }

    private void OnSampleRequestedFire(SignalSampleRequestedFire signal)
    {
        gameObject.SetActive(true);

        int colorIndex = 0;
        NextColor();

        void NextColor()
        {
            material.DOColor(colors[colorIndex], "_TintColor", 5f)
                    .OnComplete(() => {
                        colorIndex = (colorIndex + 1) % colors.Length;
                        NextColor();
                    });
        }
    }

    private void OnSampleReset(SignalSampleReset signal)
    {
        gameObject.SetActive(false);
        animator.SetTrigger("Reset");
    }

    private void OnFireTrigger(SignalFireTrigger signal)
    {
        if (signal.isOn)
        {
            animator.SetTrigger("Fire");
        }
        else
        {
            animator.SetTrigger("Reset");
        }
    }
}
