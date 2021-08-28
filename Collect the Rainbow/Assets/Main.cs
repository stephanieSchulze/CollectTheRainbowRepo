using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Main : MonoBehaviour
{
    [Tooltip("The setting of the music. True means on while False means off.")]
    public static bool musicOn;
    [Tooltip("The setting of the sound effects. True means on while False means off.")]
    public static bool soundEffectsOn;

    /// <summary>
    /// Starts the application with both music and sound effects on.
    /// </summary>
    void Start()
    {
       musicOn = true;
       soundEffectsOn = true;
    }

    /// <summary>
    /// Loads the scene for each level. Level 1 corresponds to scene 1.
    /// Currently there is only one level, but as levels get added, this method
    /// will be beneficial.
    /// </summary>
    /// <param name="levelNumber">The level you would like to load</param>
    public void LoadScene(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    /// <summary>
    /// Closes and quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

}
