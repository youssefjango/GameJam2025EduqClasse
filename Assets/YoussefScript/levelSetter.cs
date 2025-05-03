using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSetter : MonoBehaviour
{
    public string levelName;

    public void GotoLevel()
    {
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
