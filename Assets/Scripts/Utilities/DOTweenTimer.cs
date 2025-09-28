using System;
using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public static class DOTweenTimer
{
    /// <summary>
    /// Creates a timer using DOTween that executes a callback after the specified duration
    /// </summary>
    /// <param name="duration">Duration in seconds</param>
    /// <param name="onComplete">Callback to execute when timer completes</param>
    /// <returns>The DOTween Tween object for further control if needed</returns>
    public static Tween CreateTimer(float duration, Action onComplete)
    {
        return DOTween.To(() => 0f, x => { }, 1f, duration)
            .OnComplete(() => onComplete?.Invoke());
    }
    
    /// <summary>
    /// Creates a timer with a custom ease
    /// </summary>
    /// <param name="duration">Duration in seconds</param>
    /// <param name="onComplete">Callback to execute when timer completes</param>
    /// <param name="ease">Ease type for the timer</param>
    /// <returns>The DOTween Tween object for further control if needed</returns>
    public static Tween CreateTimer(float duration, Action onComplete, Ease ease)
    {
        return DOTween.To(() => 0f, x => { }, 1f, duration)
            .SetEase(ease)
            .OnComplete(() => onComplete?.Invoke());
    }
    
    /// <summary>
    /// Creates a timer with progress callback
    /// </summary>
    /// <param name="duration">Duration in seconds</param>
    /// <param name="onProgress">Callback executed during timer progress (0-1)</param>
    /// <param name="onComplete">Callback to execute when timer completes</param>
    /// <returns>The DOTween Tween object for further control if needed</returns>
    public static Tween CreateTimer(float duration, Action<float> onProgress, Action onComplete)
    {
        return DOTween.To(() => 0f, x => onProgress?.Invoke(x), 1f, duration)
            .OnComplete(() => onComplete?.Invoke());
    }
    }
}
