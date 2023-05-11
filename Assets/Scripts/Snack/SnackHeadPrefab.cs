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

    [Header("移动速度")]
    public float moveSpeed = 2f;

    private List<Transform> bodyList = new List<Transform>();

    private float bodyOffest = 1f;

    private List<Vector3> headPosList = new List<Vector3>();
    private List<Quaternion> rotationList = new List<Quaternion>();

    private int rotation;

    private int foodCnt;

    private Dictionary<int, int> snackLevelFood = new Dictionary<int, int>()
    {
        
    };

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeOri(-1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeOri(1);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Grow();
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SnackLarger();
        }
    }

    private void FixedUpdate()
    {
        SnackMove();
    }

    // 改变朝向
    private void ChangeOri(int offest)
    {
        transform.Rotate(Vector3.forward, offest * angle);
    }

    // 长大
    private void Grow()
    {
        if (bodyList.Count > 0 && bodyList.Count % 10 == 0 && bodyOffest < 5)
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
        bodyOffest++;
    }

    // 移动
    private void SnackMove()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
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
            if (foodCnt >= 3)
            {
                foodCnt = 0;
                Grow();
            }
        }    
    }
}
