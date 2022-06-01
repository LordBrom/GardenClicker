using UnityEngine;
using NateMills.UnityUtility;

public class GardenPlot : MonoBehaviour {

	#region Inspector Assignments

	[SerializeField]
	private Sprite dryDirtSprite;
	[SerializeField]
	private Sprite wetDirtSprite;

	#endregion
	#region Variables

	private SpriteRenderer spriteRenderer;

	private GardenPlotGridObject gridObject;

	private Flower flower;

	private bool isWatered;
	private Cooldown wateredCooldown;
	[SerializeField]
	private float waterDuration;

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
	}
	#endregion

	public void SetGridObject(GardenPlotGridObject gridObject) {
		this.gridObject = gridObject;
		this.transform.position = new Vector3(gridObject.x, gridObject.y);
	}

	public void SetFlower(Flower flower) {
		this.flower = flower;
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
