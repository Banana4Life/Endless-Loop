using UnityEngine;

public class Computer : MonoBehaviour, Pressable
{
    private bool end;
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
}
