using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGrappleButton : MonoBehaviour {

	public GameObject video; // Assign in inspector
    private bool isShowing;

	void OnCollisionEnter(Collision collision)
    {
		isShowing = !isShowing;
	}

	void Update ()
	{
		if(Input.GetKeyDown("escape")){
			isShowing = !isShowing;
			video.SetActive(isShowing);
		}
	}

	public void Button_Click()
	{
		isShowing = !isShowing;
		video.SetActive(isShowing);
	}

}
