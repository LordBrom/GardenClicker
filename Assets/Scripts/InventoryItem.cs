using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : ButtonWithIndicator {

	#region Inspector Assignments

	[SerializeField]
	private TextMeshProUGUI itemCountText;
	[SerializeField]
	private Image itemImage;

	#endregion
	#region Variables

	public Item item { get; private set; }
	public int itemCount;

	#endregion
	#region Unity Methods

	private void Awake() {
	}

	protected override void Update() {
		base.Update();
		this.SetItemCountText();

		if (this.isHovering && Input.GetMouseButtonUp(1)) {
			InventoryManager.instance.SellItem(this.item, Input.GetKey(KeyCode.LeftControl) ? this.itemCount : 1);
		}
	}

	#endregion

	public void AddItem(Item item, int count) {
		if (this.item == null) {
			this.SetItem(item);
		}
		this.itemCount += count;
	}

	public void SetItem(Item item, int count = 0) {
		this.item = item;
		this.itemImage.sprite = item.image;
		this.itemImage.enabled = true;
		this.tooltipText = this.item.name;
		this.itemCount = count;
	}

	public void ClearItem() {
		this.item = null;
		this.itemImage.sprite = null;
		this.itemImage.enabled = false;
		this.tooltipText = "";
		this.itemCount = 0;
	}

	public void HandleInventoryItemButton() {
		if (this.item != null && this.item.type == Item.Type.Seed) {
			GameManager.instance.SetCursorMode((Seed)this.item);
		}
	}

	private void SetItemCountText() {
		if (this.item == null) {
			itemCountText.text = "";
			return;
		}
		itemCountText.text = this.itemCount.ToString();
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
			TooltipManager.instance.SetItemTooltip(this.item);
		}
	}

}
