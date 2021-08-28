using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the music and audio settings of the game. Both the music and sounds effects
/// can be toggled on/off independently from the main menu or from the level. The image
/// on the each button will change to correspond to the current setting. The value of each
/// setting is stored in <see cref="Main"/> as static variables <see cref="Main.musicOn"/>
/// and <see cref="Main.soundEffectsOn"/>.
/// </summary>
public class SoundSettings : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundMusic;

    [Header("Button Sprites")]
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public Sprite audioOnSprite;
    public Sprite audioOffSprite;

    [Header("Button Objects")]
    public GameObject musicButtonObject;
    public GameObject audioButtonObject;


    /// <summary>
    /// Toggles the music. If the music is not playing, play it and change the
    /// value to true. Otherwise, pause the music and change the value to false.
    /// In addtion, the graphic of the music setting button changes
    /// to correspond.
    /// </summary>
    public void ToggleMusic()
    {
        if (backgroundMusic.isPlaying == false)
        {
            backgroundMusic.Play();
            Main.musicOn = true;
            ChangeMusicButtonOnGraphic(musicButtonObject);
        }
        else
        {
            backgroundMusic.Pause();
            Main.musicOn = false;
            ChangeMusicButtonOffGraphic(musicButtonObject);
        }
    }

    /// <summary>
    /// Toggles the sound effects. If sounds was previously off, change the value to true.
    /// Otherwise, change the value to false. Other scripts will referecne this value
    /// in game when events that cause sound effects are executed.
    /// The graphic of the audio setting button also changes to coreespond.
    /// </summary>
    public void ToggleAudio()
    {
        if (Main.soundEffectsOn == false)
        {
            Main.soundEffectsOn = true;
            ChangeAudioButtonOnGraphic(audioButtonObject);
        }
        else
        {
            Main.soundEffectsOn = false;
            ChangeAudioButtonOffGraphic(audioButtonObject);
        }
    }

    /// <summary>
    /// Changes the sprite on the music button to have the music on graphic
    /// </summary>
    /// <param name="buttonObject">Music setting button</param>
    public void ChangeMusicButtonOnGraphic(GameObject buttonObject)
    {
        ChangeButtonGraphic(buttonObject, musicOnSprite);
    }

    /// <summary>
    /// Changes the sprite on the music button to have the music off graphic
    /// </summary>
    /// <param name="buttonObject">Music setting button</param>
    public void ChangeMusicButtonOffGraphic(GameObject buttonObject)
    {
        ChangeButtonGraphic(buttonObject, musicOffSprite);
    }

    /// <summary>
    /// Changes the sprite on the audio button to have the audio on graphic
    /// </summary>
    /// <param name="buttonObject">Sound FX setting button</param>
    public void ChangeAudioButtonOnGraphic(GameObject buttonObject)
    {
        ChangeButtonGraphic(buttonObject, audioOnSprite);
    }

    /// <summary>
    /// Changes the sprite on the audio button to have the audio off graphic
    /// </summary>
    /// <param name="buttonObject">Sound FX setting button</param>
    public void ChangeAudioButtonOffGraphic(GameObject buttonObject)
    {
        ChangeButtonGraphic(buttonObject, audioOffSprite);
    }

    /// <summary>
    /// Changes the image of the given button to the given image
    /// </summary>
    /// <param name="buttonObject">The button you would like to change the image of</param>
    /// <param name="buttonImage">The image you would like to be on the button</param>
    public static void ChangeButtonGraphic(GameObject buttonObject, Sprite buttonImage)
    {
        buttonObject.GetComponent<Image>().sprite = buttonImage;
    }

}
