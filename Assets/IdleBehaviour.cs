using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    private SpawnMiniSpider MiniSpiderSpawner;
    private DistanceJoint2D Web;
    private bool isDropped = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MiniSpiderSpawner = animator.gameObject.GetComponent<SpawnMiniSpider>();
        Web = animator.gameObject.GetComponent<DistanceJoint2D>();
        MiniSpiderSpawner.StartCoroutine(MiniSpiderSpawner.Spawn());
        animator.gameObject.GetComponent<ShootWeb>().Shoot();
        BackToTop(animator);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameObject.FindGameObjectsWithTag("MiniSpider").Length == 0)
        {
            animator.SetTrigger("DroppedIdle");
        }
    }

    void Drop(Animator animator)
    {
        SoundManager.instance.Play("Spider");
        isDropped = true;
        Web.distance = 13f;
    }

    void BackToTop(Animator animator)
    {
        isDropped = false;
        Web.distance = 1f;
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Drop(animator);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
    //AddComponent depois
}
