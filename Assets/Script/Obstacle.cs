using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour,IInteractable
{
    public void OnCollide(IPawn collObj)
    {
        Debug.LogError("co3l");
        collObj.ReduceHealth();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
