using UnityEngine;

[CreateAssetMenu(fileName = "New Flower", menuName = "Custom/Flower")]
public class Flower : ScriptableObject {

	public new string name;

	public Sprite[] growthSprites;

	public float growTime;

	public int harvestReward;
	public HarvestDrops[] harvestDrops;
}

[System.Serializable]
public class HarvestDrops {
	public Item item;
	public float dropChance;
}
