using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using UnityEngine.XR;
using Vuforia;

using Proyecto26;
using Models;

public class LoadSceneOnClick : MonoBehaviour {

	public InputField username;
	public InputField password;
	public Button loginButton;
	private RequestHelper currentRequest;
	public GameObject camera;

	void Start(){
		PlayerPrefs.SetString ("basePath", "https://127.0.0.1:8000/");
		camera.SetActive(true);
		//VuforiaRuntime.Instance.Deinit();
	}
	/*IEnumerator StartVuforiaAndPlay(int sceneIndex)
	{
		VuforiaRuntime.Instance.InitVuforia();
		while (!VuforiaRuntime.Instance.HasInitialized)
		{
			yield return null;
		}
		SceneManager.LoadSceneAsync(sceneIndex);
	}*/
	public void LoadByIndex(int sceneIndex){
		Debug.Log ("otra");
		string basePath = PlayerPrefs.GetString ("basePath");
		/*RestClient.Post<LoginUser>(basePath + "login/", new User {
			username = username.text,
			password = password.text
		}).Then(user => {
			PlayerPrefs.SetString("token", user.token);
			//StartCoroutine(StartVuforiaAndPlay(sceneIndex));
			SceneManager.LoadScene(sceneIndex);
		});*/SceneManager.LoadScene(sceneIndex);
	}

	void Update(){
		if (username.text != "") {
			if (password.text != "") {
				loginButton.interactable = true;
			}
		}
	}
}
