using UnityEngine;
using NateMills.UnityUtility;

public class GardenPlot : MonoBehaviour {

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

	private bool isWatered;
	private Cooldown wateredCooldown;
	[SerializeField]
	private float waterDuration;

	public Cooldown flowerGrowth { get; private set; }
	private int currentGrowthStage;

	#endregion

	#region Unity Methods
	private void Start() {
		this.spriteRenderer = GetComponent<SpriteRenderer>();

		this.wateredCooldown = new Cooldown(this.waterDuration, true);
		this.spriteRenderer.sprite = this.dryDirtSprite;
		this.isWatered = false;
	}

	private void Update() {
		if (this.isWatered && this.wateredCooldown.TickCooldown(Time.deltaTime)) {
			this.spriteRenderer.sprite = this.dryDirtSprite;
			this.isWatered = false;
		}

		if (this.flower != null) {
			this.flowerGrowth.TickCooldown(Time.deltaTime * (this.isWatered ? 2 : 1));
			int flowerGrowthStage = Mathf.FloorToInt(this.flowerGrowth.PercentComplete() * (this.flower.growthSprites.Length - 1));
			if (this.currentGrowthStage != flowerGrowthStage) {
				this.flowerSpriteRenderer.sprite = this.flower.growthSprites[flowerGrowthStage];
				this.currentGrowthStage = flowerGrowthStage;
			}
		}
	}
	#endregion

	public void SetGridObject(GardenPlotGridObject gridObject, Vector3 worldPosition) {
		this.gridObject = gridObject;
		this.transform.position = worldPosition;
	}

	public void SetFlower(Flower flower) {
		this.flower = flower;
		this.currentGrowthStage = -1;
		this.flowerGrowth = new Cooldown(this.flower.growTime);
	}

	public void ClearFlower() {
		this.flower = null;
		this.flowerSpriteRenderer.sprite = null;
	}

	public void WaterPlot() {
		if (this.isWatered) {
			return;
		}
		this.isWatered = true;
		this.wateredCooldown.StartCooldown();
		this.spriteRenderer.sprite = this.wetDirtSprite;
	}
}
