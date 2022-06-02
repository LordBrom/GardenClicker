using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Custom/Seed")]
public class Seed : Item {

	#region Variables

	public Flower flower;

	public Seed() {
		this.type = Item.Type.Seed;
	}
	#endregion
}
