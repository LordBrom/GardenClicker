using UnityEngine;
using NateMills.UnityUtility;

public class ButtonWithIndicator : Tooltip {

	#region Inspector Assignments

	[SerializeField]
	private GameObject activeIndicatorObject;

	#endregion
	#region Variables

	#endregion
	#region Unity Methods

	protected virtual void Update() {
		activeIndicatorObject.SetActive(this.ActiveCondition());
	}

	#endregion

	protected virtual bool ActiveCondition() {
		return false;
	}
}
