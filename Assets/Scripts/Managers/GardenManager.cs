using System.Collections.Generic;
using UnityEngine;
using NateMills.UnityUtility;

public class GardenManager : MonoBehaviour {

	#region Singleton
	public static GardenManager instance;

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
	private GameObject gardenPlotPrefab;

	#endregion
	#region Variables

	private Grid<GardenPlotGridObject> gardenPlotGrid;
	private int gardenPlotWidth = 1;
	private int gardenPlotHeight = 1;

	public bool sprinklerActive;
	public bool autoHarvestActive;
	public bool autoReplantActive;

	public Cooldown sprinklerCooldown { get; private set; }
	//private Cooldown autoHarvestCooldown;

	private float sprinklerCooldownTime = 10 * 60;

	#endregion
	#region Unity Methods

	private void Start() {
		this.BuildGardenPlots(2, 2);

		this.sprinklerCooldown = new Cooldown(this.sprinklerCooldownTime, true);
	}

	private void Update() {
		this.sprinklerActive = UpgradeManager.instance.HasUpgrade("sprinkler") && this.sprinklerActive;
		this.autoHarvestActive = UpgradeManager.instance.HasUpgrade("auto_harvest") && this.autoHarvestActive;
		this.autoReplantActive = UpgradeManager.instance.HasUpgrade("auto_replant") && this.autoReplantActive;

		if (Input.GetMouseButtonDown(0)) {
			GardenPlotGridObject clickedPlot = this.gardenPlotGrid.GetGridObject();
			if (clickedPlot != null) {
				switch (GameManager.instance.activeCurserMode) {
					case GameManager.CurserMode.Water:
						clickedPlot.gardenPlot.WaterPlot();
						break;
					case GameManager.CurserMode.Seed:
						clickedPlot.gardenPlot.PlantSeed(GameManager.instance.activeSeed);
						break;
					case GameManager.CurserMode.Clear:
						clickedPlot.gardenPlot.ClearFlower();
						break;
					default:
						clickedPlot.gardenPlot.HandleClick();
						break;
				}
			}
			if (!Input.GetKey(KeyCode.LeftShift)) {
				GameManager.instance.ClearCursorMode();
			}
		}
		if (Input.GetMouseButtonDown(1) && GameManager.instance.activeCurserMode != GameManager.CurserMode.None) {
			GameManager.instance.ClearCursorMode();
		}

		if (UpgradeManager.instance.HasUpgrade("sprinkler") && this.sprinklerCooldown.TickCooldown(Time.deltaTime) && this.sprinklerActive) {

			for (int x = 0; x < this.gardenPlotGrid.GetWidth(); x++) {
				for (int y = 0; y < this.gardenPlotGrid.GetHeight(); y++) {
					this.gardenPlotGrid.GetGridObject(x, y).gardenPlot.WaterPlot();
				}
			}

			float newCooldown = this.sprinklerCooldownTime;
			if (UpgradeManager.instance.HasUpgrade("fast_sprinkler_1")) {
				newCooldown /= 2;
			}
			if (UpgradeManager.instance.HasUpgrade("fast_sprinkler_2")) {
				newCooldown /= 2;
			}
			this.sprinklerCooldown.StartCooldown(newCooldown);
		}
	}

	#endregion
	#region Save/Load

	public string GetSaveString() {
		List<string> gardenPlotList = new List<string>();
		gardenPlotList.Add(this.gardenPlotGrid.GetWidth() + "_" + this.gardenPlotGrid.GetHeight() + "|");
		for (int x = 0; x < this.gardenPlotGrid.GetWidth(); x++) {
			for (int y = 0; y < this.gardenPlotGrid.GetHeight(); y++) {
				GardenPlot gardenPlot = this.gardenPlotGrid.GetGridObject(x, y).gardenPlot;
				string seedSlug = gardenPlot.seed == null ? "" : gardenPlot.seed.slug;
				float growthAmount = gardenPlot.seed == null ? 0f : gardenPlot.flowerGrowth.currentCooldown;
				float wateredAmount = gardenPlot.isWatered ? gardenPlot.wateredCooldown.currentCooldown : 0f;
				gardenPlotList.Add(x + "_" + y + ":" + seedSlug + ":" + growthAmount + ":" + wateredAmount);
			}
		}
		return string.Join('|', gardenPlotList);
	}

	public void LoadSaveString(string saveString) {
		if (saveString == "") {
			return;
		}
		string[] firstSplit = saveString.Split("||");
		string[] widthHeight = firstSplit[0].Split("_");
		this.BuildGardenPlots(int.Parse(widthHeight[0]), int.Parse(widthHeight[1]));

		if (firstSplit[1] == "") {
			return;
		}
		string[] gardenPlotList = firstSplit[1].Split("|");
		foreach (string gardenPlotData in gardenPlotList) {
			string[] gardenPlotDetailSplit = gardenPlotData.Split(':');
			string[] coords = gardenPlotDetailSplit[0].Split("_");
			GardenPlot gardenPlot = this.gardenPlotGrid.GetGridObject(int.Parse(coords[0]), int.Parse(coords[1])).gardenPlot;
			gardenPlot.LoadPlot(gardenPlotDetailSplit[1], float.Parse(gardenPlotDetailSplit[2]), float.Parse(gardenPlotDetailSplit[3]));
		}
	}

	public void LoadOtherSettings(bool sprinklerActive, float sprinklerTime, bool autoHarvestActive, bool autoReplantActive) {
		this.sprinklerActive = sprinklerActive;
		this.sprinklerCooldown.StartCooldown(sprinklerTime);
		this.autoHarvestActive = autoHarvestActive;
		this.autoReplantActive = autoReplantActive;
	}

	#endregion

	private void BuildGardenPlots(int width = 1, int height = 1) {
		if (this.gardenPlotGrid != null) {
			for (int x = 0; x < this.gardenPlotGrid.GetWidth(); x++) {
				for (int y = 0; y < this.gardenPlotGrid.GetHeight(); y++) {
					Destroy(this.gardenPlotGrid.GetGridObject(x, y).gardenPlot.gameObject);
				}
			}
		}

		this.gardenPlotWidth = width;
		this.gardenPlotHeight = height;

		this.gardenPlotGrid = new Grid<GardenPlotGridObject>(this.gardenPlotWidth, this.gardenPlotHeight, 10, Vector3.zero, (Grid<GardenPlotGridObject> g, int x, int y) => new GardenPlotGridObject(g, x, y));

		for (int x = 0; x < this.gardenPlotGrid.GetWidth(); x++) {
			for (int y = 0; y < this.gardenPlotGrid.GetHeight(); y++) {
				GardenPlot newGardenPlot = Instantiate(gardenPlotPrefab, this.transform).GetComponent<GardenPlot>();
				GardenPlotGridObject gardenPlotGridObject = this.gardenPlotGrid.GetGridObject(x, y);
				newGardenPlot.SetGridObject(gardenPlotGridObject, this.gardenPlotGrid.GetWorldPosition(x, y));
				gardenPlotGridObject.SetGardenPlot(newGardenPlot);
			}
		}
	}

	public void UpgradeGardePlotSize(int width, int height) {
		string saveString = this.GetSaveString();
		saveString = width + "_" + height + "||" + saveString.Split("||")[1];
		this.LoadSaveString(saveString);
	}

}
