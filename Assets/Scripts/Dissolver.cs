using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    private SkinnedMeshRenderer[] _skinnedMeshRenderers;
    private float _min;

    private float _max;

    public float pcent = 0;

    private float _dissolveSpeed;
    
    public int undissolveSpeed = 300;
    public int dissolveSpeed = 200;


    // Start is called before the first frame update
    void Start()
    {
        _skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        _min = _skinnedMeshRenderers.ToList().Max(r => r.bounds.min.y);
        _max = _skinnedMeshRenderers.ToList().Max(r => r.bounds.max.y);
        // Debug.Log("Meshes from " + _min + " to " + _max + " h=" + (_max - _min));
        foreach (var r in _skinnedMeshRenderers)
        {
            // Debug.Log(r.name + ": " + r.bounds.min + " to " + r.bounds.max);
            r.material.SetFloat("_MaxHeight", _max + 1);
            r.material.SetFloat("_MinHeight", _min- 1);
            r.material.SetFloat("_PercentageDissolved", 50);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_dissolveSpeed != 0)
        {
            pcent += Time.deltaTime * _dissolveSpeed;
            
            foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
            {
                skinnedMeshRenderer.material.SetFloat("_PercentageDissolved", pcent);
            }
            if (pcent < 0)
            {
                _dissolveSpeed = 0;
                pcent = 0;
            }
            else if (pcent > 100)
            {
                _dissolveSpeed = 0;
                pcent = 100;
            }
        }
    }

    public void StartDissolve()
    {
        if (_dissolveSpeed == 0)
        {
            _dissolveSpeed = dissolveSpeed;
        }
    }

    public void StartUnDissolve()
    {
        _dissolveSpeed = -undissolveSpeed;
    }
}