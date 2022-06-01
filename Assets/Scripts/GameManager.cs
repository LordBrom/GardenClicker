using UnityEngine;
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

	public Flower activeFlower;

	[SerializeField]
	private GameObject gardenPlotPrefab;
	[SerializeField]
	private Transform gardenTransform;

	#endregion
	#region Variables

	private Grid<GardenPlotGridObject> gardenPlotGrid;
	private int gardenPlotWidth = 3;
	private int gardenPlotHeight = 2;
	public CurserMode activeCurserMode { get; private set; }

	public enum CurserMode {
		None,
		Water,
		Plant,
	}

	#endregion

	#region Unity Methods
	private void Start() {
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
					case CurserMode.Plant:
						clickedPlot.gardenPlot.SetFlower(this.activeFlower);
						break;
					default:
						clickedPlot.gardenPlot.HandleClick();
						break;
				}
			}
		}
		if (Input.GetMouseButtonDown(1) && this.activeCurserMode != CurserMode.None) {
			this.ClearCursorMode();
		}
	}
	#endregion

	public void ClearCursorMode() {
		this.activeCurserMode = CurserMode.None;
		this.activeFlower = null;
	}
	public void SetCursorMode(CurserMode curserMode) {
		this.activeCurserMode = curserMode;
	}
	public void SetCursorMode(Flower flower) {
		this.activeCurserMode = CurserMode.Plant;
		this.activeFlower = flower;
	}
}
