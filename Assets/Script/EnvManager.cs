using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]private List<EnvObj> _envObj;
    [SerializeField] private int _envPoolCount=10;
    [SerializeField] private int _envActivePoolCount = 7;
    [SerializeField]private Transform _envStart;

    private Queue<EnvObj> _envPool= new Queue<EnvObj>();
    [SerializeField]private List<EnvObj> _envActivePool= new List<EnvObj>();

    public static EnvManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        for(int i=0;i< _envPoolCount; i++)
        {
            var Obj = Instantiate(_envObj[0],transform);
            //Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.x, Obj.transform.position.z + i * 55.06f);
            Obj.gameObject.SetActive(false);
            PushEnv(Obj);
        }
        for (int i = 0; i < _envActivePoolCount; i++)
        {
            var Env = GetEnv();
            if (i != 0)
                Env.gameObject.transform.position = _envActivePool[i-1].endPoint.position;
            _envActivePool.Add(Env);
            Env.gameObject.SetActive(true);
        }
       


    }
    
        EnvObj GetEnv()
    {
        return _envPool.Dequeue();
    }
    void PushEnv(EnvObj env)
    {
       // env.gameObject.SetActive(false);
        _envPool.Enqueue(env);
    }
    public void Trigg()
    {
        var Env=GetEnv();
        Env.gameObject.SetActive(true);
        Env.transform.position = _envActivePool[_envActivePool.Count-1].endPoint.position;
        _envActivePool.Add(Env);
        Env= _envActivePool[0];
        _envActivePool.RemoveAt(0);
        PushEnv(Env);
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
