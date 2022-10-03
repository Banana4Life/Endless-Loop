using UnityEngine;

public class ColliderTarget : MonoBehaviour
{
    public string triggerTag = "bullet";
    public GameObject[] pressables;
    public Transform holdPosition;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(triggerTag))
        {
            Press();
            var rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            other.gameObject.transform.position = holdPosition.position;
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
