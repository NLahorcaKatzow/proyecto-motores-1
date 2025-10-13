using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Image corazones; // Filled image for hearts
    public Image magna;     // Filled image for mana

    [Header("Animation Settings")]
    public float animationDuration = 0.5f; // Duration for fill animations
    public Ease animationEase = Ease.OutQuad; // Easing for smooth animations

    public MagnaController magnaController;

    void Start()
    {
        magnaController.onMagnaChanged += SetMagna;
        HealthController.Instance.onHealthChanged += SetCorazones;
    }

    /// <summary>
    /// Setup method to initialize the UI values
    /// </summary>
    /// <param name="corazonesValue">Initial value for corazones (0-1)</param>
    /// <param name="magnaValue">Initial value for magna (0-1)</param>
    public void Setup(int corazonesValue, float magnaValue)
    {
        SetCorazones(corazonesValue);
        SetMagna(magnaValue);
    }

    /// <summary>
    /// Sets the fill amount for corazones image with smooth animation
    /// </summary>
    /// <param name="value">Health value</param>
    public void SetCorazones(int value)
    {
        if (corazones != null)
        {
            corazones.fillAmount = Mathf.Clamp01(value / 3f);
        }
        else
        {
            Debug.LogWarning("Corazones image is not assigned!");
        }
    }

    /// <summary>
    /// Sets the fill amount for magna image with smooth animation
    /// </summary>
    /// <param name="value">Magna value</param>
    public void SetMagna(float value)
    {
        if (magna != null)
        {
            float targetFillAmount = Mathf.Clamp01(value/100f);
            
            // Kill any existing tween on this image
            magna.DOKill();
            
            // Animate the fillAmount to the target value
            magna.DOFillAmount(targetFillAmount, animationDuration)
                .SetEase(animationEase);
        }
        else
        {
            Debug.LogWarning("Magna image is not assigned!");
        }
    }
}
