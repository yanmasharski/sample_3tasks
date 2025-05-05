using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAvatar : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;

    public void Initialize(DataAvatar[] dataAvatars)
    {
        foreach (var dataAvatar in dataAvatars)
        {
            dataAvatar.OnSpriteLoaded += OnSpriteLoaded;
            text.text = dataAvatar.userName;
            OnSpriteLoaded(dataAvatar);

        }
    }

    private void OnSpriteLoaded(DataAvatar dataAvatar)
    {
        if (dataAvatar.isValid)
        {
            image.overrideSprite = dataAvatar.sprite;
        }

        // Set the avatar position in a very dirty way (based on layout group rebuilding)
        switch (dataAvatar.layoutPosition)
        {
            case DataAvatar.Position.Left:
                transform.SetAsFirstSibling();
                break;
            case DataAvatar.Position.Right:
                transform.SetAsLastSibling();
                break;
            default:
                Debug.LogException(new Exception($"Invalid avatar position: {dataAvatar.layoutPosition}"));
                break;
        }
    }
}