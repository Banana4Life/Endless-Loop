using System;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public PickupData data;
    public GameObject model;

    private void Update()
    {
        model.transform.Rotate(Vector3.up, Time.deltaTime * 10);
    }

    public void RecreateModel()
    {
        foreach (var collider in GetComponentsInChildren<ColliderPickup>())
        {
            DestroyImmediate(collider.gameObject);
        }

        model = Instantiate(data.model, transform);
        model.name = "Model (auto)";
        model.GetComponent<Collider>().isTrigger = true;
        var listener = model.AddComponent<ColliderPickup>();
        listener.Init(data);
    }
}
