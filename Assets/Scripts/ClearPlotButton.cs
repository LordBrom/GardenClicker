using UnityEngine;

public class ClearPlotButton : ButtonWithIndicator {

	#region Inspector Assignments

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	#endregion

	public void HandleClearButton() {
		if (this.ClearingIsActive()) {
			GameManager.instance.ClearCursorMode();
		} else {
			GameManager.instance.SetCursorMode(GameManager.CurserMode.Clear);
		}
	}

	private bool ClearingIsActive() {
		return GameManager.instance.activeCurserMode == GameManager.CurserMode.Clear;
	}

	protected override bool ActiveCondition() {
		return this.ClearingIsActive();
	}

	public override void ShowTooltip() {
		TooltipManager.instance.SetToolTip("Remove any growth from a plot. You won't get the seed back.");
	}

}
