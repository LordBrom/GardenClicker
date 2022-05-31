using UnityEngine;
using NateMills.UnityUtils;

public class GardenPlot : Grid<GardenPlot>.GridObject {

	#region Inspector Assignments

	#endregion
	#region Variables

	public string flowerType;

	#endregion

	public GardenPlot(Grid<GardenPlot> grid, int x, int y) : base(grid, x, y) {
		this.flowerType = "Weeds,\nGet better seeds!";
	}

	public override string ToString() {
		return this.flowerType;
	}
}
