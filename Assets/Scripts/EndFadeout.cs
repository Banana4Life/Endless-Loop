using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class EndFadeout : MonoBehaviour
{
    private Volume _volume;
    private ChannelMixer _mixer;
    public float redDuration = 3f;
    public float greenDuration = 3f;
    public float blueDuration = 3f;
    private float _timeStarted = 0f;
    private float _redEndTime = 0f;
    private float _greenEndTime = 0f;
    private float _blueEndTime = 0f;
    public bool trigger = false;
    private Action _onComplete = null;

    private void Start()
    {
        _volume = GetComponent<Volume>();
        _mixer = _volume.profile.Add<ChannelMixer>();
        _mixer.active = false;
    }

    public void Fadeout(Action onComplete)
    {
        _timeStarted = Time.time;
        _redEndTime = _timeStarted + redDuration;
        _greenEndTime = _redEndTime + greenDuration;
        _blueEndTime = _greenEndTime + blueDuration;
        
        _mixer.redOutRedIn.overrideState = true;
        _mixer.redOutRedIn.value = 100;
        _mixer.greenOutGreenIn.overrideState = true;
        _mixer.greenOutGreenIn.value = 100;
        _mixer.blueOutBlueIn.overrideState = true;
        _mixer.blueOutBlueIn.value = 100;
        
        _mixer.active = true;

        _onComplete = onComplete;
    }

    private void Update()
    {
        var time = Time.time;
        if (trigger)
        {
            Fadeout(null);
            trigger = false;
        }
        if (_timeStarted > 0)
        {
            if (time < _redEndTime)
            {
                _mixer.redOutRedIn.value = 100 - (time - _timeStarted) / redDuration * 100;
                _mixer.greenOutGreenIn.value = 100;
                _mixer.blueOutBlueIn.value = 100;
            }
            else if (time < _greenEndTime)
            {
                _mixer.redOutRedIn.value = 0;
                _mixer.greenOutGreenIn.value = 100 - (time - _redEndTime) / greenDuration * 100;
                _mixer.blueOutBlueIn.value = 100;
            }
            else if (time < _blueEndTime)
            {
                _mixer.redOutRedIn.value = 0;
                _mixer.greenOutGreenIn.value = 0;
                _mixer.blueOutBlueIn.value = 100 - (time - _greenEndTime) / blueDuration * 100;
            }
            else
            {
                _mixer.redOutRedIn.value = 0;
                _mixer.greenOutGreenIn.value = 0;
                _mixer.blueOutBlueIn.value = 0;
                _timeStarted = 0f;
                _onComplete?.Invoke();
            }
        }
    }
}