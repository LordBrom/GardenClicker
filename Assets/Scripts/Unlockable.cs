using System;
using UnityEngine;
using NateMills.UnityUtility;


public class Unlockable : HidableMenu {

	#region Inspector Assignments

	#endregion
	#region Variables

	public Func<bool> Condition;

	#endregion
	#region Unity Methods
	protected override void Awake() {
		base.Awake();
		if (this.Condition == null) {
			this.Condition = this.ConditionsMet;
		}
	}

	protected override void Start() {
		if (this.Condition()) {
			this.ShowMenu();
		} else {
			this.HideMenu();
		}
	}

	private void Update() {
		if (!this.isShown && this.Condition()) {
			this.ShowMenu();
		}
	}

	#endregion

	protected bool ConditionsMet() {
		Debug.LogWarning("Unlock conditions not implemented for " + this.name);
		return false;
	}
}
