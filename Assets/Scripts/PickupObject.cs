using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public PickupData data;
    private GameObject _model;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(transform.Find("PlaceholderModel").gameObject);
        _model = Instantiate(data.model, transform);
        _model.GetComponent<Collider>().isTrigger = true;
        var listener = _model.AddComponent<TriggerListener>();
        listener.Init(data);
    }

    private void Update()
    {
        _model.transform.Rotate(Vector3.up, Time.deltaTime * 10);
    }
}