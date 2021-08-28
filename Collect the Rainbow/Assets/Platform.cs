using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the teleporter platform and its sectors. In the beginning of the game, all 6 sectors of the platform are transparent.
/// When a light orb (<see cref="Orb">) is collected by the player, the matching sector color becomes opaque. After all 6 light orbs have been collected,
/// and therefore all 6 sectors of the platform have been activated, the teleporter platform becomes 'activated'. When the player
/// collides with the platform, they float up and the level finished screen is displayed. The level completes and the user is returned
/// to the main menu.
/// </summary>
public class Platform : MonoBehaviour
{
    //The materials for each sector on the teleporter platform. These are assigned in the unity editor.
    [Header("Sector Materials")]
    public Material red;
    public Material orange;
    public Material yellow;
    public Material green;
    public Material blue;
    public Material purple;

    [Header("Player")]
    public GameObject player;

    /// <summary>The UI Panel that will be displayed when the level is completed.</summary>
    private GameObject levelFinishUI;

    /// <summary>The materials used to make up the 6 sectors of the teleporter platform.</summary>
    private Material[] platformMaterials = new Material[6];

    /// <summary>The amount of sector colors that have been activated on the teleporter platform.</summary>
    private int activatedSectorCount = 0;


    /// <summary>
    /// Gets a reference to the UI panel that contains the level completion image and then turns it off.
    /// Assigns material objects of each of the colored sectors in the teleporter platform to an array, platformMaterials.
    /// Deactivates all the sector materials in the array by changing the material alpha values to 0.
    /// When the game starts, all platform sectors will be transparent.
    /// </summary>
    void Start()
    {
        levelFinishUI = GameObject.FindGameObjectWithTag("Finish");
        levelFinishUI.SetActive(false);

        platformMaterials[0] = red;
        platformMaterials[1] = orange;
        platformMaterials[2] = yellow;
        platformMaterials[3] = green;
        platformMaterials[4] = blue;
        platformMaterials[5] = purple;

        this.DeActivateAllSectors(platformMaterials);
    }

    ///<summary>
    ///Deactivates every material in the array.
    /// </summary>
    /// <param name="matToDeactivate">The materials you wish to deactivate</param>
    private void DeActivateAllSectors(Material[] matToDeactivate)
    {
        for (int i = 0; i < matToDeactivate.Length; i++)
        {
            this.DeActivateSector(matToDeactivate[i]);
        }
    }

    /// <summary>
    /// Changes the material of the sector. RGB values are maintained and the alpha value is changed to 0.
    /// The material becomes transparent, rendering that sector deactivated.
    /// This method should only be used at the beginning of the game. However, if it is used later in the game,
    /// the activated sector count will reduce by 1.
    /// </summary>
    /// <param name="material">The material you wish to deactivate by making it transparent.</param>
    private void DeActivateSector(Material material)
    {
        material.SetColor("_BaseColor", new Color(material.color.r, material.color.g, material.color.b, 0f));
        if (activatedSectorCount > 0)
        {
            activatedSectorCount -= 1;
        }
    }

    /// <summary>
    /// Activates the sector color of the teleporter platform that matches the ID.
    /// </summary>
    /// <param name="colorID">The ID of the orb that was collided with.
    /// The ID will identify which sector color needs to be activated. The orb color and sector color will be the same.</param>
    public void ActivateSector(int colorID)
    {
        ActivateSectorColor(platformMaterials[colorID]);
    }

    /// <summary>
    /// Changes the material of the sector. RGB values are maintained and the alpha value is changed to 1.
    /// The material becomes opaque, rendering that sector activated. The activated sector count is increased by 1.
    /// If the activated sector count becomes equal to 6 (The total amount of sectors on the teleporter platform),
    /// the teleporter platform's collider becomes a trigger.
    /// </summary>
    /// <param name="material">The material you wish to activate by making it opaque.</param>
    private void ActivateSectorColor(Material material)
    {
        material.SetColor("_BaseColor", new Color(material.color.r, material.color.g, material.color.b, 1f));
        activatedSectorCount += 1;

        if (activatedSectorCount == 6)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    /// <summary>
    /// If the player collides with the teleporter platform (once it is activated),
    /// the gravity of the player will be changed so that they slowly rise off the ground.
    /// A coroutine is also started to complete the level.
    /// </summary>
    /// <param name="other">The object the teleporter platform collided with.</param>
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.GetComponent<MushroomMovement>().SetGravity(10f);
            StartCoroutine(LevelFinish());
        }
    }

    /// <summary>
    /// The player rises off the teleporter platform for 2 seconds.
    /// A level completion image will then be displayed for 5 seconds before returing to the main menu.
    /// </summary>
    /// <returns></returns>
    IEnumerator LevelFinish()
    {
        yield return new WaitForSeconds(2f);
        levelFinishUI.SetActive(true);

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

}
