using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton
	public static Inventory instance;

	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
	#endregion
	#region Inspector Assignments

	[SerializeField]
	private GameObject inventoryItemPrefab;
	[SerializeField]
	private Seed startSeed;

	private int inventorySize = 20;

	#endregion
	#region Variables

	private InventoryItem[] inventory;

	#endregion
	#region Unity Methods

	private void Start() {
		this.BuildInventory();

		this.AddToInventory(this.startSeed, 5);

	}

	private void Update() {

	}

	#endregion

	private void BuildInventory() {
		this.inventory = new InventoryItem[this.inventorySize];
		for (int i = 0; i < this.inventorySize; i++) {
			this.inventory[i] = Instantiate(this.inventoryItemPrefab, this.transform).GetComponent<InventoryItem>();
		}
	}

	public bool AddToInventory(Item item, int count = 1) {
		int useSlot = -1;
		for (int i = 0; i < this.inventory.Length; i++) {
			if (useSlot == -1 && this.inventory[i].item == null) {
				useSlot = i;
			}
			if (this.inventory[i].item == item) {
				useSlot = i;
				break;
			}
		}

		if (useSlot == -1) {
			return false;
		}
		this.inventory[useSlot].AddItem(item, count);
		return true;
	}

	public bool RemoveFromInventory(Item item, int count) {
		for (int i = 0; i < this.inventory.Length; i++) {
			if (this.inventory[i].item == item) {
				if (this.inventory[i].itemCount >= count) {
					this.inventory[i].itemCount -= count;
					if (this.inventory[i].itemCount == 0) {
						this.inventory[i].ClearItem();
					}
					return true;
				}
				return false;
			}
		}
		return false;
	}
}
