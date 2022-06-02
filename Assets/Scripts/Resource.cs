using UnityEngine;

public class Resource {

	#region Inspector Assignments

	#endregion
	#region Variables

	public string name;
	public int amountHeld { get; private set; }

	#endregion
	/*#region Unity Methods

	private void Start() {

	}

	private void Update() {

	}

	#endregion*/

	public Resource(string name, int startAmount = 0) {
		this.name = name;
		this.amountHeld = startAmount;
	}

	public bool SpendResource(int amountToSpend) {
		if (this.amountHeld >= amountToSpend) {
			this.amountHeld -= amountToSpend;
			return true;
		}
		return false;
	}

	public void GainResource(int amountGained) {
		this.amountHeld += amountGained;
	}
}
