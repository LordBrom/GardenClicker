using UnityEngine;
using NateMills.UnityUtility;

public class GardenPlotGridObject : Grid<GardenPlotGridObject>.GridObject {

	#region Inspector Assignments

	#endregion
	#region Variables

	public string flowerType;
	public GardenPlot gardenPlot { get; private set; }

	#endregion

	public GardenPlotGridObject(Grid<GardenPlotGridObject> grid, int x, int y) : base(grid, x, y) {
		this.flowerType = "Weeds,\nGet better seeds!";
	}

	public void SetGardenPlot(GardenPlot gardenPlot) {
		this.gardenPlot = gardenPlot;
	}
}
