using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void RemoveObstacles(List<GameObject> obstacles)
    {
        foreach (var obj in obstacles)
        {
            if (obj != null)
                Destroy(obj);
        }
    }
}
