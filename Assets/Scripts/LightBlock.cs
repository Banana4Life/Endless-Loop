using UnityEngine;

public class LightBlock : MonoBehaviour, Pressable
{
    public float onTime = 1.5f;
    public bool onByDefault;

    public void Start()
    {
        foreach (var light in GetComponentsInChildren<Light>())
        {
            light.enabled = onByDefault;
        }
    }

    public void Press()
    {
        foreach (var light in GetComponentsInChildren<Light>())
        {
            light.enabled = !onByDefault;
            Invoke("UnPress", onTime);
        }
    }
    
    public void UnPress()
    {
        foreach (var light in GetComponentsInChildren<Light>())
        {
            light.enabled = onByDefault;
        }
    }
}
