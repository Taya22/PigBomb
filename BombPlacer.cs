using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlacer : MonoBehaviour
{
    /*
     * Вариант реализации через CircleCast размещенный на свинке. Когда круг задевает 4 камня - вычисляется позиция
     * в центре и устанавливается как SpawnPoint для создания в этой точке бомбы.
     */
    [SerializeField] private GameObject bombPrefab;
    
    int BombLayerMask = 1 << 8;
    private int StoneLayerMask = 1 << 10;

    private Vector2 spawnPoint;
    ContactFilter2D filter;
    readonly RaycastHit2D[] hit = new RaycastHit2D[4];


    void Start()
    {
        filter.layerMask = StoneLayerMask;
        filter.useLayerMask = true;
    }
    
    public void PlaceBomb() 
    {
        var castRay = Physics2D.CircleCast(transform.position, 1.45f, Vector2.zero, filter, hit);
        if (castRay == 4) //когда луч задевает 4 камня - вычисляет позицию центра
        {
            var xPos = (hit[0].point.x + hit[1].point.x + hit[2].point.x + hit[3].point.x) / 4;
            var yPos = (hit[0].point.y + hit[1].point.y + hit[2].point.y + hit[3].point.y) / 4;
            spawnPoint = new Vector2(xPos, yPos);
            CheckIfEmpty();
        }
    }
    
    void CheckIfEmpty()
    {
        RaycastHit2D hit = Physics2D.CircleCast(spawnPoint, .3f, Vector2.zero, Mathf.Infinity, BombLayerMask);
        
        if (hit.collider == null) //если бомбы нет в точке спавна - создает бомбу
        {
            Instantiate(bombPrefab, spawnPoint, Quaternion.identity); 
        }
        else
        {
            Debug.Log("bomb can't be placed");
        }
    }
}
