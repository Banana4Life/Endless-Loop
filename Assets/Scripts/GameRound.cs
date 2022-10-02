using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameRound : MonoBehaviour
{
    private Volume volume;
    private Dissolver _dissolver;

    public AudioSource tik;
    public AudioSource tok;
    private bool tikOrTok;
    
    private float roundTime;
    private float tikTokTime;
    public float maxRoundTime = 10;

    public int state = 0;

    public int undissolveSpeed = 300;
    public int dissolveSpeed = 200;

    // Start is called before the first frame update
    void Start()
    {
        volume = Camera.main.GetComponent<Volume>();
        _dissolver = GetComponent<Dissolver>();
        if (volume.profile.TryGet<Vignette>(out var vignette))
        {
            vignette.intensity.overrideState = true;
            vignette.intensity.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        tikTokTime += Time.deltaTime;
        roundTime += Time.deltaTime;
        
        if (roundTime > maxRoundTime - 1)
        {
            _dissolver.StartDissolve(dissolveSpeed);
        }
        
        if (tikTokTime > 1)
        {
            tikTokTime -= 1;
            if (tikOrTok)
            {
                tik.Play();    
            }
            else
            {
                tok.Play();
            }

            tikOrTok = !tikOrTok;

        }
        if (roundTime > maxRoundTime)
        {
            roundTime -= maxRoundTime;
            _dissolver.StartUnDissolve(undissolveSpeed);
        }
        
        if (volume.profile.TryGet<Vignette>(out var vignette))
        {
            vignette.intensity.value = roundTime / maxRoundTime;
            
        }
    }
}
