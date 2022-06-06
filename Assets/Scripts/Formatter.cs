using UnityEngine;

public static class Formatter {

	#region Variables

	//private static CultureInfo ci = new CultureInfo("en-us");

	private static int hourInSeconds = 60 * 60;
	private static int minuteInSeconds = 60;

	#endregion

	public static string TimeFormat(float time) {
		string timeString = "";
		float seconds = time;
		if (time >= Formatter.hourInSeconds) {
			int hours = Mathf.FloorToInt(time / Formatter.hourInSeconds);
			time %= Formatter.hourInSeconds;
			timeString += Formatter.NumberFormat(hours) + ":";
		}
		if (time >= Formatter.minuteInSeconds) {
			int minutes = Mathf.FloorToInt(time / Formatter.minuteInSeconds);
			time %= Formatter.minuteInSeconds;
			timeString += minutes.ToString("00") + ":";
		}
		if (time < 60) {
			timeString += time.ToString("0");
		}

		return timeString;
	}

	public static string NumberFormat(float num) {
		string numString;
		if (num < 100000) {
			numString = num.ToString("N0");
		} else {
			numString = num.ToString("E01");
		}
		return numString;
	}

	//public static string NumberFormat(int num) {
	//	string numString = num.ToString("E03");

	//	return numString;
	//}

}
