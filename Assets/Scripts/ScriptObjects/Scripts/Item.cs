using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Custom/Item")]
public class Item : ScriptableObject {

	#region Variables

	public new string name;
	public string description;
	public Sprite image;
	public Type type;

	public int sellValue;

	public enum Type {
		Seed,
		Product
	}

	public Item() {
		this.type = Type.Product;
	}

	#endregion
}
