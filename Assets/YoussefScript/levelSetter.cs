using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI; // Make sure to include this namespace for TextMeshPro

public class levelSetter : MonoBehaviour
{
    public string levelName;
    public GameObject title1;
    public GameObject title2;

    public GameObject description;
    private string descriptionText = "Objet : Réactivation du Dossier — Archive de Territoire Oublié. \nBonjour, \nVous avez été sélectionné·e par notre Agence de Récupération Visuelle pour un type de mission délicat : reconstituer des fragments de mondes oubliés. Des paysages autrefois vibrants, désormais égarés dans les méandres de récits brisés. Chaque client nous confie une description — incomplète, troublée, parfois erronée. À vous de l’interpréter. À vous de replacer les bons objets dans le décor... pour raviver un lieu perdu dans l’univers narratif. Mais attention : chaque erreur peut modifier le souvenir. Chaque détail compte. \nBienvenue dans les coulisses de la mémoire collective.\n\nBienvenue chez L.A.P.I.N.";

    public Button buttonFirst;
    public Button buttonSecond;


    public void triggerAnimation(){
        //animate text such that every character disappears one by one using a coroutine
        StartCoroutine(AnimateText(title1, title2));
        buttonFirst.gameObject.SetActive(false); 
    }

    private IEnumerator AppearText(GameObject text, string descriptionText)
    {
        text.SetActive(true); // Show the description text
        // Get the TextMeshProUGUI component from the GameObject
        TextMeshProUGUI textMesh = text.GetComponent<TextMeshProUGUI>();

        // Animate each character one by one
        for (int i = 0; i < descriptionText.Length; i++)
        {
            Debug.Log("i: " + i + " textMesh.text: " + textMesh.text);
            textMesh.text += descriptionText[i]; // Remove the last character
            yield return new WaitForSeconds(0.05f); // Adjust the delay as needed
        }
        buttonSecond.gameObject.SetActive(true); // Show the second button
    }
    private IEnumerator AnimateText(GameObject text1, GameObject text2)
    {
        // Get the TextMeshProUGUI component from the GameObjects
        TextMeshProUGUI textMesh1 = text1.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textMesh2 = text2.GetComponent<TextMeshProUGUI>();

        // Animate each character one by one
        int length = textMesh1.text.Length;
        for (int i = 0; i < length; i++)
        {
            textMesh1.text = textMesh1.text.Substring(0, textMesh1.text.Length - 1); // Remove the last character
            yield return new WaitForSeconds(0.05f);
        }
        int length2 = textMesh2.text.Length;
        for (int i = 0; i <  length2; i++)
        {
             textMesh2.text = textMesh2.text.Substring(0, textMesh2.text.Length - 1); // Remove the last character
            yield return new WaitForSeconds(0.05f); // Adjust the delay as needed
        }
        yield return StartCoroutine(AppearText(description, this.descriptionText));
    }
    public void GotoLevel()
    {
        Score.currentScore = 0; // Reset the score to 0
        SceneManager.LoadScene(levelName);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
