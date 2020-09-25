using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBombPlacer : MonoBehaviour
{
    /*
     * Один из вариантов реализации, с помощью вейпоинтов можно выбрать в какие из точек вокруг камня
     * будут устанавливаться бомбы, а пересечение коллайдеров позволяет вычислять только ближайшие вейпоинты,
     * а не все на сцене.
     */
    [SerializeField] private GameObject bombPrefab;
    
    Transform currentCol;
    private Vector2 spawnPoint;
    
    int BombLayerMask = 1 << 8;
    
    private void OnTriggerStay2D(Collider2D other) //ищет камень, с которым есть колижн и запоминает его
    {
        if (other.CompareTag("Stone"))
        {
            currentCol = other.transform;
        }
    }
    
    public void PlaceBombWithWaypoint()
    {
        if (currentCol == null) { Debug.Log("no col"); return; }
        
        FindClosestSpawnPoint();
        CheckIfEmpty();
    }

    private void FindClosestSpawnPoint() //ищет ближайший к свинке вейпоинт среди чайлдов камня
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < currentCol.childCount; i++)
        {
            var spawnPos = currentCol.GetChild(i).transform.position;
            Vector2 diff = spawnPos - transform.position;
            float curDis = diff.magnitude;
            if (curDis < distance)
            {
                spawnPoint = spawnPos;
                distance = curDis;
            }
        }
    }

    void CheckIfEmpty() //проверка не стоит ли на этом вейпоинте бомба
    {
        RaycastHit2D hit = Physics2D.CircleCast(spawnPoint, .3f, Vector2.zero, Mathf.Infinity, BombLayerMask);
        
        if (hit.collider == null) //если на вейпоинте нет бомбы - ставит туда бомбу
        {
            Instantiate(bombPrefab, spawnPoint, Quaternion.identity); 
        }
        else
        {
            Debug.Log("bomb can't be placed");
        }
    }
}
