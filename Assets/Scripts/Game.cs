using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    [Header("画线")] public GameObject linePre;
    [Header("蜜蜂")] public GameObject xiaomifeng;
    
    [Header("狗")] public GameObject dog;
    
    private DrawColliderLine drawColliderLine;

    private bool drawing;

    public float speed = 1F;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var line = Instantiate(linePre, Vector3.zero, Quaternion.identity);
            line.transform.SetParent(transform);
            drawColliderLine = line.GetComponent<DrawColliderLine>();
            drawColliderLine.OnMouseButtonDown();
            drawing = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (!drawing)
            {
                return;
            }

            drawColliderLine.OnMouseButton();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!drawing)
            {
                return;
            }

            drawColliderLine.OnMouseButtonUp();
            drawing = false;
            
            OutBees();
        }
    }

    private void OutBees()
    {
        var startPos = new Vector2(-1.5f, 3);
        var endPos = dog.transform.position;

        var xiaomofeng = Instantiate(xiaomifeng, startPos, Quaternion.identity);
        xiaomofeng.transform.SetParent(transform);
        xiaomofeng.GetComponent<Xiaomifeng>().Fly(startPos, dog.transform);
    }
}
