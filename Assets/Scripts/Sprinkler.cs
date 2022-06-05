using UnityEngine;

public class Sprinkler : ButtonWithIndicator {

	#region Inspector Assignments

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	private void Awake() {
		GetComponent<Unlockable>().Condition = this.UnlockCondition;
	}

	protected override void Update() {
		base.Update();
		if (this.isHovering) {
			TooltipManager.instance.SetToolTip("Sprinklers: " + (!this.ActiveCondition() ? "On" : "Off") + "\nnext sprinkle: " + Formatter.TimeFormat(GardenManager.instance.sprinklerCooldown.currentCooldown));
		}
	}

	#endregion

	public void HandleSprinklerButton() {
		GardenManager.instance.sprinklerActive = !GardenManager.instance.sprinklerActive;
	}

	protected override bool ActiveCondition() {
		return !GardenManager.instance.sprinklerActive;
	}

	private bool UnlockCondition() {
		return UpgradeManager.instance.HasUpgrade("sprinkler");
	}

}
