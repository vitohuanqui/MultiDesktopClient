using System;

namespace Models
{
	[Serializable]
	public class User
	{
		public string username;

		public string password;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

