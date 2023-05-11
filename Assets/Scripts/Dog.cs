using UnityEngine;

public class Dog : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGravityScale()
    {
        rigidbody2D.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Line"))
        {
            
        }
    }
}
