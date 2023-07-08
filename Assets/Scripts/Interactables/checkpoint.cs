using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint
{
    public Transform transform;
    public int id { get; }
    public string name { get; }

    public Checkpoint(Transform position, int id, string name)
    {
        this.transform = position;
        this.id = id;
        this.name = name;
    }
}
