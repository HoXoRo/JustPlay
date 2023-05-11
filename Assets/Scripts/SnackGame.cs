using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackGame : MonoBehaviour
{

    [Header("食物预制体")] public GameObject foodPre;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 生成食物
    private void GenerateFood()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 45));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 45));
        
        var offestX = 5;
        var offestY = 5;

        for (float i = bottomLeft.x; i < topRight.x; i += offestX)
        {
            for (float j = bottomLeft.y; j < topRight.y; j += offestY)
            {
                var x = i + UnityEngine.Random.Range(0, offestX);
                var y = j + UnityEngine.Random.Range(0, offestY);

                var food = Instantiate(foodPre);
                food.transform.parent = transform;
                food.transform.localPosition = new Vector3(x, y, 0);
            }
        }
    }
}
