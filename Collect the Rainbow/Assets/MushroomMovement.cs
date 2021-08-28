using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines player movement as well as the camera movement and related sound effects.
/// Up and down arrow keys control player movement, left and right arrow keys control
/// player roation, and the spacebar controls player jump. The mouse or touchpad
/// controls vertical camera roation in order to search the scene. Sounds effects will
/// play when the player moves, jumps up, and lands, given that the sound effect
/// setting is on.
/// </summary>
public class MushroomMovement : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool mushJumped = false;

    public GameObject cameraObject;
    public float mouseSensitivity = 0.75f;

    [Header("Player Settings")]
    public float playerSpeed = 12.0f;
    public float jumpHeight = 3.0f;
    public float rotationSpeed = 75.0f;
    private float gravityValue = -9.81f;

    private float controllerCenterY = 2.98f;
    private float controllerHeight = 6.0f;


    [Header("Player Sound Effects")]
    public AudioClip mushWalkAudio;
    public AudioClip mushJumpUp;
    public AudioClip mushJumpLand;

    private AudioSource mushWalkAudioSource;
    private AudioSource mushJumpUpAudioSource;
    private AudioSource mushJumpLandAudioSource;

    /// <summary>
    /// Initializes the player. A character controller is added and the radius, height, and center of the controller is configured.
    /// A reference to the camera game object, which is a child of the player, is obtained.
    /// Audio for player movement and jumping is added and configured. Movement aduio will loop while jumping audio will not.
    /// The volume for the landing from a jump is set to a higher value as that audio clip is naturally not as loud.
    /// </summary>
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.radius = 1.27f;
        controller.height = controllerHeight;
        controller.center = new Vector3(0f, controllerCenterY, 0f);
        

        cameraObject = gameObject.transform.GetChild(0).gameObject;

        mushWalkAudioSource = AddAudio(mushWalkAudio, true, false,0.75f);
        mushJumpUpAudioSource = AddAudio(mushJumpUp, false, false, 0.75f);
        mushJumpLandAudioSource = AddAudio(mushJumpLand, false, false, 0.95f);

        mushWalkAudioSource.pitch = -1.25f;

    }

    /// <summary>
    /// Adds an audio source component. The audio clip is assigned and the settings for loop, play on awake,
    /// and volume are configured.
    /// </summary>
    /// <param name="clip">The audio clip to add</param>
    /// <param name="loop">True will cause the audio to loop, while false will not</param>
    /// <param name="playAwake">True will cause the audio to play when the scene loads, while false will not</param>
    /// <param name="volume">The volume you want the audio to play</param>
    /// <returns>The configured audio component</returns>
    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float volume)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = volume;

        return newAudio;
    }

    /// <summary>
    /// Setter: Sets the gravity of the player. A value below zero causes the player to fall downward,
    /// while a value above zero causes the user to fall upward.
    /// </summary>
    /// <param name="newGravity">The gravity value you want for the player</param>
    public void SetGravity(float newGravity)
    {
        gravityValue = newGravity;
    }

    /// <summary>
    /// Getter: returns the current gravity of the player. A value below zero causes the player to fall downward,
    /// while a value abouve zero causes the user to fall upward.
    /// </summary>
    /// <returns>The current gravity value of the player</returns>
    public float GetGravity()
    {
        return gravityValue;
    }

    /// <summary>
    /// Will pause time. For use in future functionality.
    /// </summary>
    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Will resume time after it has been paused. For use in future functionality.
    /// </summary>
    public void ResumeTime()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Called every frame and defines the player movement. The up arrow and down arrow keys
    /// will move the player forwards and backwards respectively. The left and right arrow keys
    /// will rotate the player. Vertical mouse/touchpad inputs will move the camera up and down.
    /// The spacebar will allow the player to jump.
    /// </summary>
    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        ///Gets horizontal and vertical input from the keyboard which will affect the mushroom movement and rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        ///-----Rotate----
        /// If there is horizontal input, rotate the player at a consistent speed around the world y-axis, either left or right.
        /// If the input is negative, rotate the player to the left.
        /// If input is positive, rotate the player to the right.
        /// The defined roation speed and time are taken into account so the player will rotate consistently across frames.
        /// Since the camera object should be a child of the player, the camera will also rotate with the player.
        if (horizontalInput != 0)
        {
            gameObject.transform.Rotate(0, horizontalInput * rotationSpeed * Time.deltaTime, 0, Space.World);
        }

        ///-----Move-----
        /// If there is vertical input, move the player along the z axis, either forward or backward.
        /// If the input is positive, move the player forward.
        /// If the input is negative, move the player backward.
        if (verticalInput != 0)
        {
            Vector3 move = this.transform.TransformDirection(new Vector3(0, 0, verticalInput * Time.deltaTime));
            controller.Move(move * playerSpeed);

            ///If player has sound effects setting set to on and if the mushroom moving audio is not already playing,
            ///play the mushroom moving audio.
            if(Main.soundEffectsOn == true && mushWalkAudioSource.isPlaying == false)
            {
                mushWalkAudioSource.Play();
            }
        }

        ///If the player is not moving , check to see if the moving audio is already playing, and then turn it off.
        else if(mushWalkAudioSource.isPlaying == true)
        {
            mushWalkAudioSource.Stop();
        }

        ///-----Camera Vertical Rotate-----
        ///Vertical input from the mouse/touchpad is used to rotate the camera about the x-axis in local space.
        ///The speed of the rotation is affeceted by the mouseSensitivity variable. 
        ///This allows the user to look up and down in the scene. 
        float verticalRotation = Input.GetAxis("Mouse Y");
        cameraObject.transform.Rotate(-verticalRotation * mouseSensitivity, 0,0, Space.Self);

        ///-----Jump-----
        ///If the character controller is on a collider, either the ground or some object, the player
        ///is able to jump. If the player has already jumped and sound effect settings are on,
        ///the landing audio will play when the player lands on a collider.
        if (groundedPlayer)
        {
            ///If the player presses the jump button (spacebar),the y value of the player velocity
            ///is calculated. If the user has the sound effects setting on, the Jump Up audio will play.
            if (Input.GetButtonDown("Jump"))
            {
                mushJumped = true;
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

                if (Main.soundEffectsOn == true)
                {
                    mushJumpUpAudioSource.Play();
                }
            }

            ///If the player has already jumped, turn the mushroom jumped value to false so this will only be
            ///executed once after a jump.
            ///If the user has the sound effect setting on, the landing from the jump aduio will play.
            else if (mushJumped)
            {
                mushJumped = false;

                if (Main.soundEffectsOn == true)
                {
                    mushJumpLandAudioSource.Play();
                }
            }
        }

        ///The y value of the player velocity takes gravity and time into account. The character controller
        ///moves the player in respect to time.
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
