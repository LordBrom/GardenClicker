using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : ButtonWithIndicator {

	#region Inspector Assignments

	[SerializeField]
	private Flower flower;

	#endregion
	#region Variables

	private Image image;

	#endregion
	#region Unity Methods

	private void Start() {
		this.image = GetComponent<Image>();
		SetFlower(this.flower);
	}

	protected override void Update() {
		base.Update();
	}

	#endregion

	public void SetFlower(Flower flower) {
		this.flower = flower;
		this.image.sprite = flower.seedSprite;
		this.tooltipText = this.flower.name;
	}

	public void HandleInventoryItemButton() {
		if (this.flower != null) {
			GameManager.instance.SetCursorMode(this.flower);
		}
	}

	private bool InventoryItemSelected() {
		return GameManager.instance.activeCurserMode == GameManager.CurserMode.Plant && GameManager.instance.activeFlower == this.flower;
	}

	protected override bool ActiveCondition() {
		return this.InventoryItemSelected();
	}
}
