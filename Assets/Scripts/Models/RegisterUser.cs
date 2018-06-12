using System;

namespace Models
{
	[Serializable]
	public class RegisterUser
	{
		public string token;
		public string username;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

