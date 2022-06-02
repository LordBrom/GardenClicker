using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Custom/Upgrade")]
public class Upgrade : ScriptableObject {

	public new string name;
	public int id;
	public int cost;

}
