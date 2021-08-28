using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Creates and sets up 6 light orbs (<see cref="Orb"/>). The orbs are assigned to an array, sceneOrbs, for easy access.
/// </summary>
public class Orbs : MonoBehaviour
{
    [Tooltip ("The visual effect that is used to create the light orbs' appearance")]
    public VisualEffectAsset orbEffectAsset;
    [Tooltip("The size of the light orbs")]
    public float orbRadius = 2f;
    [Tooltip("The audio that will be played when an orb is collected")]
    public AudioClip orbCollectedAudio;

    /// <summary>The light orb objects in the scene.</summary>
    private Orb[] sceneOrbs = new Orb[6];

    /// <summary> The positions of each of the orbs in the scene. The first object in the array is the position of the red orb,
    /// and the last object in the array is the position of the purple orb</summary>
    private Vector3[] orbPositions = { new Vector3(-27.95f, 4.21f,47.54f), new Vector3(16.14f, 27.18f, 43.63f), new Vector3(35.69f, 2.21f, -22.52f),
    new Vector3(15.32f, 26.23f,-39.55f),new Vector3(-33.81f, 1f,-44.47f),new Vector3(-50.22f, 11.93f,-12f)};

    /// <summary> The colors of each of the orbs in the scene. The first object in the array is the color of the red orb,
    /// and the last object in the array is the color of the purple orb. </summary>
    private Vector4[] orbColors = { new Color(6.158117f, 0.5519065f, 0.8273795f), new Color(2.055392f, 0.7350395f, 0.1454287f),
    new Color(6.143448f, 6.158116f, 0.5803461f), new Color(0.6522133f, 6.158116f, 0.6125876f),
    new Color(0.6448288f, 2.098002f, 6.158116f), new Color(2.033882f, 0.6448288f, 6.158116f) };


    /// <summary>
    /// For every Orb in the scene: <br/>
    /// -A Game Object is created and given a name. The Game Object is childed to this current object in the project hierarchy. <br/>
    /// -The Orb component is added and initialized with CreateOrb <br/>
    /// -An ID is set. The ID identifys what color sector the Orb will be tied to. 0 is red, 1 is orage,..., 5 is purple. <br/>
    /// -The position and size of the orb in the scene is set.<br/>
    /// -The visual effect asset, which gives the orbs their dynamic appearence, is assigned.<br/>
    /// -The color of the orb is set. This color is applied to the visual effect asset.<br/>
    /// -The audio that plays when an orb is collected is set.<br/>
    /// -Assigned to the sceneOrbs array.
    /// </summary>
    void Start()
    {
        for (int i = 0; i < sceneOrbs.Length; i++)
        {
            GameObject orbObject = new GameObject();
            orbObject.name = "Orb" + i.ToString();
            orbObject.transform.SetParent(this.transform);

            Orb orb = orbObject.AddComponent<Orb>();
            orb.CreateOrb();

            orb.SetID(i);
            orb.SetPosition(orbPositions[i]);
            orb.SetRadius(2f);

            orb.SetVisualEffectAsset(orbEffectAsset);
            orb.SetColor(orbColors[i]);

            orb.SetOrbAudio(orbCollectedAudio);

            sceneOrbs[i] = orb;
        }

    }

}
