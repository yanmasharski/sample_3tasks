using UnityEngine;
using TMPro;
public class DebugFPS : MonoBehaviour
{

    /// <summary>
    /// Update interval in seconds to calculate FPS
    /// </summary>
    [SerializeField] private float updateIntervalSeconds = 1f;

    [SerializeField] private TextMeshProUGUI label;

    private float fpsInInterval;
    private float fpsActual;
    private float accum = 0.0f;
    private int frames = 0;
    private float timeleft;


// #if !DEBUG
//     private void Awake()
//     {
//         Destroy(this);
//     }
// #endif

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

        if (label == null)
        {
            return;
        }

        label.text = "FPS: " + fpsInInterval.ToString("F1") + " (" + fpsActual.ToString("F1") + ")";
    }
}
