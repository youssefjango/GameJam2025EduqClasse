using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace for text components
using UnityEngine.SceneManagement; // Import for scene management

public class Score : MonoBehaviour
{
    public static int currentScore = 0;
    public  static bool removed = false;
    public string gameOverScenePath; // Path to the game over scene
    public List<string> placedObjects = new List<string>(); // Objects placed by the user
    [SerializeField] TextMeshProUGUI scoreText; // Text component to display the score
    [SerializeField] TextMeshProUGUI descriptionText;
    

    //sound assets
    public AudioClip soundEffectLoss; // Sound effect to play
    public AudioClip soundEffectWin; // Sound effect to play
    public GameObject audioSourceSFX; // Audio source to play the sound effect

    public GameObject audioSourceMusic; // Audio source to play the music
    public AudioClip musicClip1; // Music clip to play
    public AudioClip musicClip2; // Music clip to play
    public AudioClip musicClip3; // Music clip to play
    
    //report assets
    public GameObject reportTab;
    public GameObject reportButton;
    public GameObject reportText;

    private static int levelNumber = 0;

    // Background images
    public Sprite roomBackgroundImage;
    public Sprite publicPlaceBackgroundImage;
    public Sprite feistBackgroundImage;
    public GameObject backgroundImageComponent;

    public GameObject timer;
    
    
    // Descriptions
    public string DescriptionRoom = "La chambre exiguë sentait le renfermé. Un lit défait occupait presque tout l'espace. la piece clignotait faiblement face à une fenêtre au rideau entrouvert. Le jour filtrait timidement. Un drapeau LGBT pendait au mur, froissé, vibrant faiblement dans l'air, seul éclat de couleur dans la grisaille poussiéreuse, bizarrement, sans présance de cousin.";
    public string DescriptionPublicPlace = "Sous un soleil étincelant, le vendeur africain tenait son étal coloré en pleine place publique. Les parfums de bijoux et de mangues juteuses flottaient dans l'air tout en passant par les assietes diverses. Des colliers traditionnels brillaient sous la lumière. Son sourire, franc et fier, invitait les passants à goûter un morceau d'Afrique vivante.";
    public string DescriptionFeist = "Lanternes rouges suspendues aux balcons dansaient au rythme du vent chaud. Des tambours résonnaient entre les rires et les parfums de nouilles sautées, de jasmin et d'encens. Un dragon chinois ondulait dans la foule, doré et flamboyant. Au cœur de la célébration, un couple lesbien se tenait la main, leurs regards tendres illuminés par les feux d'artifice.";
    
    // Time Challenge
    public static float timeLimit = 180f; // 3 minutes
    private static float constTime;

    // Current description being used
    private string currentDescription;
    
    // Dictionary mapping descriptions to their required objects
    private Dictionary<string, List<string>> descriptionToRequiredObjects = new Dictionary<string, List<string>>();
    
    private void Awake()
    {
        // Initialize the mapping between descriptions and required objects
        descriptionToRequiredObjects.Add(DescriptionRoom, new List<string> { "lit", "rideauOvert", "burreau", "drapeau" });
        descriptionToRequiredObjects.Add(DescriptionPublicPlace, new List<string> {"assietes", "fruits", "bijoux" });
        descriptionToRequiredObjects.Add(DescriptionFeist, new List<string> { "grilladeAsia", "dragonAsia", "lanternesAsia", "templeAsia", "dragonAsia", "fontaineAsia", "coupleAsia" });
    }
    
    private void Start()
    {
        if(levelNumber == 0){
            constTime = timeLimit;
        } else{
            timeLimit = constTime - (levelNumber * 40f); // Decrease the time limit by 30 seconds for each level
        }
        int randomNum = Random.Range(0, 3); // Randomly select a description (0, 1, or 2)
        Debug.Log($"Random number generated: {randomNum}");
        
        switch (randomNum)
        {
            case 0:
                currentDescription = DescriptionRoom;
                SetBackground(roomBackgroundImage);
                audioSourceMusic.GetComponent<AudioSource>().clip = musicClip1; // Set the music clip for this level
                break;
            case 1:
                currentDescription = DescriptionPublicPlace;
                SetBackground(publicPlaceBackgroundImage);
                audioSourceMusic.GetComponent<AudioSource>().clip = musicClip2; // Set the music clip for this level
                break;
            case 2:
                currentDescription = DescriptionFeist;
                SetBackground(feistBackgroundImage);
                audioSourceMusic.GetComponent<AudioSource>().clip = musicClip3; // Set the music clip for this level
                break;
        }
        audioSourceMusic.GetComponent<AudioSource>().Play(); // Play the music clip

        
        // Initialize the UI
        UpdateScoreUI();
        UpdateDescriptionUI();
    }
    private void Update()
    {
        // Check if the time limit has been reached
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            if(timeLimit <= 20){
                timer.GetComponent<TextMeshProUGUI>().color = Color.red; // Change color to red when time is low
            } else if (timeLimit <= 60){
                timer.GetComponent<TextMeshProUGUI>().color = Color.yellow; // Change color to yellow when time is low
            } else {
                timer.GetComponent<TextMeshProUGUI>().color = Color.white; // Reset color to white when time is normal
            }
            
            timer.GetComponent<TextMeshProUGUI>().text = (int) timeLimit / 60 + ":" + (int) timeLimit % 60;
            if(timeLimit % 60 < 10){
                timer.GetComponent<TextMeshProUGUI>().text = (int) timeLimit / 60 + ":0" + (int) timeLimit % 60;
            }
        } else
            {
                CalculateScore();
            }
           if (Input.GetKeyDown(KeyCode.Space)){
                CalculateScore();
            }
    }
    
    // Ensure this method is linked to the button's onClick event in the Unity Editor
    public void CalculateScore()
    {
        audioSourceSFX.GetComponent<AudioSource>().clip = soundEffectWin; // Set the sound effect for this level
        audioSourceSFX.GetComponent<AudioSource>().Play(); // Play the sound effect
        int score = 0;
        // emplement score logic here
        List<GameObject> objectsInScene = new List<GameObject>(GameObject.FindGameObjectsWithTag("Objet"));
        bool atLeastOneObject = false;
        foreach (GameObject obj in objectsInScene)
        {
            string objectName = obj.GetComponent<ObjetGen>().nameObject;
            Debug.Log($"Checking object: {objectName}");
            Debug.Log(descriptionToRequiredObjects[currentDescription].Contains(objectName));
            if (descriptionToRequiredObjects[currentDescription].Contains(objectName))
            {
                score += 30; // Add points for each correctly placed object
                atLeastOneObject = true;
            } else{
                score -= 30;
            }
        }
        // check if there are missing objects
        if(objectsInScene.Count < descriptionToRequiredObjects[currentDescription].Count){
            score -= 10 * (descriptionToRequiredObjects[currentDescription].Count - objectsInScene.Count); // Deduct points for missing objects
        }
        currentScore += score;
        Debug.Log($"Current score: {currentScore}");
        if(!atLeastOneObject || currentScore < 0){ // no object placed correctly
        removed = true;
        SceneManager.LoadScene(gameOverScenePath);
         // Load the game over scene
        return;
            }
        if(levelNumber == 5){
            levelNumber = 0;
            // Load the final scene
            SceneManager.LoadScene("Scenes/GameOver"); 
        } else{
            levelNumber++;
        }
        UpdateScoreUI();
        //reload scene to reset the game
        loadReport(); 
        
    }
    public void loadReport(){
        reportTab.SetActive(true);
        reportButton.SetActive(true);
        reportText.GetComponent<TextMeshProUGUI>().text = "1. Vous avez recu" + currentScore + "$ pour vos traveaux. \n" + "2. Il y avait " + (descriptionToRequiredObjects[currentDescription].Count) + " objets à placer au total.\n Notre client est content du resultat, merci.\n  -L.U.P.I.N.";

    }
    
    // Method to update the score display
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{currentScore}";
        }
    }
    
    // Method to update the description display
    private void UpdateDescriptionUI()
    {
        descriptionText.text = ""; // Clear the text before displaying the new description
        StartCoroutine(AppearText(descriptionText, currentDescription)); 
    }
    private IEnumerator AppearText(TextMeshProUGUI text, string descriptionText)
    {
        
        // Animate each character one by one
        for (int i = 0; i < descriptionText.Length; i++)
        {
            text.text += descriptionText[i]; // Remove the last character
            yield return new WaitForSeconds(0.05f); // Adjust the delay as needed
        }
    }
    
    // Method to add a placed object (call this when user places an object)
    private void SetBackground(Sprite backgroundSprite)
    {
        Debug.Log($"Setting background image: {backgroundSprite.name}");
        backgroundImageComponent.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
    }
    public void resetLevel(){
        timeLimit = constTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}