using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    /// <summary>
    /// Starts the scene with the users audio and sound effects settings.
    /// If the music is set to off, Toggle Music Off. 
    /// </summary>
    void Start()
    {
        if (Main.musicOn == false)
        {
            gameObject.GetComponent<SoundSettings>().ToggleMusic();
        }
        if (Main.soundEffectsOn == false)
        {
            //workaround
            Main.soundEffectsOn = true;
            gameObject.GetComponent<SoundSettings>().ToggleAudio();
        }
    }

    /// <summary>
    /// Brings user back to the main menu. All progress will be lost.
    /// </summary>
    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Quits the game and closes the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

}
