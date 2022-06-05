using UnityEngine;

[CreateAssetMenu(fileName = "New Flower", menuName = "Custom/Flower")]
public class Flower : ScriptableObject {

	public new string name;

	public Sprite[] growthSprites;

	public float growTime;

	public HarvestDrop[] harvestDrops;

	public int harvestCount = 1;
}

[System.Serializable]
public class HarvestDrop {
	public Item item;
	public float dropChance;
	public Vector2Int dropCount = Vector2Int.one;
}
