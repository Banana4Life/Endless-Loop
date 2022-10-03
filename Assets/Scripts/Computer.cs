using System;
using UnityEngine;
using UnityEngine.Audio;

public class Computer : MonoBehaviour, Pressable
{
    public AudioMixer mixer;
    public bool end;
    public void Press()
    {
        if (!end)
        {
            end = true;
            Camera.main.GetComponent<EndFadeout>().Fadeout(() =>
            {
                // TODO end?
            });
        }
        
    }

    private void Update()
    {
        if (end)
        {
            mixer.FindSnapshot("Off").TransitionTo(10);
        }
    }
}
