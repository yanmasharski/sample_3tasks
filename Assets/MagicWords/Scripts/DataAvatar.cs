using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class DataAvatar
{
    public event Action<DataAvatar> OnSpriteLoaded;
    [SerializeField] private string url;
    [SerializeField] private string name;
    [SerializeField] private string position;

    public DataAvatar()
    {
    }

    public DataAvatar(string name)
    {
        this.name = name;
        position = "left";
        Initialize();
    }

    public bool isValid { get; private set; }

    public Sprite sprite { get; private set; }
    public string userName => name;
    public Position layoutPosition { get; private set; }

    public void Initialize()
    {
        layoutPosition = position switch
        {
            "left" => Position.Left,
            "right" => Position.Right,
            _ => Position.Undefined
        };
        LoadSprite();
    }

    private async Task LoadSprite()
    {
        isValid = false;
        try
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load sprite: " + request.error);
                return;
            }

            var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f)
                );
            isValid = true;
            OnSpriteLoaded?.Invoke(this);
            OnSpriteLoaded = null;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public enum Position
    {
        Undefined,
        Left,
        Right
    }
}