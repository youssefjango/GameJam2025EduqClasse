using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // ⬅️ Correct UI namespace

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ObjetGen> objetGenList;
    private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] GameObject buttonPrefab; // ⬅️ Must be GameObject
    [SerializeField] RectTransform panel;     // ⬅️ Make sure this is assigned in Inspector

    void Start()
    {
        panel = GetComponent<RectTransform>();
        ResizePanel();
        if (buttonPrefab != null)
        {
            GenerateButtons();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                if(hit.collider.tag == "Objet")
                    hit.collider.gameObject.GetComponent<ObjetGen>().Move(new Vector2(hit.collider.transform.position.x - mouseWorldPos.x, hit.collider.transform.position.y - mouseWorldPos.y));
                

            }
        }
    }

    void ResizePanel()
    {
        int objectCount = objetGenList.Count;
        float buttonWidth = 100f; // ⬅️ Customize based on prefab
        float spacing = 10f;

        float totalWidth = objectCount * buttonWidth + (objectCount - 1) * spacing;
        panel.sizeDelta = new Vector2(totalWidth, panel.sizeDelta.y);
    }

    void GenerateButtons()
    {
        foreach (ObjetGen obj in objetGenList)
        {
            GameObject button = Instantiate(buttonPrefab, panel); 
            buttons.Add(button);
            Image img = button.GetComponent<Image>();
            button.GetComponent<InvItem>().item = obj;
            if (img != null && obj != null && obj.sprite != null)
            {
                img.sprite = obj.sprite;
            }
            else
            {
                Debug.LogWarning("Missing Image component or sprite on button or object.");
            }
        }
        SetIndexes();
    }

    public void DeleteBouton(GameObject bouton)
    {
        int index = bouton.GetComponent<InvItem>().index;
        objetGenList.RemoveAt(index);
        buttons.RemoveAt(index);
        Destroy(bouton);
        SetIndexes();
        ResizePanel();
    }

    public void SetIndexes()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<InvItem>().index = i;
        }
    }

    public void AddItem(ObjetGen obj)
    {
        objetGenList.Add(obj);
        GameObject button = Instantiate(buttonPrefab, panel);
        button.GetComponent<InvItem>().index = buttons.Count;
        buttons.Add(button);
        button.GetComponent<InvItem>().item = obj;
        button.GetComponent<Image>().sprite = obj.sprite;
        ResizePanel();
    }
}