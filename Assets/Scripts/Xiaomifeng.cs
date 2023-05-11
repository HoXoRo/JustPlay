using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Xiaomifeng : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;

    private float speed = 0.1f;

    private Vector2 midPos;
    private Vector2 endPos;
    private bool isChange;

    private Transform dogTransform;

    private Vector2 lastPoint;

    void Start()
    {

    }

    void Update()
    {
        if (!isChange && Vector2.Distance(transform.position, midPos) < 0.1f)
        {
            isChange = true;
            ChangePath(endPos);
        }
        
    }

    private Vector3 point;
    public void Fly(Vector2 startPos, Transform dog)
    {
        dogTransform = dog;
        endPos = dogTransform.position;
        lastPoint = endPos;
        
        midPos = GetBetweenPoint(startPos, endPos, 0.2f);
        midPos.y = startPos.y + Random.Range(0.5f, 1f);

        gameObject.transform.position = startPos;
        point = startPos;
        ChangePath(midPos);
    }

    private void ChangePath(Vector2 pos)
    {
        // 旋转角度
        Vector3	MoveNormalized = (Vector3)pos - transform.position;
        //两点之间的向量
        Vector3	 V3 =  MoveNormalized.normalized;
        // 改变角度
        RotateToDir(V3);
        // 速率方向
        rigidbody2D.velocity = V3 * 1f;
    }
    
    protected void RotateToDir(Vector3 dir)
    {
        float angle = Vector3.SignedAngle(Vector3.right, dir, Vector3.forward);
        Quaternion rotation00 = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation00;
    }

    /// <summary>
    /// 获取两点之间距离一定百分比的一个点
    /// </summary>
    /// <param name="start">起始点</param>
    /// <param name="end">结束点</param>
    /// <param name="distance">起始点到目标点距离百分比</param>
    /// <returns></returns>
    private Vector2 GetBetweenPoint(Vector2 start, Vector2 end, float percent=0.5f)
    {
        Vector2 normal = (end - start).normalized;
        float distance = Vector2.Distance(start, end);
        return normal * (distance * percent) + start;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.transform.CompareTag("Line")) {return;}

        rigidbody2D.velocity = -rigidbody2D.velocity;
        midPos = (Vector2)transform.position + rigidbody2D.velocity * 0.5f;
        isChange = false;

        endPos = dogTransform.position;
        endPos.x += Random.Range(0, 0.5f);
        endPos.y -= Random.Range(0, 0.5f);
        lastPoint = endPos;
    }
}