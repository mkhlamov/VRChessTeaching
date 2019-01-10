using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

    public Transform playerHead;
    public float rayLength = 10;

    private ImageProgressBar imgProgressBar;

    private void Start()
    {

    }

    private void Raycast()
    {

        Ray ray = new Ray(playerHead.position, playerHead.forward);
        RaycastHit hit;

        Debug.DrawRay(playerHead.position, playerHead.forward * rayLength, Color.white, 0.1f);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("VR_UI")) {
                imgProgressBar = hit.collider.gameObject.GetComponent<ImageProgressBar>();
                imgProgressBar.GazeOver = true;
                imgProgressBar.StartFillingProgressBar();
                return;
            }
            
        }

        else if (imgProgressBar != null)
        {
            imgProgressBar.GazeOver = false;
            imgProgressBar.StopFillingProgressBar();
            imgProgressBar = null;
            return;
        }

    }

    private void Update()
    {

        Raycast();
    }

}
