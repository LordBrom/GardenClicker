using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Custom/Item")]
public class Item : ScriptableObject {

	#region Variables

	public static Dictionary<int, Item> lookup = new Dictionary<int, Item>();

	[SerializeField]
	public int id { get; private set; } = Item.lookup.Count + 1;
	public new string name;
	public string description;
	public Sprite image;
	public Type type = Type.Product;

	public int sellValue;

	public enum Type {
		Seed,
		Product
	}

	public Item() {
		if (!Item.lookup.ContainsKey(this.id)) {
			Item.lookup.Add(id, this);
		} else {
			Item.lookup[id] = this;
		}
	}

	#endregion
}
