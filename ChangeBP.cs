using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeBP : MonoBehaviour
{
    [SerializeField] private Button waypointBP;
    [SerializeField] private Button raycastBP;
    [SerializeField] private Text currentBPText;
    private bool raycastBPSetActive;

    private void Start()
    {
       RaycastBPActive(false);
    }

    public void SwitchBP()
    {
        raycastBPSetActive = !raycastBPSetActive;
        RaycastBPActive(raycastBPSetActive);
        
        var bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (var bomb in bombs)
        {
            Destroy(bomb);
        }
    }

    private void RaycastBPActive(bool isActive)
    {
        raycastBP.gameObject.SetActive(isActive);
        waypointBP.gameObject.SetActive(!isActive);
        
        if (isActive)
        {
            currentBPText.text = " Current BP = Raycast Bomb Placer";
        }
        else
        {
            currentBPText.text = " Current BP = Waypoint Bomb Placer";
        }
    }
}
