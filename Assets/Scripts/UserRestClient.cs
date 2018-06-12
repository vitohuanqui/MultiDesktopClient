using UnityEngine;
using Proyecto26;
using Models;
using System.Collections.Generic;


public class UserRestClient : MonoBehaviour {

	private RequestHelper currentRequest;
	private UserDocument[] documents;
	public GameObject parent;

	void Start () {
		string basePath = "https://127.0.0.1:8000/";
		PlayerPrefs.SetString ("basePath", basePath);
		RestClient.DefaultRequestHeaders ["Authorization"] = "Token " + SystemInfo.deviceUniqueIdentifier;
		RestClient.GetArray<UserDocument>(basePath + "accounts/documents/").Then(documents => {
			foreach (UserDocument document in documents){
				byte[] b64_bytes = System.Convert.FromBase64String (document.file_b64);
				Texture2D tex = new Texture2D(1,1);
				tex.LoadImage(b64_bytes);
				tex.Apply ();
				GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
				plane.transform.localScale *= 2;

				parent = GameObject.Find("ImageTarget");

				plane.transform.position = new Vector3(document.x, document.y, document.z);
				plane.transform.Rotate (0, 0, 0);
				plane.transform.parent = parent.transform;
				plane.gameObject.tag = "document";
				Material material = new Material(Shader.Find("Diffuse"));
				//material.mainTexture = tex;
				plane.GetComponent<Renderer>().material.mainTexture = tex;
			}
		});
		/*documents = new UserDocument[2];
		documents[0] = new UserDocument("iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAhFBMVEX///8AAAB1dXXU1NReXl60tLTBwcH29vadnZ3Z2dn8/PwPDw8FBQVpaWn5+fnv7+82Njbp6ek+Pj4LCwumpqZRUVHf39/Ozs6JiYkZGRmAgIC7u7sUFBRLS0siIiLIyMgtLS13d3eVlZVkZGQ5OTlDQ0ODg4MeHh6QkJChoaFNTU0wMDBLy4H9AAAE7ElEQVR4nO2dyWLiMAxAbaAsmRDCFpaWFmg70+X//2+g7SEYNRhsSY6jd+wB+RWTyJuslBXFrJM/aGrmmV3r3Gl3yOW+6SiVEPj1hkx+OtVDCsHRhEvwyI5AcMUpqPUe3fCV1S/V+h1VL1F7VsEv7lAVx1NuvwNdzAfqktvuiyc8wWzLLfdNG82wza32QzrGMpyVw3zsqEjPFHMsxX4pyAwpBgCQAec9nFDrUowRTgiIP0BH/cB5nrZKIVACwECGeo4SqmxIkQP/ABrqNUaooAz1X4RQYRnqvv/kJijD9Kjom6AMj4res3B+w8PoNC0nAF3PofgNW2buP/Abit9weNKGI4XXUCEYqvmp4b+xzydqEIaZofjoM38MwTBRvbwsmOpXj1l4CIYHRsZAfJ5466iBGKrxqaFuRWeoCkPR20RxMIbqyVB88xQqHEN1byh6SlEDMlR3hqKfFDUkQ/VuKHqZKA7KUL0ZigsPocIyPEtRPWThgRkmxlrt1D1FDcxQqU1ZMNWPzhPFwRlmG30yIZ67ZuHBGarMWLF1TVH5Dbe7Volda3c6lEqP2zVc4De8TMspVB0M3ebCa2HolKLWw9Cln4ohElfuEayh4ZX7zFwG/EyG5lAwPkN13W7POhqOrlKso+Gho25WaQXT2hteDlZ3w4skYmhJPQxr+Ma3QAxtEUM+xNCWphsmizt8FiMwwSExbH9qClbgNigKQ3PhGQ/oMAKF4TOZ4YTJkKaPfu1IBPbP0BieHxpAgsmQ7tzlK9AyCkNzFw8e0Go2yduioDma+Aku1xO98Ys2PkXG98YnS+PYDFkRQ1vEkA8xtEUM+RBDW8SQDzG0RQz5aNbqWvyG8fdSMawiCEMwMJFhVgzQ+eU0JYnhdRuXbuYFPNpM8SylK+QGKVJ8hzuyWf0Nk+ELkZ/WU6AEJoUhVRmwy2tPWL9Duoqfn0DLKAwHZIb3TIaqS1SUFiwyRpN59xZ9fO7h02kytrBFDPkQQ1vEkA8xtEUM+RBDW+phGH/WJoZViCEfzTKM/1kqhlVcMuzhozh3X+4pJoXzJXglEoVhtjEbg8QWmouiMKS7n8WspERlSHdJ0gOTIeH1JUA3pTCcm+1A4xFoGb5hclZuEw+oShLJ26JvtgQJ8EoEmvfhYvncQWcJX4UkWZstYsiHGNoihnyIoS1iyIcY2lIPw/jnS8WwCjHko1mG8T9LxbCKxhuOKSp/MK7MFDS79R/ZqreMiTZBw7dy01RRojlRkrJVUcrNpqAZcp0ooTPkqfWVEFakewFaFltVQaBGFYGhWkzNpuAAHZmheuMPuvi04VyKZmzBmcfJ6MkWMeRDDG0RQz6aZRj/2yJ+w/h7qRhWIYZ8iKEtYsiHGNoihnyIoS1iyEezxhbxG8bfS8WwCjHkQwxtEUM+xNCWehjGn9PEbxh/LxXDKsSQDzG0RQz5EENbxJCPZmVtYliFGPLRLMP4n6ViWEX8huvS5/xytRQTvVLL1g6fUy7nBV4Vwsas1DKo2tktn6PnO4BWC/orOifl8Fz+921dB6ATmLZkhMURb2YLFji1ZcndfAuWLoJqTHQ+zYF07GRIWIf1VvZugiqhK8R6C6meOGcidNcc3sQKvvDqOsWQv8WJB8FDgjTk9viVoa9UckB3E+A1dAae/I4Us05OViXCgoe8MwNvYD3nP0WcjsP9UOQ6AAAAAElFTkSuQmCC", 10, 10, 50);
		documents[1] = new UserDocument("iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAhFBMVEX///8AAAB1dXXU1NReXl60tLTBwcH29vadnZ3Z2dn8/PwPDw8FBQVpaWn5+fnv7+82Njbp6ek+Pj4LCwumpqZRUVHf39/Ozs6JiYkZGRmAgIC7u7sUFBRLS0siIiLIyMgtLS13d3eVlZVkZGQ5OTlDQ0ODg4MeHh6QkJChoaFNTU0wMDBLy4H9AAAE7ElEQVR4nO2dyWLiMAxAbaAsmRDCFpaWFmg70+X//2+g7SEYNRhsSY6jd+wB+RWTyJuslBXFrJM/aGrmmV3r3Gl3yOW+6SiVEPj1hkx+OtVDCsHRhEvwyI5AcMUpqPUe3fCV1S/V+h1VL1F7VsEv7lAVx1NuvwNdzAfqktvuiyc8wWzLLfdNG82wza32QzrGMpyVw3zsqEjPFHMsxX4pyAwpBgCQAec9nFDrUowRTgiIP0BH/cB5nrZKIVACwECGeo4SqmxIkQP/ABrqNUaooAz1X4RQYRnqvv/kJijD9Kjom6AMj4res3B+w8PoNC0nAF3PofgNW2buP/Abit9weNKGI4XXUCEYqvmp4b+xzydqEIaZofjoM38MwTBRvbwsmOpXj1l4CIYHRsZAfJ5466iBGKrxqaFuRWeoCkPR20RxMIbqyVB88xQqHEN1byh6SlEDMlR3hqKfFDUkQ/VuKHqZKA7KUL0ZigsPocIyPEtRPWThgRkmxlrt1D1FDcxQqU1ZMNWPzhPFwRlmG30yIZ67ZuHBGarMWLF1TVH5Dbe7Volda3c6lEqP2zVc4De8TMspVB0M3ebCa2HolKLWw9Cln4ohElfuEayh4ZX7zFwG/EyG5lAwPkN13W7POhqOrlKso+Gho25WaQXT2hteDlZ3w4skYmhJPQxr+Ma3QAxtEUM+xNCWphsmizt8FiMwwSExbH9qClbgNigKQ3PhGQ/oMAKF4TOZ4YTJkKaPfu1IBPbP0BieHxpAgsmQ7tzlK9AyCkNzFw8e0Go2yduioDma+Aku1xO98Ys2PkXG98YnS+PYDFkRQ1vEkA8xtEUM+RBDW8SQDzG0RQz5aNbqWvyG8fdSMawiCEMwMJFhVgzQ+eU0JYnhdRuXbuYFPNpM8SylK+QGKVJ8hzuyWf0Nk+ELkZ/WU6AEJoUhVRmwy2tPWL9Duoqfn0DLKAwHZIb3TIaqS1SUFiwyRpN59xZ9fO7h02kytrBFDPkQQ1vEkA8xtEUM+RBDW+phGH/WJoZViCEfzTKM/1kqhlVcMuzhozh3X+4pJoXzJXglEoVhtjEbg8QWmouiMKS7n8WspERlSHdJ0gOTIeH1JUA3pTCcm+1A4xFoGb5hclZuEw+oShLJ26JvtgQJ8EoEmvfhYvncQWcJX4UkWZstYsiHGNoihnyIoS1iyIcY2lIPw/jnS8WwCjHko1mG8T9LxbCKxhuOKSp/MK7MFDS79R/ZqreMiTZBw7dy01RRojlRkrJVUcrNpqAZcp0ooTPkqfWVEFakewFaFltVQaBGFYGhWkzNpuAAHZmheuMPuvi04VyKZmzBmcfJ6MkWMeRDDG0RQz6aZRj/2yJ+w/h7qRhWIYZ8iKEtYsiHGNoihnyIoS1iyEezxhbxG8bfS8WwCjHkQwxtEUM+xNCWehjGn9PEbxh/LxXDKsSQDzG0RQz5EENbxJCPZmVtYliFGPLRLMP4n6ViWEX8huvS5/xytRQTvVLL1g6fUy7nBV4Vwsas1DKo2tktn6PnO4BWC/orOifl8Fz+921dB6ATmLZkhMURb2YLFji1ZcndfAuWLoJqTHQ+zYF07GRIWIf1VvZugiqhK8R6C6meOGcidNcc3sQKvvDqOsWQv8WJB8FDgjTk9viVoa9UckB3E+A1dAae/I4Us05OViXCgoe8MwNvYD3nP0WcjsP9UOQ6AAAAAElFTkSuQmCC", 100, 10, 10);
*/
		//RestClient.GetArray<UserDocument>(basePath).Then(allUsers => {
			//documents = JsonHelper.ArrayToJsonString<UserDocument>(allUsers, true);
			//});
		//RestClient.Post("https://jsonplaceholder.typicode.com/posts", newPost).Then(response => {
		//	EditorUtility.DisplayDialog("Status", response.StatusCode.ToString(), "Ok");
		//});
		//RestClient.Put("https://jsonplaceholder.typicode.com/posts/1", updatedPost).Then(response => {
		//	EditorUtility.DisplayDialog("Status", response.StatusCode.ToString(), "Ok");
		//});
		//RestClient.Delete("https://jsonplaceholder.typicode.com/posts/1").Then(response => {
		//	EditorUtility.DisplayDialog("Status", response.StatusCode.ToString(), "Ok");
		//});
		//RestClient.Head("https://jsonplaceholder.typicode.com/posts").Then(response => {
		//	EditorUtility.DisplayDialog("Status", response.StatusCode.ToString(), "Ok");
		//});
	//	StartCoroutine(GetDocuments());
/*		for (int i = 0; i<2; ++i){
		//foreach (UserDocument p in documents){
			Debug.Log(documents [i]);
			byte[] b64_bytes = System.Convert.FromBase64String (documents [i].file);
			Texture2D tex = new Texture2D(1,1);
			tex.LoadImage(b64_bytes);
			tex.Apply ();
			GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
			plane.transform.localScale *= 2;

			parent = GameObject.Find("ImageTarget");

			plane.transform.position = new Vector3(documents [i].x, documents [i].y, documents [i].z);
			plane.transform.Rotate (0, 0, 0);
			plane.transform.parent = parent.transform;
			plane.gameObject.tag = "document";
			Material material = new Material(Shader.Find("Diffuse"));
			//material.mainTexture = tex;
			plane.GetComponent<Renderer>().material.mainTexture = tex;
		}*/
	}
}