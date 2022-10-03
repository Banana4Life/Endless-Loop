using System;
using System.Linq;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public PickupData data;
    private GameObject model;

    private void Start()
    {
        model = GetComponentInChildren<ColliderPickup>().gameObject;
    }

    private void Update()
    {
        model.transform.Rotate(Vector3.up, Time.deltaTime * 10);
    }

    public GameObject RecreateModel(bool immediate = true)
    {
        foreach (var collider in GetComponentsInChildren<ColliderPickup>())
        {
            if (immediate)
            {
                DestroyImmediate(collider.gameObject);
            }
            else
            {
                Destroy(collider.gameObject);
            }

        }

        model = Instantiate(data.model, transform);
        model.name = "Model (auto)";
        model.AddComponent<ColliderPickup>().Init(data);
        return model;
    }
}
