using UnityEngine;

[System.Serializable]
public struct Character
{
	public Sprite image;
	public string name;
	/*
	[Range(0, 100)] public float speed;
	[Range(0, 100)] public float power;
	*/

	[Range(0, 10)] public float speed;
	[Range(0, 10)] public float power;
	public int price;

	public bool isPurchased;
}