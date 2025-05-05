using System;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
public class UIReplica : MonoBehaviour
{
    // TODO: Move to a separate class
    private static readonly Dictionary<string, string> emojiReplacements = new Dictionary<string, string>
    {
        {"{satisfied}", "😌"},
        {"{intrigued}", "🤔"},
        {"{neutral}", "😐"},
        {"{affirmative}", "✅"},
        {"{laughing}", "😂"},
        {"{win}", "🏆"},
    };

    [SerializeField] private UIAvatar avatar;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private LayoutElement layoutElement;

    public float height { get; private set; }

    public void Initialize(DataMessage dataMessage, DataAvatar[] dataAvatars)
    {
        var matchingAvatars = dataAvatars.Where(avatar => avatar.userName == dataMessage.userName).ToArray();
        
        if (matchingAvatars.Length == 0)
        {
            Debug.LogWarning($"No avatar found for username: {dataMessage.userName}");
            matchingAvatars = new DataAvatar[] { new DataAvatar(dataMessage.userName) };
        }
        var message = dataMessage.message;
        foreach (var emoji in emojiReplacements)
        {
            message = message.Replace(emoji.Key, emoji.Value);
        }

        text.text = message;
        avatar.Initialize(matchingAvatars);
        height = layoutElement.preferredHeight;
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}
