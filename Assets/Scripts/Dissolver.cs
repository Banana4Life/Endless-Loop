using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    private float _min;

    private float _max;

    public float pcent = 0;

    public float dissolveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _min = skinnedMeshRenderers.ToList().Max(r => r.bounds.min.y);
        _max = skinnedMeshRenderers.ToList().Max(r => r.bounds.max.y);
        // Debug.Log("Meshes from " + _min + " to " + _max + " h=" + (_max - _min));
        foreach (var r in skinnedMeshRenderers)
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
        if (dissolveSpeed != 0)
        {
            pcent += Time.deltaTime * dissolveSpeed;
            
            foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
            {
                skinnedMeshRenderer.material.SetFloat("_PercentageDissolved", pcent);
            }
            if (pcent < 0)
            {
                dissolveSpeed = 0;
                pcent = 0;
            }
            else if (pcent > 100)
            {
                dissolveSpeed = 0;
                pcent = 100;
            }
        }
    }

    public void StartDissolve(float speed)
    {
        if (dissolveSpeed == 0)
        {
            dissolveSpeed = speed;
        }
    }

    public void StartUnDissolve(float speed)
    {
        dissolveSpeed = -speed;
    }
}