using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int currentScore = 0;
    public List<string> placedObjects = new List<string>(); // Objects placed by the user
    public Text scoreText;
    public Text descriptionText;
    public Button submitButton;
    
    // Descriptions
    public string DescriptionRoom = "La chambre exiguë sentait le renfermé. Un lit défait occupait presque tout l'espace. Une télévision clignotait faiblement face à une fenêtre au rideau entrouvert. Le jour filtrait timidement. Un drapeau LGBT pendait au mur, froissé, vibrant faiblement dans l'air, seul éclat de couleur dans la grisaille poussiéreuse.";
    public string DescriptionPublicPlace = "Sous un soleil étincelant, le vendeur africain tenait son étal coloré en pleine place publique. Les parfums d'épices, de grillades et de mangues juteuses flottaient dans l'air. Des colliers traditionnels brillaient sous la lumière. Son sourire, franc et fier, invitait les passants à goûter un morceau d'Afrique vivante.";
    public string DescriptionFeist = "Lanternes rouges suspendues aux balcons dansaient au rythme du vent chaud. Des tambours résonnaient entre les rires et les parfums de nouilles sautées, de jasmin et d'encens. Un dragon chinois ondulait dans la foule, doré et flamboyant. Au cœur de la célébration, un couple lesbien se tenait la main, leurs regards tendres illuminés par les feux d'artifice.";
    
    // Current description being used
    public string currentDescription;
    
    // Dictionary mapping descriptions to their required objects
    private Dictionary<string, List<string>> descriptionToRequiredObjects = new Dictionary<string, List<string>>();
    
    private void Awake()
    {
        // Initialize the mapping between descriptions and required objects
        descriptionToRequiredObjects.Add(DescriptionRoom, new List<string> { "lit", "télévision", "fenêtre", "rideau", "drapeau LGBT" });
        descriptionToRequiredObjects.Add(DescriptionPublicPlace, new List<string> { "étal", "vendeur", "épices", "grillades", "mangues", "colliers" });
        descriptionToRequiredObjects.Add(DescriptionFeist, new List<string> { "lanternes", "tambours", "dragon", "encens", "couple" });
    }
    
    private void Start()
    {
        // Set the current description (you could choose one based on level or randomly)
        currentDescription = DescriptionRoom; // Change this as needed
        
        // Initialize the UI
        UpdateScoreUI();
        UpdateDescriptionUI();
        
        // Set up the submit button
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(CalculateScore);
        }
    }
    
    public void CalculateScore()
    {
        // Get required objects for the current description
        List<string> requiredObjects = descriptionToRequiredObjects[currentDescription];
        
        int matchedObjects = 0;
        int unrelatedObjects = 0;
        int totalRequiredObjects = requiredObjects.Count;
        
        // Count matches and unrelated objects
        foreach (string obj in placedObjects)
        {
            if (requiredObjects.Contains(obj))
            {
                matchedObjects++;
            }
            else
            {
                unrelatedObjects++;
            }
        }
        
        // Calculate percentage of required objects found
        float completionPercentage = (float)matchedObjects / totalRequiredObjects;
        
        // Update score based on matches and unrelated objects
        int matchScore = matchedObjects * 10; // 10 points per match
        int penaltyScore = unrelatedObjects * 5; // 5 points penalty per unrelated object
        
        // Update the score
        currentScore += matchScore - penaltyScore;
        
        // Show results
        Debug.Log($"Required objects: {totalRequiredObjects}, Matched: {matchedObjects}, Unrelated: {unrelatedObjects}");
        Debug.Log($"Completion rate: {completionPercentage * 100}%");
        Debug.Log($"Score update: +{matchScore} -${penaltyScore} = {matchScore - penaltyScore}");
        
        // Check for failure condition
        if (matchedObjects == 0)
        {
            Debug.Log("No required objects placed. You lose!");
        }
        
        // Update UI
        UpdateScoreUI();
    }
    
    // Method to update the score display
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }
    
    // Method to update the description display
    private void UpdateDescriptionUI()
    {
        if (descriptionText != null)
        {
            descriptionText.text = currentDescription;
        }
    }
    
    // Method to add a placed object (call this when user places an object)
    public void AddPlacedObject(string objectName)
    {
        placedObjects.Add(objectName);
        Debug.Log($"Object added: {objectName}");
    }
    
    // Method to remove a placed object (call this when user removes an object)
    public void RemovePlacedObject(string objectName)
    {
        placedObjects.Remove(objectName);
        Debug.Log($"Object removed: {objectName}");
    }
}