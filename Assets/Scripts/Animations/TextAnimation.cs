    using UnityEngine;

[System.Serializable]
public enum AnimationPreset
{
    None,
    Gentle,
    Dynamic,
    Floating,
    Spinning,
    Pulsing
}

public class TextAnimation : MonoBehaviour
{
    [Header("Float Animation")]
    [SerializeField] private bool enableFloat = true;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatHeight = 0.5f;
    
    [Header("Scale Animation")]
    [SerializeField] private bool enablePulse = true;
    [SerializeField] private float pulseSpeed = 0.5f;
    [SerializeField] private float pulseScale = 0.1f;
    
    [Header("Quick Setup")]
    [SerializeField] private AnimationPreset preset = AnimationPreset.Gentle;
    [SerializeField] private bool applyPresetOnStart = true;
    
    private Vector3 startPosition;
    private Vector3 startScale;
    
    private void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
        
        if (applyPresetOnStart)
            ApplyPreset(preset);
    }
    
    private void Update()
    {
        if (enableFloat)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
        
        if (enablePulse)
        {
            float scale = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseScale;
            transform.localScale = startScale * scale;
        }
    }
    
    public void ApplyPreset(AnimationPreset preset)
    {
        switch (preset)
        {
            case AnimationPreset.None:
                StopAllAnimations();
                break;
            case AnimationPreset.Gentle:
                SetGentleAnimation();
                break;
            case AnimationPreset.Dynamic:
                SetDynamicAnimation();
                break;
            case AnimationPreset.Floating:
                SetFloatingAnimation();
                break;
            case AnimationPreset.Spinning:
                SetSpinningAnimation();
                break;
            case AnimationPreset.Pulsing:
                SetPulsingAnimation();
                break;
        }
    }
    
    private void StopAllAnimations()
    {
        enableFloat = enablePulse = false;
    }
    
    private void SetGentleAnimation()
    {
        enableFloat = true; floatSpeed = 1f; floatHeight = 0.3f;
        enablePulse = true; pulseSpeed = 2f; pulseScale = 0.05f;
    }
    
    private void SetDynamicAnimation()
    {
        enableFloat = true; floatSpeed = 3f; floatHeight = 0.8f;
        enablePulse = true; pulseSpeed = 4f; pulseScale = 0.15f;
    }
    
    private void SetFloatingAnimation()
    {
        enableFloat = true; floatSpeed = 1.5f; floatHeight = 0.5f;
        enablePulse = false;
    }
    
    private void SetSpinningAnimation()
    {
        enableFloat = false;
        enablePulse = false;
    }
    
    private void SetPulsingAnimation()
    {
        enableFloat = false;
        enablePulse = true; pulseSpeed = 3f; pulseScale = 0.1f;
    }
    
    public void StartAnimation() { enabled = true; }
    public void StopAnimation() { enabled = false; }
    public void ToggleAnimation() { enabled = !enabled; }
}
