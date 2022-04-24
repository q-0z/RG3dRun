using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [SerializeField] public List<GameObject> obstacleList;
    public static ObstacleManager Instance;

    private void Awake()
    {
        Instance = this;
    }
   
    public GameObject getRandomObstacle()
    {
        int id= Random.Range(0, obstacleList.Count);

        return Instantiate(obstacleList[id]);
    }
}
