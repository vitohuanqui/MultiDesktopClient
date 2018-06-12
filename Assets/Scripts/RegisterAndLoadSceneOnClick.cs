using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

using Proyecto26;
using Models;

public class RegisterAndLoadSceneOnClick : MonoBehaviour {

	public InputField username;
	public InputField password;
	public Button registerButton;

	private RequestHelper currentRequest;

	public void LoadByIndex(int sceneIndex){
		string basePath = PlayerPrefs.GetString ("basePath");
		RestClient.Post<RegisterUser>(basePath + "login/", new User {
			username = username.text,
			password = password.text
		}).Then(user => {
			PlayerPrefs.SetString("token", user.token);
		});
		SceneManager.LoadScene (sceneIndex);
	}

	void Update(){
		if (username.text != "") {
			if (password.text != "") {
				registerButton.interactable = true;
			}
		}
	}
}
