using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonFire:MonoBehaviour
{
    [SerializeField] private Sprite spriteFireOn;
    private Button button;

    private bool isFire = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        isFire = !isFire;
        SignalBus.Fire(new SignalFireTrigger(isFire));
        button.image.overrideSprite = isFire ? spriteFireOn : null;
    }
}