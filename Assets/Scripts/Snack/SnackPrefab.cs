using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackPrefab : MonoBehaviour
{
    
    [Header("蛇头预制体")] public GameObject headPre;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        var head = Instantiate(headPre);
        head.transform.parent = transform;
        head.transform.localPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
    }
}
