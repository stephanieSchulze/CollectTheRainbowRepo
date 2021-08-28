using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the introduction sequence that is played before level 1.
/// the story line is introduced through a series of UI panels. The user can navigate between the images.
/// If there is no previous image, the user returns to the main screen. When the player has gone through
/// all of the introductory panels, level 1 scene is loaded.
/// </summary>
public class IntroNavigation : MonoBehaviour
{
    [Header("Intro UI Panels")]
    public GameObject IntroPart1;
    public GameObject IntroPart2;
    public GameObject IntroPart3;
    public GameObject IntroPart4;
    public GameObject IntroPart5;
    public GameObject Controls;

    private GameObject[] introScenes = new GameObject[6];
    private int sceneNumber;

    /// <summary>
    /// Starts the sceneNumber count as 0. Assigns the intro and control image UI objects to the introScene array.
    /// </summary>
    void Start()
    {
        sceneNumber = 0;

        introScenes[0] = IntroPart1;
        introScenes[1] = IntroPart2;
        introScenes[2] = IntroPart3;
        introScenes[3] = IntroPart4;
        introScenes[4] = IntroPart5;
        introScenes[5] = Controls;
    }

    /// <summary>
    /// Loads the next image in the intro sequence.
    /// If the intro is completed and there is no next image, loads level 1.
    /// </summary>
    public void next()
    {
        if (sceneNumber != introScenes.Length - 1)
        {
            nextScene();
        }

        else
        {
            SceneManager.LoadScene(1);
        }
    }

    /// <summary>
    /// Loads the previous image in the intro sequence.
    /// If the intro is at the beginning and there is no previous image, turn the
    /// intro UI off so the user is back to viewing the level menu.
    /// </summary>
    public void previous()
    {
        if(sceneNumber > 0)
        {
            previousScene();
        }

        else
        {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Deactivates the current scene image and activates the next one. The sceneNumber count is increased by 1;
    /// </summary>
    private void nextScene()
    {
        sceneNumber += 1;
        introScenes[sceneNumber - 1].SetActive(false);
        introScenes[sceneNumber].SetActive(true);
        
    }

    /// <summary>
    /// Deactivates the current scene image and activates the previous one. The sceneNumber count is decreased by 1;
    /// </summary>
    private void previousScene()
    {
        sceneNumber -= 1;
        introScenes[sceneNumber].SetActive(true);
        introScenes[sceneNumber + 1].SetActive(false);
    }

}
