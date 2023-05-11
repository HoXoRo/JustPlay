using System.Collections;
using System.Collections.Generic;
using Snack;
using UnityEngine;

public class FoodCellPrefab : MonoBehaviour
{
    public Transform foodTran;

    // 是否需要延时
    private bool needDelay = false;
    
    // 累计时间
    private float delayTime = 0;

    // 延时展示食物时间
    private float showTime = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (needDelay)
        {
            delayTime += Time.deltaTime;

            if (delayTime > showTime)
            {
                needDelay = false;
                delayTime = 0;
                ShowFood();
            }
        }
    }

    public void ShowFood()
    {
        foodTran.localScale = Vector3.one;
        var w = GameConst.foodCellW;
        var x = UnityEngine.Random.Range(-w/2, w/2);
        var y = UnityEngine.Random.Range(-w/2, w/2);
        
        foodTran.localPosition = new Vector3(x, y, 0);
    }

    public void HideFood()
    {
        foodTran.localScale = Vector3.zero;
        needDelay = true;
        delayTime = 0;
    }
}
