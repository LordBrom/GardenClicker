using UnityEngine;

[CreateAssetMenu(fileName = "New Flower", menuName = "Custom/Flower")]
public class Flower : ScriptableObject {

	public new string name;

	public Sprite seedSprite;
	public Sprite[] growthSprites;

	public float growTime;


}
