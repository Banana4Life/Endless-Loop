using UnityEngine;
using UnityEngine.Rendering;

public class GameRound : MonoBehaviour
{
    [Header("Player Refs")]
    public Dissolver playerDissolver;
    public PlayerCollider playerCollider;
    public PlayerControlled playerControlled;
    public Transform playerPos;

    [Header("Audio")]
    public AudioSource tik;
    public AudioSource tok;
    private bool tikOrTok;
    private float tikTokTime;

    [Header("Round Settings")]
    private float roundTime;
    public float maxRoundTime = 10;
    public float teleportTime = 1;
    public float preRoundTime = 1;
    public float postRoundTime = 1;

    private Volume volume;

    
    // Start is called before the first frame update
    void Start()
    {
        playerControlled.EnableMovement();
    }

    // Update is called once per frame
    void Update()
    {
        PlayTikTokSound();
        roundTime += Time.deltaTime;

        if (roundTime > maxRoundTime + teleportTime + postRoundTime + preRoundTime)
        {
            playerControlled.EnableMovement();
            roundTime = 0;
        }
        else if (roundTime > maxRoundTime + teleportTime + postRoundTime)
        {
            playerDissolver.StartUnDissolve();
        }
        else if (roundTime > maxRoundTime + teleportTime)
        {
            playerPos.position = playerCollider.lastTeleportPosition;
        }
        else if (roundTime > maxRoundTime)
        {
            playerDissolver.StartDissolve();
            playerControlled.DisableMovement();
        }
        
        // if (volume.profile.TryGet<Vignette>(out var vignette))
        // {
        //     vignette.intensity.value = roundTime / maxRoundTime;
        // }
    }

    private void PlayTikTokSound()
    {
        tikTokTime += Time.deltaTime;

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
    }
}
