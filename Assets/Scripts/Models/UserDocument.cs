using System;

namespace Models
{
	[Serializable]
	public class UserDocument	{
		public string file_b64;
		public int x;
		public int y;
		public int z;
		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}/*
		public UserDocument(string _file, int _x, int _y, int _z){
			file = _file;
			x = _x;
			y = _y;
			z = _z;
		}*/
	}
}

