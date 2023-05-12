using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.UIElements;

public class SnackHeadPrefab : MonoBehaviour
{

    [Header("蛇身")] public GameObject bodyPrefab;

    [Header("转向角度")]
    [Range(0, 360)] 
    public int angle = 10;

    [Header("初始移动速度")]
    public float moveSpeed = 2f;

    // 蛇身
    private List<Transform> bodyList = new List<Transform>();

    // 蛇身间距
    private float bodyOffest = 1f;

    // 存储蛇头移动点
    private List<Vector3> headPosList = new List<Vector3>();
    // 存储蛇头朝向
    private List<Quaternion> rotationList = new List<Quaternion>();
    
    // 累计食物数量
    private int foodCnt;

    // 需要的食物数量
    private int foodNeed = 3;

    // 等级
    private int level = 1;
    
    // 移动速度倍数
    private int speedUp = 1;

    void Start()
    {
        
    }

    void Update()
    {
        SnackMove();

        // 转向
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeOri(1);
        }

        // 转向
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeOri(-1);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Grow();
        }

        // 加速
        if (Input.GetKeyDown(KeyCode.W))
        {
            speedUp = 5;
        }
        
        // 还原速度
        if (Input.GetKeyUp(KeyCode.W))
        {
            speedUp = 1;
        }
    }

    private void FixedUpdate()
    {
    }

    // 改变朝向
    private void ChangeOri(int offest)
    {
        transform.Rotate(Vector3.forward, offest * angle);
    }

    // 长大
    private void Grow()
    {
        if (bodyList.Count > 0 && bodyList.Count % 10 == 0 && level < 5)
        {
            SnackLarger();
        }
        else
        {
            var body = Instantiate(bodyPrefab);
            body.transform.parent = transform.parent;
            body.transform.localScale = Vector3.zero;
            bodyList.Add(body.transform);
        }
    }

    // 变大
    private void SnackLarger()
    {
        // 等级
        level++;
        // 身体间距
        bodyOffest++;
        // 长身体需要的食物变多
        foodNeed *= level;
        // 速度减缓
        moveSpeed -= level * 0.1f;
    }

    // 移动
    private void SnackMove()
    {
        transform.Translate(moveSpeed * speedUp * Time.deltaTime, 0, 0);
        headPosList.Add(transform.position);
        rotationList.Add(transform.rotation);
        transform.localScale = Vector3.one * bodyOffest;

        if (bodyList.Count > 0)
        {
            var dis = 0f;
            var bodyIndex = 0;
            for (int i = headPosList.Count - 1; i > 0; i--)
            {
                dis += Vector3.Distance(headPosList[i], headPosList[i - 1]);
                if (dis >= bodyOffest)
                {
                    if (bodyIndex < bodyList.Count)
                    {
                        bodyList[bodyIndex].position = headPosList[i - 1];
                        bodyList[bodyIndex].rotation = rotationList[i - 1];
                        bodyList[bodyIndex].localScale = Vector3.one * bodyOffest;
                    }
                    else
                    {
                        if (i > 2)
                        {
                            headPosList.RemoveRange(0, i - 1);
                            rotationList.RemoveRange(0, i - 1);
                        }
                        break;
                    }

                    bodyIndex++;
                    dis = 0;
                }
            }

            for (int i = bodyIndex; i < bodyList.Count; i++)
            {
                bodyList[i].localScale = Vector3.zero;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("food"))
        {
            var foodCell = other.transform.parent;
            foodCell.GetComponent<FoodCellPrefab>().HideFood();
            
            foodCnt++;
            if (foodCnt >= foodNeed)
            {
                foodCnt = 0;
                Grow();
            }
        }    
    }
}
