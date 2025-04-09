using UnityEngine;

public class RainbowBackground : MonoBehaviour
{
    // List of rainbow colors
    private Color[] rainbowColors = new Color[]
    {
        Color.red,
        new Color(1f, 0.5f, 0f), // Orange
        Color.yellow,
        Color.green,
        Color.blue,
        new Color(0.29f, 0f, 0.51f), // Indigo
        new Color(0.56f, 0f, 1f) // Violet
    };

    [SerializeField] private float colorChangeSpeed = 1.0f; // Speed of color transition (adjustable in Inspector)

    private Camera cam;
    private int currentColorIndex = 0;
    private int nextColorIndex = 1;
    private float t = 0f;

    void Start()
    {
        cam = Camera.main; // Get main camera
        if (cam == null)
        {
            Debug.LogError("No main camera found in the scene!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // Lerp between current color and next color
        cam.backgroundColor = Color.Lerp(rainbowColors[currentColorIndex], rainbowColors[nextColorIndex], t);
        t += Time.deltaTime * colorChangeSpeed;

        // When we reach the next color, move to the following color
        if (t >= 1f)
        {
            t = 0f;
            currentColorIndex = nextColorIndex;
            nextColorIndex = (nextColorIndex + 1) % rainbowColors.Length; // Loop around
        }
    }
}
