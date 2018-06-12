using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using Proyecto26;
using Models;
using UnityEngine;
using System.IO;

public class ScreenshotVuforia : MonoBehaviour {

	WebCamTexture webCamTexture;
	private RequestHelper currentRequest;

	/*IEnumerator TakePicture () {
		yield return new WaitForEndOfFrame();
		var texture = ScreenCapture.CaptureScreenshotAsTexture();
		File.WriteAllBytes (Application.persistentDataPath + "/" + "screen_" + System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png", texture.EncodeToPNG ());
		Object.Destroy(texture);
		SceneManager.LoadScene("scene");
	}*/
	IEnumerator TakePhoto(){
		string basePath = PlayerPrefs.GetString ("basePath");
		yield return new WaitForEndOfFrame(); 
		Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
		photo.SetPixels(webCamTexture.GetPixels());
		photo.Apply();
		File.WriteAllBytes (Application.persistentDataPath + "/" + "screen_" + System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png", photo.EncodeToPNG ());
		RestClient.DefaultRequestHeaders ["Authorization"] = "Token " + SystemInfo.deviceUniqueIdentifier;
		RestClient.Post<UserDocument>(basePath + "accounts/documents/", new UserDocument {
			file_b64 = System.Convert.ToBase64String(photo.EncodeToPNG()),
			x = PlayerPrefs.GetInt("x"),
			y = PlayerPrefs.GetInt("y"),
			z = PlayerPrefs.GetInt("z")
			}).Then(user => {
				SceneManager.LoadScene("scene");
		});
	}

	void Update(){
		StartCoroutine (TakePhoto ());
	}
	void Start(){
		webCamTexture = new WebCamTexture();
		webCamTexture.Play();
	}

}
