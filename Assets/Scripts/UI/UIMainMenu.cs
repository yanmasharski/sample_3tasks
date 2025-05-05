using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonAceOfShadows;
    [SerializeField] private Button buttonMagicWords;
    [SerializeField] private Button buttonPhoenixFlame;

    private void Awake()
    {
        buttonAceOfShadows.onClick.AddListener(OnButtonClickedAceOfShadows);
        buttonMagicWords.onClick.AddListener(OnButtonClickedMagicWords);
        buttonPhoenixFlame.onClick.AddListener(OnButtonClickedPhoenixFlame);
    }

    private void OnButtonClickedAceOfShadows()
    {
        SignalBus.Fire(new SignalSampleRequestedAce());
    }

    private void OnButtonClickedMagicWords()
    {
        Debug.Log("OnButtonClickedMagicWords");
    }

    private void OnButtonClickedPhoenixFlame()
    {
        Debug.Log("OnButtonClickedPhoenixFlame");
    }
    
}