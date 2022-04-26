using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [SerializeField] public List<GameObject> obstacleList;
    public static ObstacleManager Instance;
    [SerializeField] private int _maxObsPoolCount = 40;
    [SerializeField] private Queue<GameObject> _obsPool = new Queue<GameObject>();
    float[] xRange = { -3, 0, 3 };
    private void Awake()
    {
        Instance = this;
    }
    public void Init()
    {
        int j=0;
        for(int i=0;i<_maxObsPoolCount;i++)
        {
            if (j >= obstacleList.Count)
                j = 0;
            _obsPool.Enqueue(Instantiate( obstacleList[j]));
            j++;
        }
       var list= _obsPool.ToList().OrderBy(item => (new System.Random()).Next()).ToList();
        _obsPool = new Queue<GameObject>(list);
    }
    public GameObject getRandomObstacle()
    {
        int id= Random.Range(0, obstacleList.Count);
        if (_obsPool.Count <= 0) return null;
        var obj = _obsPool.Dequeue();//Instantiate(obstacleList[id]);

        float randX = xRange[Random.Range(0, 3)];
        obj.transform.position = new Vector3(randX,-0.2f,transform.position.z);
        StartCoroutine(LateEnqueue(obj));
        return obj;
    }

    IEnumerator LateEnqueue(GameObject obj,float delay=3)
    {
        yield return new WaitForSeconds(delay);
        _obsPool.Enqueue(obj);
    }
}
