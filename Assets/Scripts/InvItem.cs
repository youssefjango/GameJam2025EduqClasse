using System;
using UnityEngine;

public class InvItem : MonoBehaviour
{
    public ObjetGen item;
    bool checking = false;
    public int index;
    Vector3 current;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checking)
            Check();
    }

    public void Selected()
    {
        checking = true;
        current = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Waiting");
    }

    private float FindDistance(Vector3 pos, Vector3 pos2) => Mathf.Sqrt(Mathf.Pow(pos.x - pos2.x, 2) + Mathf.Pow(pos.y - pos2.y, 2));

    private void Check()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        
        
        if (Input.GetMouseButtonUp(0))
        {
            checking = false;
            Debug.Log("Abandoned");
        }
        if (FindDistance(mouseWorldPos, current) > 0f)
        {
            Debug.Log("Now Following");
            ObjetGen objet = Instantiate(item);
            objet.originalPrefab = item;
            objet.Move();
            checking = false;
            transform.parent.GetComponent<Inventory>().DeleteBouton(this.gameObject);
        }

    }
}
