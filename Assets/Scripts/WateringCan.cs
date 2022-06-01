using UnityEngine;

public class WateringCan : MonoBehaviour {

	#region Inspector Assignments

	[SerializeField]
	private GameObject activeIndicatorObject;

	#endregion
	#region Variables

	#endregion

	#region Unity Methods
	private void Update() {
		activeIndicatorObject.SetActive(GameManager.instance.activeCurserMode == GameManager.CurserMode.Water);
	}
	#endregion

	public void HandleWaterCanButton() {
		if (GameManager.instance.activeCurserMode == GameManager.CurserMode.Water) {
			GameManager.instance.ClearCursorMode();
		} else {
			GameManager.instance.SetCursorMode(GameManager.CurserMode.Water);
		}
	}
}
