public static class Constants {
	public static readonly float SPRITES_ANGLE_OFFSET = 90f;
	public static readonly float EPSILON = 0.001f;
	public static readonly float FADE = 1f;

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