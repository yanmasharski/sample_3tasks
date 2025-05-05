using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class UIDialogue : MonoBehaviour
{
    [SerializeField] private UIReplica replicaPrefab;
    [SerializeField] private VerticalLayoutGroup dialogueContainer;

    private List<UIReplica> replicas = new List<UIReplica>();   

    public void Clear()
    {
        foreach (var replica in replicas)
        {
            replica.Dispose();
        }
        replicas.Clear();
    }

    public void SetData(DataMessage[] dataMessages, DataAvatar[] dataAvatars)
    {
        foreach (var dataMessage in dataMessages)
        {
            var replica = Instantiate(replicaPrefab, dialogueContainer.transform);
            replica.Initialize(dataMessage, dataAvatars);
            replicas.Add(replica);
        }

        // Calculate total height based on replicas and container padding
        float totalHeight = 0;
        
        // Add up heights of all replicas
        foreach (var replica in replicas)
        {
            totalHeight += replica.height;
        }
        
        // Add padding between elements
        if (replicas.Count > 1 && dialogueContainer)
        {
            totalHeight += dialogueContainer.spacing * (replicas.Count - 1);
            totalHeight += dialogueContainer.padding.top + dialogueContainer.padding.bottom;
        }
        
        // Apply the calculated height to the container's RectTransform
        RectTransform containerRect = dialogueContainer.GetComponent<RectTransform>();
        if (containerRect != null)
        {
            Vector2 sizeDelta = containerRect.sizeDelta;
            sizeDelta.y = totalHeight;
            containerRect.sizeDelta = sizeDelta;
        }

    }
}
