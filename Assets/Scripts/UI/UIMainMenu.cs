using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonAceOfShadows;
    [SerializeField] private Button buttonMagicWords;
    [SerializeField] private Button buttonPhoenixFlame;

    public void Initialize()
    {
        buttonAceOfShadows.onClick.RemoveAllListeners();
        buttonAceOfShadows.onClick.AddListener(OnButtonClickedAceOfShadows);
        buttonMagicWords.onClick.RemoveAllListeners();
        buttonMagicWords.onClick.AddListener(OnButtonClickedMagicWords);
        buttonPhoenixFlame.onClick.RemoveAllListeners();
        buttonPhoenixFlame.onClick.AddListener(OnButtonClickedPhoenixFlame);
    }

    private void OnButtonClickedAceOfShadows()
    {
        SignalBus.Fire(new SignalSampleReset());
        SignalBus.Fire(new SignalSampleRequestedAce());
    }

    private void OnButtonClickedMagicWords()
    {
        SignalBus.Fire(new SignalSampleReset());
        SignalBus.Fire(new SignalSampleRequestedWords());
    }

    private void OnButtonClickedPhoenixFlame()
    {
        SignalBus.Fire(new SignalSampleReset());
        SignalBus.Fire(new SignalSampleRequestedFire());
    }

}