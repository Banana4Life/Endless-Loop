using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CapsuleDissolver : MonoBehaviour
{
    private MeshFilter _filter;
    private MeshRenderer _renderer;
    private float _min;

    private float _max;

    private float _cutoff;

    public float dissolveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        _min = _filter.sharedMesh.vertices.ToList().Min(v => v.y) - 1;
        _max = _filter.sharedMesh.vertices.ToList().Max(v => v.y) + 1;
        _cutoff = _max;
        _renderer.sharedMaterial.SetFloat("_CutOffHeight", _cutoff);
    }

    // Update is called once per frame
    void Update()
    {
        if (dissolveSpeed != 0)
        {
            _cutoff -= Time.deltaTime * dissolveSpeed;
            _renderer.sharedMaterial.SetFloat("_CutOffHeight", _cutoff);
            if (_cutoff < _min)
            {
                dissolveSpeed = 0;
            }
            else if (_cutoff > _max)
            {
                dissolveSpeed = 0;
            }
        }
    }

    public void StartDissolve(float speed)
    {
        if (dissolveSpeed == 0)
        {
            _cutoff = _max;
            dissolveSpeed = speed;
        }
    }

    public void StartUnDissolve(float speed)
    {
        _cutoff = _min;
        dissolveSpeed = -speed;
    }
}