using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public string triggerTag = "Player";
    public GameObject[] pressables;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(triggerTag))
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
