using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CapsuleDesolver : MonoBehaviour
{
    private MeshFilter filter;
    private MeshRenderer renderer;
    public float min;

    public float max;

    public float cutoff;

    public float dissolveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        min = filter.sharedMesh.vertices.ToList().Min(v => v.y) - 1;
        max = filter.sharedMesh.vertices.ToList().Max(v => v.y) + 1;
        cutoff = max + 1;
        renderer.sharedMaterial.SetFloat("_CutOffHeight", cutoff);
    }

    // Update is called once per frame
    void Update()
    {
        cutoff -= Time.deltaTime * dissolveSpeed;
        renderer.sharedMaterial.SetFloat("_CutOffHeight", cutoff);
        if (cutoff > max)
        {
            dissolveSpeed = Math.Abs(dissolveSpeed);
        }
        else if (cutoff < min)
        {
            dissolveSpeed = -Math.Abs(dissolveSpeed);
        }
    }
}
