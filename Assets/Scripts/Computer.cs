using UnityEngine;

public class Computer : MonoBehaviour, Pressable
{
    public void Press()
    {
        Camera.main.GetComponent<EndFadeout>().Fadeout(() =>
        {
            // TODO end?
        });
    }
}
