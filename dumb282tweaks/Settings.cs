using System;
using System.ComponentModel;
using UnityEngine;
using UnityModManagerNet;

namespace dumb282tweaks
{

	public class Settings : UnityModManager.ModSettings
	{
		// public enum CabType
		// {
		// 	[Description("Default 282 Cab")] Default,
		// 	[Description("German Cab")] German
		// }

		public enum BoilerType
		{
			[Description("Default Boiler")] Default,
			[Description("Streamlined Boiler")] Streamlined,
		}

		public enum SmokeDeflectorType
		{
			[Description("No smoke deflectors")]
			None,

			[Description("Witte smoke deflectors")]
			Witte,

			[Description("Wagner smoke deflectors")]
			Wagner,

			[Description("Chinese smoke deflectors and more")]
			Chinese,
		}

		// public CabType cabType = CabType.Default;
		public SmokeDeflectorType smokeDeflectorType = SmokeDeflectorType.Wagner;
		public BoilerType boilerType = BoilerType.Default;

		public void Draw(UnityModManager.ModEntry modEntry)
		{
			GUILayout.BeginVertical();

			GUILayout.Label("These settings are applied on train spawn, meaning rejoining the game will refresh all 282 locos to the settings specified here, but if you don't unload the train it will keep whatever settings were there previously. This is a temporary solution until I have a proper GUI implemented.");
			GUILayout.Label("Also, reloading a save will currently break things and the tweaks won't load. This isn't good.");

			GUILayout.Space(2f);

			//todo this is not implemented
			// GUILayout.Label("Cab Type");
			// Settings.cabType = (Settings.CabType) GUILayout.SelectionGrid((int) Settings.cabType, cabTypeTexts, 1, "toggle");

			GUILayout.Label("Smoke Deflector Type");
			smokeDeflectorType = (SmokeDeflectorType) GUILayout.SelectionGrid((int) smokeDeflectorType, Enum.GetNames(typeof(SmokeDeflectorType)), 1, "toggle");

			GUILayout.Label("Boiler Type");
			boilerType = (BoilerType) GUILayout.SelectionGrid((int) boilerType, Enum.GetNames(typeof(BoilerType)), 1, "toggle");

			GUILayout.EndVertical();
		}

		public override void Save(UnityModManager.ModEntry modEntry)
		{
			Save(this, modEntry);
		}
	}
}
