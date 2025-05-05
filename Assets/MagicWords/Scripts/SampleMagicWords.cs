using UnityEngine;

public class SampleMagicWords : MonoBehaviour
{
    [SerializeField] private string apiGetDialogueUrl = "https://private-624120-softgamesassignment.apiary-mock.com/v3/magicwords";
    [SerializeField] private UIDialogue dialogue;

    public void Initialize()
    {
        SignalBus.Subscribe<SignalSampleRequestedWords>(OnSampleRequested);
        SignalBus.Subscribe<SignalSampleReset>(OnSampleReset);
        gameObject.SetActive(false);
    }

    private void OnSampleRequested(SignalSampleRequestedWords signal)
    {
        gameObject.SetActive(true);
        var dialogueLoader = new DialogueLoader();
        dialogue.Clear();
        dialogueLoader.LoadDialogue(apiGetDialogueUrl, (dataDialogue) =>
        {
            dialogue.SetData(dataDialogue.dialogue, dataDialogue.avatars);
        });
    }

    private void OnSampleReset(SignalSampleReset signal)
    {
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        SignalBus.Unsubscribe<SignalSampleRequestedWords>(OnSampleRequested);
        SignalBus.Unsubscribe<SignalSampleReset>(OnSampleReset);
    }
}