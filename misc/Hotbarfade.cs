using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Add this namespace

[RequireComponent(typeof(CanvasGroup))]
public class HotbarFade : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float fadeDelay = 3f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] [Range(0.1f, 1f)] private float minAlpha = 0.3f;
    
    [Header("Input Settings")]
    [SerializeField] private InputActionReference hotbarInputAction; // Assign this in inspector
    [SerializeField] private InputActionAsset inputActions; // Assign your input action asset
    [SerializeField] private string targetActionMap = "Inventory"; // Name of your action map
    
    private CanvasGroup canvasGroup;
    private float timeSinceLastActivity;
    private bool isFaded = false;
    private InputActionMap inventoryActionMap;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
        // Get the specific action map
        inventoryActionMap = inputActions.FindActionMap(targetActionMap);
        
        // Enable the action map and listen for actions
        if (inventoryActionMap != null)
        {
            inventoryActionMap.Enable();
            foreach (var action in inventoryActionMap.actions)
            {
                action.performed += ctx => ResetFadeTimer();
            }
        }
        
        // Also listen to the specific hotbar input if assigned
        if (hotbarInputAction != null)
        {
            hotbarInputAction.action.performed += ctx => ResetFadeTimer();
        }
    }

    private void OnEnable()
    {
        if (inventoryActionMap != null) inventoryActionMap.Enable();
        if (hotbarInputAction != null) hotbarInputAction.action.Enable();
    }

    private void OnDisable()
    {
        if (inventoryActionMap != null) inventoryActionMap.Disable();
        if (hotbarInputAction != null) hotbarInputAction.action.Disable();
    }

    private void Update()
    {
        // Update fade timer
        if (!isFaded)
        {
            timeSinceLastActivity += Time.deltaTime;
            
            if (timeSinceLastActivity >= fadeDelay)
            {
                StartCoroutine(FadeHotbar(true));
            }
        }
    }

    public void ResetFadeTimer()
    {
        timeSinceLastActivity = 0f;
        
        if (isFaded)
        {
            StartCoroutine(FadeHotbar(false));
        }
    }

    private IEnumerator FadeHotbar(bool fadeOut)
    {
        isFaded = fadeOut;
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = fadeOut ? minAlpha : 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}