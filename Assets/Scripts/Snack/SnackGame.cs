using System;
using System.Collections;
using System.Collections.Generic;
using Snack;
using UnityEngine;

public class SnackGame : MonoBehaviour
{

    [Header("食物预制体")] public GameObject foodCellPre;
    
    [Header("蛇头预制体")] public GameObject headPre;

    
    // Start is called before the first frame update
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 游戏初始化
    private void InitGame()
    {
        GenerateHead();
        GenerateFood();
    }
    
    private void GenerateHead()
    {
        var head = Instantiate(headPre);
        head.transform.parent = transform;
        head.transform.localPosition = Vector3.zero;
    }

    // 生成食物
    private void GenerateFood()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 45));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 45));
        

        for (float i = bottomLeft.x; i < topRight.x; i += GameConst.foodCellW)
        {
            for (float j = bottomLeft.y; j < topRight.y; j += GameConst.foodCellW)
            {
                var food = Instantiate(foodCellPre);
                food.transform.parent = transform;
                food.transform.localPosition = new Vector3(i, j, 0);
                food.GetComponent<FoodCellPrefab>().ShowFood();
            }
        }
    }
}
