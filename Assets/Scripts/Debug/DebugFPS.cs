using UnityEngine;

public class DebugFPS : MonoBehaviour
{

    /// <summary>
    /// Update interval in seconds to calculate FPS
    /// </summary>
    [SerializeField] private float updateIntervalSeconds = 1f;

    private float fpsInInterval;
    private float fpsActual;
    private float accum = 0.0f;
    private int frames = 0;
    private float timeleft;


#if !DEBUG
    private void Awake()
    {
        Destroy(this);
    }
#endif

    private void Start()
    {
        timeleft = updateIntervalSeconds;
    }

    private void Update()
    {
        timeleft -= Time.deltaTime;
        fpsActual = 1f / Time.unscaledDeltaTime;
        accum += fpsActual;
        frames++;

        if (timeleft <= 0.0f)
        {
            fpsInInterval = accum / frames;
            timeleft = updateIntervalSeconds;
            accum = 0.0f;
            frames = 0;
        }
    }

    private void OnGUI()
    {
        var style = new GUIStyle();
        style.fontSize = 26;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(30, 20, 160, 30), "FPS: " + fpsInInterval.ToString("F1") + " (" + fpsActual.ToString("F1") + ")", style);
    }
}
