using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Custom/Item")]
public class Item : ScriptableObject {

	#region Variables

	public new string name;
	public string slug;
	public string description;
	public Sprite image;
	public Type type = Type.Product;

	public int sellValue;

	public bool inShop;

	public enum Type {
		Seed,
		Product
	}

	#endregion

	public Item() {
	}

}
