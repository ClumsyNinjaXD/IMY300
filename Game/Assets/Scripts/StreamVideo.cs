using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {
	public RawImage rawImage;

	public VideoPlayer videoPlayer;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		StartCoroutine(PlayVideo());
	}
	
	IEnumerator PlayVideo(){
		videoPlayer.Prepare();
		WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
		while(!videoPlayer.isPrepared)
		{
			yield return waitForSeconds;
			break;
		}

		rawImage.texture = videoPlayer.texture;
		videoPlayer.Play();
		audioSource.Play();
	}
}






// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Video;

// public class StreamVideo : MonoBehaviour {
// 	public RawImage image;

// 	private VideoPlayer videoPlayer;
// 	private VideoSource videoSource;

// 	// Use this for initialization
// 	void Start () {
// 		Application.runInBackground = true;
// 		StartCoroutine(playVideo());
// 	}
	
// 	IEnumerator playVideo(){
// 		videoPlayer = gameObject.AddComponent<VideoPlayer>();

// 		videoPlayer.playOnAwake = false;

// 		videoPlayer.source = VideoSource.Url;
// 		videoPlayer.url = "Video/TutorialVideo.avi";

// 		videoPlayer.Prepare();

// 		WaitForSeconds waitTime = new WaitForSeconds(1);
// 		while (!videoPlayer.isPrepared){
// 			Debug.Log("Preparing Video");
// 			yield return waitTime;
// 			break;
// 		}

// 		Debug.Log("Done Preparing Video");

// 		image.texture = videoPlayer.texture;

// 		videoPlayer.Play();

// 		Debug.Log("Playing Video");
// 		while (videoPlayer.isPlaying){
// 			Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
// 			yield return null;
// 		}

// 		Debug.Log("Done Playing Video");
// 	}

// 	// Update is called once per frame
// 	void Update () {
		
// 	}
// }
