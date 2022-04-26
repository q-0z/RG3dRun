using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,IInteractable
{
    [SerializeField] private float _yAxisRotVal = 10;
    public void OnCollide(IPawn collObj)
    {
        
        collObj.AddCoin();
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _yAxisRotVal, 0);
    }
}
