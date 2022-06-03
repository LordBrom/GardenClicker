using UnityEngine;

public class WateringCan : ButtonWithIndicator {

	#region Inspector Assignments

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	protected void Start() {
		this.tooltipText = "Watering Can";
	}

	protected override void Update() {
		base.Update();
	}

	#endregion

	public void HandleWaterCanButton() {
		if (this.WateringCanIsActive()) {
			GameManager.instance.ClearCursorMode();
		} else {
			GameManager.instance.SetCursorMode(GameManager.CurserMode.Water);
		}
	}

	private bool WateringCanIsActive() {
		return GameManager.instance.activeCurserMode == GameManager.CurserMode.Water;
	}

	protected override bool ActiveCondition() {
		return this.WateringCanIsActive();
	}

	public override void ShowTooltip() {
		return;
	}
}
