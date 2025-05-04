using UnityEngine;

public class Sac : MonoBehaviour
{
    GameObject inventaire;
    Inventory inventory;
    bool waiting;
    ObjetGen objet;

    [SerializeField] Sprite open;
    [SerializeField] Sprite close;

    SpriteRenderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventaire = GameObject.FindGameObjectWithTag("Inventaire");
        inventory = inventaire.GetComponent<Inventory>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            if (Input.GetMouseButtonUp(0))
            {
                inventory.AddItem(objet.originalPrefab);
                Destroy(objet.gameObject);
                waiting = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Objet")
        {
            Debug.Log("Nah");
            if (open != null)
            {
                renderer.sprite = open;
            }
            waiting = true;
            objet = (collision.gameObject.GetComponent<ObjetGen>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Objet")
        {
            Debug.Log("Wait");
            if (close != null) 
            {
                renderer.sprite = close;
            }
            waiting = false;
        }
    }
}

    
