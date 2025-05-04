using UnityEngine;
using TMPro;
public class sceneFinale : MonoBehaviour
{
    public string finalMessage;
    public GameObject finalScene;
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip finalSoundGood;
    public AudioClip finalSoundBad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Score.currentScore = 0; // Reset the score to 0 
        Debug.Log("Final message: " + Score.removed);
       if(Score.removed){
        finalScene.GetComponent<TextMeshProUGUI>().text = finalMessage;
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
