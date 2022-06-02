using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : ButtonWithIndicator {

	#region Inspector Assignments

	[SerializeField]
	private Seed seed;
	[SerializeField]
	private Item item;

	#endregion
	#region Variables

	private Image image;

	#endregion
	#region Unity Methods

	private void Start() {
		this.image = GetComponent<Image>();
		SetItem(this.item);
	}

	protected override void Update() {
		base.Update();
	}

	#endregion

	public void SetItem(Item item) {
		this.item = item;
		this.image.sprite = item.image;
		this.tooltipText = this.item.name;
	}

	public void HandleInventoryItemButton() {
		if (this.item != null) {
			GameManager.instance.SetCursorMode((Seed)this.item);
		}
	}

	private bool InventoryItemSelected() {
		return GameManager.instance.activeCurserMode == GameManager.CurserMode.Seed && this.item.type == Item.Type.Seed && GameManager.instance.activeSeed == this.item;
	}

	protected override bool ActiveCondition() {
		return this.InventoryItemSelected();
	}

	public override void ShowTooltip() {
		TooltipManager.instance.SetItemTooltip(this.item.name, this.item.description);
	}

}
