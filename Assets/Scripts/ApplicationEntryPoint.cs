using UnityEngine;

/// <summary>
/// Entry point of the application. Like Main.cs
/// Should be the first game script to execute in script execution order (project settings)
/// </summary>
public class ApplicationEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private SampleAceOfShadows sampleAceOfShadows;
    [SerializeField] private SampleMagicWords sampleMagicWords;
    // [SerializeField] private SamplePhoenixFlame samplePhoenixFlame;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        uiMainMenu.Initialize();
        sampleAceOfShadows.Initialize();
        sampleMagicWords.Initialize();
    }
    
}
