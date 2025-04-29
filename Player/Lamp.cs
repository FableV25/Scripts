using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : MonoBehaviour, IWeapon
{

    [SerializeField] private weaponInfo weaponInfo;
    [SerializeField] private Light2D lampLight; 
    [SerializeField] private float fadeDuration = 0.2f;

    private Coroutine fadeCoroutine;
    private bool lightIsOn = true;

    private void Start()
    {
        if (lampLight == null)
            lampLight = GetComponentInChildren<Light2D>();
    }



    public void Attack()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (lightIsOn)
        {
            fadeCoroutine = StartCoroutine(FadeLightOut());
        }
        else
        {
            lampLight.intensity = 1f;
            lampLight.enabled = true;
        }

        lightIsOn = !lightIsOn;
    }
    private IEnumerator FadeLightOut()
    {
        float startIntensity = lampLight.intensity;
        float time = 0f;

        while (time < fadeDuration)
        {
            lampLight.intensity = Mathf.Lerp(startIntensity, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        lampLight.intensity = 0f;
        lampLight.enabled = false;
    }


    private void Update()
    {
        mouseFollowWithOffset();
    }



    public weaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void mouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            activeWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        } else {
            activeWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}