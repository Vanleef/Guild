using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPlatform : MonoBehaviour
{
    public Animator animator;
    public Transform checkpointTransform;
    public int checkpointId;
    public string checkpointName;

    private void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController.Instance.RevivePlayer();
            animator.SetBool("isActive",true);
            CheckpointEvents.NewCheckPoint(new Checkpoint(checkpointTransform, checkpointId, checkpointName));
        }
    }
}
