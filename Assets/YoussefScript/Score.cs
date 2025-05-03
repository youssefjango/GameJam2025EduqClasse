using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int currentScore = 0; // Player's current score
    public List<string> requiredObjects; // List of objects required in the scene
    public List<string> placedObjects; // List of objects placed by the player
    public Text scoreText; // Reference to the UI Text element for displaying the score
    public Button submitButton; // Reference to the Submit button

    private void Start()
    {
        // Initialize the score display
        UpdateScoreUI();

        // Add a listener to the Submit button
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(CalculateScore);
        }
    }

    // Method to calculate the score when the player presses the submit button
    public void CalculateScore()
    {
        int matchedObjects = 0;
        int unrelatedObjects = 0;

        // Count matched and unrelated objects
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

        // Update score based on matches and unrelated objects
        currentScore += matchedObjects * 10; // Add points for correct objects
        currentScore -= unrelatedObjects * 5; // Deduct points for unrelated objects

        // Check if no required objects are placed
        if (matchedObjects == 0)
        {
            Debug.Log("No required objects placed. You lose!");
            UpdateScoreUI();
            return;
        }

        Debug.Log($"Score calculated: {currentScore}");
        UpdateScoreUI();
    }

    // Method to update the score display in the UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }
}
