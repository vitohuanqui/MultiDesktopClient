using System;

namespace Models
{
	[Serializable]
	public class LoginUser
	{
		public string token;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

