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


	#endregion

	public void HandleSprinklerButton() {
		GardenManager.instance.sprinklerActive = !GardenManager.instance.sprinklerActive;
	}

	public override void UpdateToolTip() {
		TooltipManager.instance.SetToolTip("Sprinklers: " + (!this.ActiveCondition() ? "On" : "Off") + "\nnext sprinkle: " + Formatter.TimeFormat(GardenManager.instance.sprinklerCooldown.GetRemainingTime()));
	}

	protected override bool ActiveCondition() {
		return !GardenManager.instance.sprinklerActive;
	}

	private bool UnlockCondition() {
		return UpgradeManager.instance.HasUpgrade("sprinkler");
	}

}
