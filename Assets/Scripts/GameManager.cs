using UnityEngine;
using TMPro;
using NateMills.UnityUtility;

public class GameManager : MonoBehaviour {

	#region Singleton
	public static GameManager instance;

	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
	#endregion
	#region Inspector Assignments

	public Seed activeSeed;

	[SerializeField]
	private GameObject gardenPlotPrefab;
	[SerializeField]
	private Transform gardenTransform;

	#endregion
	#region Variables

	private Grid<GardenPlotGridObject> gardenPlotGrid;
	private int gardenPlotWidth = 1;
	private int gardenPlotHeight = 1;
	public CurserMode activeCurserMode { get; private set; }

	public enum CurserMode {
		None,
		Water,
		Seed,
	}

	public Resource goldResource;
	[SerializeField]
	private TextMeshProUGUI goldText;

	#endregion

	#region Unity Methods
	private void Start() {
		this.goldResource = new Resource("Gold");
		this.gardenPlotGrid = new Grid<GardenPlotGridObject>(this.gardenPlotWidth, this.gardenPlotHeight, 10, Vector3.zero, (Grid<GardenPlotGridObject> g, int x, int y) => new GardenPlotGridObject(g, x, y));

		for (int x = 0; x < this.gardenPlotGrid.GetWidth(); x++) {
			for (int y = 0; y < this.gardenPlotGrid.GetHeight(); y++) {
				GardenPlot newGardenPlot = Instantiate(gardenPlotPrefab, gardenTransform).GetComponent<GardenPlot>();
				GardenPlotGridObject gardenPlotGridObject = this.gardenPlotGrid.GetGridObject(x, y);
				newGardenPlot.SetGridObject(gardenPlotGridObject, this.gardenPlotGrid.GetWorldPosition(x, y));
				gardenPlotGridObject.SetGardenPlot(newGardenPlot);
			}
		}
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			GardenPlotGridObject clickedPlot = this.gardenPlotGrid.GetGridObject();
			if (clickedPlot != null) {
				switch (this.activeCurserMode) {
					case CurserMode.Water:
						clickedPlot.gardenPlot.WaterPlot();
						break;
					case CurserMode.Seed:
						clickedPlot.gardenPlot.PlantSeed(this.activeSeed);
						break;
					default:
						clickedPlot.gardenPlot.HandleClick();
						break;
				}
			}
			if (!Input.GetKey(KeyCode.LeftShift)) {
				this.ClearCursorMode();
			}
		}
		if (Input.GetMouseButtonDown(1) && this.activeCurserMode != CurserMode.None) {
			this.ClearCursorMode();
		}
		this.goldText.text = this.goldResource.amountHeld.ToString();
	}
	#endregion

	public void ClearCursorMode() {
		this.activeCurserMode = CurserMode.None;
		this.activeSeed = null;
	}
	public void SetCursorMode(CurserMode curserMode) {
		this.activeCurserMode = curserMode;
	}
	public void SetCursorMode(Seed seed) {
		this.activeCurserMode = CurserMode.Seed;
		this.activeSeed = seed;
	}
}
