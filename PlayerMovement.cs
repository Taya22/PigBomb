using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private float angleFactor = .3f; //смещение в сторону при движении вдоль оси Y

    [Header("Input System")]
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    
    
    private Rigidbody2D rb;

    private float inputHorizontal;
    private float inputVertical;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
           MovePig(); 
        }
        else
        {
           rb.velocity = new Vector2(0f, 0f);
        }
    }
    
    void MovePig()
    {
        inputHorizontal = SimpleInput.GetAxis( horizontalAxis ) * moveSpeed;
        inputVertical = SimpleInput.GetAxis( verticalAxis ) * moveSpeed;
    
        if (inputVertical > 0)
        {
            rb.velocity = new Vector2(angleFactor, inputVertical);
        }
        else if (inputVertical < 0)
        {
            rb.velocity = new Vector2(-angleFactor, inputVertical);
        }
        else
        {
            rb.velocity = new Vector2(inputHorizontal, inputVertical);
        }
    }
}
