using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawColliderLine : MonoBehaviour
{
    [Header("多远绘制一次")] 
    public float distanceOfPosition = 0.02f;

    private const float LineWidth = 0.1f;

    public LineRenderer lr;

    public PolygonCollider2D edgeCollider2D;
    
    public Rigidbody2D rigidbody2D;

    private int index;

    private Vector3 upPoint = Vector2.zero;

    private List<Vector2> colliderPos = new List<Vector2>();

    private Action OnFloorFunc;

    void Start()
    {
        // edgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();
        // rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        // lr = gameObject.GetComponent<LineRenderer>();
    }

    public void SetOnFloorFunc(Action onFloorFunc)
    {
        OnFloorFunc = onFloorFunc;
    }

    public void OnMouseButtonDown()
    {
        colliderPos.Clear();
            
        rigidbody2D.gravityScale = 0;
        edgeCollider2D.enabled = false;
            
        lr.startWidth = LineWidth;
        lr.endWidth = LineWidth;

        index = 0;
    }

    public void OnMouseButton()
    {
        Vector3 point =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        if (lr != null)
        {
            if (Vector3.Distance(upPoint, point) >= distanceOfPosition)
            {
                index++;

                lr.positionCount = index;
                lr.SetPosition(index - 1, point);
                    
                colliderPos.Add(new Vector2(point.x, point.y));

                upPoint = point;
            }
        }
    }

    public void OnMouseButtonUp()
    {
        var pointList = GetColliderPath();
        edgeCollider2D.points = pointList.ToArray();
        // rigidbody2D.gravityScale = 1;
        rigidbody2D.useAutoMass = true;
        edgeCollider2D.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Floor"))
        {
            Debug.Log("到底了");
            OnFloorFunc?.Invoke();
        }    
    }

    List<Vector2> GetColliderPath()
    {
        //碰撞体轮廓点位
        List<Vector2> edgePointList = new List<Vector2>();
        //以LineRenderer的点位为中心, 沿法线方向与法线反方向各偏移一定距离, 形成一个闭合且不交叉的折线
        for (int j = 1; j < colliderPos.Count; j++)
        {
            //当前点指向前一点的向量
            Vector2 distanceVector = colliderPos[j - 1] - colliderPos[j];
            //法线向量
            Vector3 crossVector = Vector3.Cross(distanceVector, Vector3.forward);
            //标准化, 单位向量
            Vector2 offectVector = crossVector.normalized;
            //沿法线方向与法线反方向各偏移一定距离
            Vector2 up = colliderPos[j - 1] + 0.5f * LineWidth * offectVector;
            Vector2 down = colliderPos[j - 1] - 0.5f * LineWidth * offectVector;
            //分别加到List的首位和末尾, 保证List中的点位可以围成一个闭合且不交叉的折线
            edgePointList.Insert(0, down);
            edgePointList.Add(up);
            //加入最后一点
            if (j == colliderPos.Count - 1)
            {
                up = colliderPos[j] + 0.5f * LineWidth * offectVector;
                down = colliderPos[j] - 0.5f * LineWidth * offectVector;
                edgePointList.Insert(0, down);
                edgePointList.Add(up);
            }
        }
        //返回点位
        return edgePointList;
    }
}
