using UnityEngine;
using UnityEngine.InputSystem;

public class ObjetGen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sprite sprite;
    bool following = false;
    Vector2 decalage = Vector2.zero;

    public ObjetGen originalPrefab;

    public string nameObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Place();
        }
        if(following && Input.GetMouseButton(0))
        {
            Follow();
        }
    }

    public void Place()
    {
        following = false;
    }

    public void Follow()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        mouseWorldPos.x += decalage.x;
        mouseWorldPos.y += decalage.y;
        transform.position = mouseWorldPos;
    }

    public void Move()
    {
        following = true;
    }

    public void Move(Vector2 decalage)
    {
        Move();
        this.decalage = decalage;
    }
}
