using UnityEngine;
using NateMills.UnityUtility;

public class GardenPlot : Tooltip {

	#region Inspector Assignments

	[SerializeField]
	private Sprite dryDirtSprite;
	[SerializeField]
	private Sprite wetDirtSprite;
	[SerializeField]
	private SpriteRenderer flowerSpriteRenderer;

	#endregion
	#region Variables

	private SpriteRenderer spriteRenderer;

	private GardenPlotGridObject gridObject;

	public Flower flower { get; private set; }
	public Seed seed { get; private set; }

	public bool isWatered { get; private set; }
	public Cooldown wateredCooldown { get; private set; } = new Cooldown(0);
	[SerializeField]
	private float waterDuration = 60f;

	public Cooldown flowerGrowth { get; private set; } = new Cooldown(0);
	private int currentGrowthStage;

	public int harvestCount;

	#endregion
	#region Unity Methods

	private void Awake() {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.wateredCooldown = new Cooldown(this.waterDuration, true);
		this.spriteRenderer.sprite = this.dryDirtSprite;
		this.isWatered = false;
	}
	private void Start() {

	}

	protected override void Update() {
		base.Update();
		if (this.isWatered && this.wateredCooldown.TickCooldown(Time.deltaTime)) {
			this.spriteRenderer.sprite = this.dryDirtSprite;
			this.isWatered = false;
		}

		if (this.flower != null) {
			//this.tooltipText = this.flower.name + " (" + Mathf.FloorToInt(this.flowerGrowth.PercentComplete(false)) + "%)";
			this.flowerGrowth.tickMultiplier = this.isWatered ? 2 : 1;
			if (this.flowerGrowth.TickCooldown(Time.deltaTime) && GardenManager.instance.autoHarvestActive) {
				this.HarvestPlot();
			} else {
				int flowerGrowthStage = Mathf.FloorToInt(this.flowerGrowth.PercentComplete() * (this.flower.growthSprites.Length - 1));
				if (this.currentGrowthStage != flowerGrowthStage) {
					this.flowerSpriteRenderer.sprite = this.flower.growthSprites[flowerGrowthStage];
					this.currentGrowthStage = flowerGrowthStage;
				}
			}
		} else {
			this.tooltipText = "Nothing Planted";
		}
	}

	#endregion

	private void SetSeed(Seed seed) {
		this.flower = seed.flower;
		this.seed = seed;
		this.currentGrowthStage = -1;
		this.flowerGrowth = new Cooldown(this.flower.growTime);
		this.harvestCount = 0;
	}

	public void SetGridObject(GardenPlotGridObject gridObject, Vector3 worldPosition) {
		this.gridObject = gridObject;
		this.transform.position = worldPosition;
	}

	public void PlantSeed(Seed seed) {
		if (this.flower == null && InventoryManager.instance.RemoveFromInventory(seed, 1)) {
			this.SetSeed(seed);
		}
	}

	public void ClearFlower() {
		this.flower = null;
		this.flowerSpriteRenderer.sprite = null;
	}

	private void HarvestPlot() {
		foreach (HarvestDrop harvestDrop in this.flower.harvestDrops) {
			if (Random.Range(0, 100) <= harvestDrop.dropChance) {
				int dropCount = Random.Range(harvestDrop.dropCount.x, harvestDrop.dropCount.y);
				InventoryManager.instance.AddToInventory(harvestDrop.item, dropCount);
			}
		}
		this.harvestCount++;
		if (this.harvestCount < this.flower.harvestCount) {
			this.currentGrowthStage = -1;
			this.flowerGrowth.StartCooldown(this.flower.growTime / 3);
		} else if (UpgradeManager.instance.HasUpgrade("auto_replant") && InventoryManager.instance.RemoveFromInventory(this.seed, 1)) {
			this.currentGrowthStage = -1;
			this.flowerGrowth = new Cooldown(this.flower.growTime);
		} else {
			this.ClearFlower();
		}
	}

	public void WaterPlot() {
		if (this.isWatered) {
			return;
		}
		this.isWatered = true;
		this.wateredCooldown.StartCooldown(this.waterDuration * (UpgradeManager.instance.HasUpgrade("spungy_dirt_1") ? 2 : 1));
		this.spriteRenderer.sprite = this.wetDirtSprite;
	}

	public void HandleClick() {
		if (this.flower == null) {
			return;
		}
		if (this.flowerGrowth.TickCooldown(1)) {
			HarvestPlot();
		}
	}


	public override void ShowTooltip() {
		TooltipManager.instance.SetGardenPlotTooltip(this);
	}
	public override void UpdateToolTip() {
		TooltipManager.instance.SetGardenPlotTooltip(this);
	}

	public void LoadPlot(string seedSlug, float growthAmount, float waterAmount) {
		if (seedSlug != "") {
			Item seedItem = GameManager.itemLookUp[seedSlug];
			if (seedItem.type == Item.Type.Seed) {
				Seed seed = (Seed)seedItem;
				this.SetSeed(seed);
				this.flowerGrowth.StartCooldown(growthAmount);
			}
		}
		if (waterAmount > 0) {
			this.wateredCooldown.StartCooldown(waterAmount);
			this.isWatered = true;
			this.spriteRenderer.sprite = this.wetDirtSprite;
		}
	}
}
