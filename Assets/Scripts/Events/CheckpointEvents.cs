using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEvents : MonoBehaviour
{
    public delegate void CheckPointEventHandler(Checkpoint checkpoint);
    public static event CheckPointEventHandler OnCheckpointInteraction;

    public static void NewCheckPoint(Checkpoint checkpoint)
    {
        if (OnCheckpointInteraction != null)
            OnCheckpointInteraction(checkpoint);
    }
}
