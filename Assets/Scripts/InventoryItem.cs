using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : ButtonWithIndicator {

	#region Inspector Assignments

	public Seed seedTest;

	#endregion
	#region Variables

	public Item item { get; private set; }
	public int itemCount { get; private set; }
	private Image image;

	#endregion
	#region Unity Methods

	private void Awake() {
		this.image = GetComponent<Image>();
	}

	protected override void Update() {
		base.Update();
	}

	#endregion

	public void AddItem(Item item, int count) {
		if (this.item == null) {
			this.SetItem(item);
		}
		this.itemCount = count;
	}

	public void SetItem(Item item) {
		this.item = item;
		this.image.sprite = item.image;
		this.tooltipText = this.item.name;
		this.itemCount = 0;
	}

	public void HandleInventoryItemButton() {
		if (this.item != null && this.item.type == Item.Type.Seed) {
			GameManager.instance.SetCursorMode((Seed)this.item);
		}
	}

	private bool InventoryItemSelected() {
		if (this.item != null &&
			GameManager.instance.activeCurserMode == GameManager.CurserMode.Seed &&
			this.item.type == Item.Type.Seed &&
			GameManager.instance.activeSeed == this.item
		) {
			return true;
		}
		return false;
	}

	protected override bool ActiveCondition() {
		return this.InventoryItemSelected();
	}

	public override void ShowTooltip() {
		if (this.item != null) {
			TooltipManager.instance.SetItemTooltip(this.item.name, this.item.description);
		}
	}

}
