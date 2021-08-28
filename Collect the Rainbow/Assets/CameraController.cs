using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float distance = -10f;
    private Vector3 offset;
    public float cameraHeight = 3.71f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        //cameraHeight = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 back = -player.transform.forward;
        back.y = cameraHeight; // this determines how high. Increase for higher view angle.
        transform.position = player.transform.position - back * distance;

        transform.forward = player.transform.position - transform.position;
        //transform.position = player.transform.position + offset;
        //transform.LookAt(player.transform);//transform.rotation = player.transform.rotation;

        //float horizontalRotation = Input.GetAxis("Mouse X");
        //float verticalRotation = Input.GetAxis("Mouse Y");
    }
}
