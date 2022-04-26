using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour,IInteractable
{
    public void OnCollide(IPawn collObj)
    {
        collObj.ReduceHealth();
    }
}
