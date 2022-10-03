using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    
    public GameObject[] pressables;
    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerCollider>();
        if (player)
        {
            Press();
        }
    }

    private void Press()
    {
        foreach (var pressable in pressables)
        {
            var component = pressable.GetComponent<Pressable>();
            if (component != null)
            {
                component.Press();
            }
        }
    }
}
