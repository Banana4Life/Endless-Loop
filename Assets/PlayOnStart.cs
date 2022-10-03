using UnityEngine;

public class PlayOnStart : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<AudioSource>().Play();
    }
}
