using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// This class manages a single light orb. When an orb object is created, a collider, visual effect, an audio source are added to the object.
/// It is expected that the orb will be further initialized through the setter methods. The <see cref="Orbs"/> class calls these methods to properly set up
/// each orb in the scene. Getter methods are also provided in case of future functionality.
/// </summary>
public class Orb : MonoBehaviour
{
    private int id;
    private Vector3 orbPosition;
    private SphereCollider orbCollider;
    private VisualEffect orbEffect;
    private AudioSource orbAudioSource;

    /// <summary>
    /// The initialization of a light orb. A collider is added to the orb and the trigger is enabled
    /// to recognize when the player has collected the orb. A visual effect component is added. An assest  to define the dynamic appeareance of the orb.
    /// An audio source component is added for the audio clip that will play when the orb is collected. The audo source is connfigured so it does not loop or play on awake.
    /// The volume of the audio source is also set.
    /// The orb object is scaled down. For future functionality, there is opportunity to increase the scale of the orb if the player is having difficulties locating it. 
    /// </summary>
    public void CreateOrb()
    {
        orbCollider = gameObject.AddComponent<SphereCollider>();
        orbCollider.isTrigger = true;

        orbEffect = gameObject.AddComponent<VisualEffect>();

        orbAudioSource = gameObject.AddComponent<AudioSource>();
        orbAudioSource.loop = false;
        orbAudioSource.playOnAwake = false;
        orbAudioSource.volume = 0.9f;

        gameObject.transform.localScale = new Vector3(0.34f, 0.34f, 0.34f);

    }

    /// <summary>
    /// Getter: Returns the ID of the orb.
    /// </summary>
    /// <returns>The Orb ID</returns>
    public int GetID()
    {
        return this.id;
    }

    /// <summary>
    /// Setter: sets the id of the orb. The ID correlates with the orb color and respective sector.
    /// And ID of 0 is red, an ID of 1 is orange,...,and ID of 5 is purple.
    /// </summary>
    /// <param name="idNum">The ID Number for the Orb.</param>
    public void SetID(int idNum)
    {
        this.id = idNum;
    }

    /// <summary>
    /// Getter: Returns the current position of the orb.
    /// </summary>
    /// <returns>Current position of the orb</returns>
    public Vector3 GetPosition()
    {
        return orbPosition;
    }

    /// <summary>
    /// Setter: Stores the new position of the orb, and then puts the orb in that position in the scene.
    /// </summary>
    /// <param name="newPosition">The position in the scene where you want the orb to be.</param>
    public void SetPosition(Vector3 newPosition)
    {
        this.orbPosition = newPosition;
        gameObject.transform.position = this.orbPosition;
    }

    /// <summary>
    /// Getter: Returns the current radius of the orb collider.
    /// </summary>
    /// <returns>Current Orb Radius</returns>
    public float GetRadius()
    {
        return orbCollider.radius;
    }

    /// <summary>
    /// Setter: Changes the radius of the orb via its collider.
    /// </summary>
    /// <param name="newRadius">The radius you want the orb to be</param>
    public void SetRadius(float newRadius)
    {
        orbCollider.radius = newRadius;
    }

    /// <summary>
    /// Setter: provides a visual effect assest to define the appearance of the orb.
    /// </summary>
    /// <param name="orbEffectAsset">The effect to be used on the orb.</param>
    public void SetVisualEffectAsset(VisualEffectAsset orbEffectAsset)
    {
        orbEffect.visualEffectAsset = orbEffectAsset;
    }

    /// <summary>
    /// Setter: Changes the color of the orb via the visual effect assest to the color provided.
    /// </summary>
    /// <param name="colorVector">The color you want the orb to be</param>
    public void SetColor(Vector4 colorVector)
    {
        orbEffect.SetVector4("Color", colorVector);
    }

    /// <summary>
    /// Setter: Assigns the given audio clip to the orb audio sorce.
    /// This clip will be played when the orb is collected.
    /// </summary>
    /// <param name="orbAudio">The audio clip to be played when orb is collected</param>
    public void SetOrbAudio(AudioClip orbAudio)
    {
        orbAudioSource.clip = orbAudio;
    }

    /// <summary>
    /// When the player collides with an orb, the orb ID is passed to the teleporter platform
    /// to activate the corresponding sector color. The visual effect of the orb is turned off and
    /// the player can no longer see the orb. The collider is also turned off so the player can no longer run
    /// into the orb. However, the orb object iself still exists in case future functionality needs access to
    /// its properties. If the user has sound effects on, the orb collection audio will play.
    /// </summary>
    /// <param name="other">The object that collided with the orb. This object will be the player,
    /// since they are the only element in the game that changes position.</param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("Platform").GetComponent<Platform>().ActivateSector(id);
        orbEffect.Stop();
        orbCollider.enabled = false;
        if (Main.soundEffectsOn == true)
        {
            orbAudioSource.Play();
        }
    }
}
