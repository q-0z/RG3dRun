using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,IInteractable
{
    public void OnCollide(IPawn collObj)
    {
        
        collObj.AddCoin();
       // Destroy(this.gameObject,0.5f);
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
