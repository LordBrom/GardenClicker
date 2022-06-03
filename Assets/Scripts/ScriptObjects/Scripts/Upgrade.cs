using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Custom/Upgrade")]
public class Upgrade : ScriptableObject {

	public static Dictionary<string, Upgrade> lookup = new Dictionary<string, Upgrade>();

	public new string name;
	public string slug = "-";
	public int id;
	public int cost;

	public Upgrade() {
		if (Upgrade.lookup.ContainsKey(this.slug)) {
			Upgrade.lookup[this.slug] = this;
		} else {
			Upgrade.lookup.Add(this.slug, this);
		}
	}

}
