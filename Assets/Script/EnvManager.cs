using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]private List<GameObject> _envObj;
    [SerializeField] private int _envPoolCount=10;
    [SerializeField]private Transform _envStart;

    private Queue<GameObject> _envPool= new Queue<GameObject>();
    void Start()
    {
        for(int i=0;i<_envPoolCount;i++)
        {
            var Obj = Instantiate(_envObj[0],transform);
            Obj.SetActive(false);
            _envPool.Enqueue(Obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
