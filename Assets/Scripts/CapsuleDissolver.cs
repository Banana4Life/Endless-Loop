using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CapsuleDissolver : MonoBehaviour
{
    private MeshFilter filter;
    private MeshRenderer renderer;
    private float min;

    private float max;

    private float cutoff;

    public float dissolveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        min = filter.sharedMesh.vertices.ToList().Min(v => v.y) - 1;
        max = filter.sharedMesh.vertices.ToList().Max(v => v.y) + 1;
        cutoff = max;
        renderer.sharedMaterial.SetFloat("_CutOffHeight", cutoff);
    }

    // Update is called once per frame
    void Update()
    {
        if (dissolveSpeed != 0)
        {
            cutoff -= Time.deltaTime * dissolveSpeed;
            renderer.sharedMaterial.SetFloat("_CutOffHeight", cutoff);
            if (cutoff < min)
            {
                dissolveSpeed = 0;
            }
            else if (cutoff > max)
            {
                dissolveSpeed = 0;
            }
        }
    }

    public void StartDissolve(float speed)
    {
        if (dissolveSpeed == 0)
        {
            cutoff = max;
            dissolveSpeed = speed;
        }
    }

    public void StartUnDissolve(float speed)
    {
        cutoff = min;
        dissolveSpeed = -speed;
    }
}