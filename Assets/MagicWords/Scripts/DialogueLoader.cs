using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
public class DialogueLoader
{
    public async Task<DataDialogue> LoadDialogue(string url, Action<DataDialogue> onLoaded)
    {
        try
        {
            var request = UnityWebRequest.Get(url);
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load dialogue: {request.error}");
                return null;
            }
            var dataDialogue = JsonUtility.FromJson<DataDialogue>(request.downloadHandler.text);
            foreach (var avatar in dataDialogue.avatars)
            {
                avatar.Initialize();
            }

            // Check for duplicate avatar names
            var avatars = dataDialogue.avatars;
            for (int i = 0; i < avatars.Length; i++)
            {
                for (int j = i + 1; j < avatars.Length; j++)
                {
                    if (avatars[i].userName == avatars[j].userName)
                    {
                        Debug.LogWarning($"Duplicate avatar name found: {avatars[i].userName}");
                    }
                }
            }
            
            onLoaded?.Invoke(dataDialogue);
            return dataDialogue;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }

    [Serializable]
    public class DataDialogue
    {
        public DataMessage[] dialogue;
        public DataAvatar[] avatars;
    }


}