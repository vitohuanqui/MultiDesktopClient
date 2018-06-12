using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using Vuforia;

public class Screenshot : MonoBehaviour {
	void RecordFrame()
	{
		SceneManager.LoadScene("scene_to_picture");
	}

	public void LateUpdate()
	{
		if (Input.touchCount > 0) {
			Vector3 cameraPosition = Camera.main.transform.position;
			PlayerPrefs.SetInt("x", Mathf.RoundToInt(cameraPosition.x));
			PlayerPrefs.SetInt("y", Mathf.RoundToInt(cameraPosition.y));
			PlayerPrefs.SetInt("z", Mathf.RoundToInt(cameraPosition.z));
			RecordFrame ();
		}
	}
	/*Camera mainCamera;
	RenderTexture renderTex;
	Texture2D screenshot;
	int width = Screen.width;
	int height = Screen.height;
	private bool isProcessing = false;
	public GameObject parent;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!isProcessing) {
			//if (Input.GetKeyDown("space")) {
			if (Input.touchCount > 0) {
				StartCoroutine (takeScreenShot ());
			}
		}
	}
	IEnumerator takeScreenShot(){
		ScreenCapture.CaptureScreenshot("Screenshot.png");

		Debug.Log ("puc");
		isProcessing = true;
		yield return new WaitForEndOfFrame();
		mainCamera = Camera.main.GetComponent<Camera> ();
		renderTex = new RenderTexture (width, height, 24);
		mainCamera.targetTexture = renderTex;
		mainCamera.Render ();
		screenshot = new Texture2D (width, height, TextureFormat.RGB24, false);
		screenshot.ReadPixels (new Rect (0, 0, width, height), 0, 0);
		screenshot.Apply ();

		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		parent = GameObject.Find("ImageTarget");
		plane.transform.position = new Vector3(50, 10, 10);
		plane.transform.Rotate (0, 0, 0);
		plane.transform.parent = parent.transform;
		plane.transform.localScale *= 2;

		Material material = new Material(Shader.Find("Diffuse"));
		//material.mainTexture = tex;
		plane.GetComponent<Renderer>().material.mainTexture = screenshot;
		File.WriteAllBytes (Application.persistentDataPath + "/" + "screen_" + System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png", screenshot.EncodeToPNG ());
		//plane.GetComponent<Renderer>().material.mainTexture = screenshot;

		//string path = "Assets/test.txt";
		//Write some text to the test.txt file
		//StreamWriter writer = new StreamWriter(path, true);
		//writer.WriteLine(System.Convert.ToBase64String(screenshot.EncodeToPNG()));
		//writer.Close();

		RenderTexture.active = null;
		mainCamera.targetTexture = null;
		isProcessing = false;
	}*/

}
