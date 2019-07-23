using System.Numerics;
public static class Constants {
	
	public static readonly string NEW_RECORD = "CONGRATS!\nNEW RECORD!";

	public static readonly float SPRITES_ANGLE_OFFSET = 90f;
	public static readonly float EPSILON = 0.001f;
	public static readonly float FADE = 1f;
	public static readonly float MOVEMENT_TIMEOUT_DIVISOR = 10f;

	public static readonly int MIN_LEVEL = 1;
	public static readonly int MAX_LEVEL = 9;

	public static class AngleFromToDirection {
		public static readonly float DOWN_TO_LEFT = 0f;
		public static readonly float UP_TO_LEFT = 90f;
		public static readonly float UP_TO_RIGHT = 180f;
		public static readonly float DOWN_TO_RIGHT = 270f;
	}

	public static class Settings {
		public static readonly string MUSIC_KEY = "MusicVolume";
		public static readonly string SFX_KEY = "SFXVolume";
	}
}