using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityModManagerNet;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

namespace dumb282tweaks;

public static class Main {
	// Variables
	public static UnityModManager.ModEntry Instance { get; private set; }
	public static dumb282tweaksSettings Settings { get; private set; }

	// public static readonly string[] cabTypeTexts = new[] {
	// 	"Default",
	// 	"German"
	// };
	public static readonly string[] smokeDeflectorTypeTexts = new[] {
		"None",
		"Witte",
		"Wagner"
	};
	public static readonly string[] boilerTypeTexts = new[] {
		"Default",
		"Streamlined"
	};
	// public static readonly string[] smokeStackTypeTexts = new[] {
	// 	"Default",
	// 	"Short",
	// };

	public static GameObject wagnerSmokeDeflectorsLoad;
	public static GameObject witteSmokeDeflectorsLoad;
	public static GameObject streamlinedBoilerLoad;

	// Load
	private static bool Load(UnityModManager.ModEntry modEntry) {
		Harmony? harmony = null;

		try {
			Instance = modEntry;
			Settings = UnityModManager.ModSettings.Load<dumb282tweaksSettings>(Instance);

			Instance.OnGUI = OnGUI;
			Instance.OnSaveGUI = OnSaveGUI;

			harmony = new Harmony(Instance.Info.Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			string assetPath = Path.Combine(Instance.Path, "assets\\");

			wagnerSmokeDeflectorsLoad = AssetBundle.LoadFromFile(Path.Combine(assetPath, "wagnersmokedeflectors")).LoadAsset<GameObject>("Assets/WagnerSmokeDeflectors.prefab");
			witteSmokeDeflectorsLoad = AssetBundle.LoadFromFile(Path.Combine(assetPath, "wittesmokedeflectors")).LoadAsset<GameObject>("Assets/WitteSmokeDeflectors.prefab");
			streamlinedBoilerLoad = AssetBundle.LoadFromFile(Path.Combine(assetPath, "streamline")).LoadAsset<GameObject>("Assets/Streamline.prefab");

		} catch(Exception ex) {
			Instance.Logger.LogException($"Failed to load {Instance.Info.DisplayName}:", ex);
			harmony?.UnpatchAll(Instance.Info.Id);
			return false;
		}

		return true;
	}

	// GUI Rendering
	static void OnGUI(UnityModManager.ModEntry modEntry) {
		GUILayout.BeginVertical();

		GUILayout.Label("These settings are applied on train spawn, meaning rejoining the game will refresh all 282 locos to the settings specified here, but if you don't unload the train it will keep whatever settings were there previously. This is a temporary solution until I have a proper GUI implemented.");
		GUILayout.Label("Also, reloading a save will currently break things and the tweaks won't load. This isn't good.");

		GUILayout.Space(2f);

		//todo this is not implemented
		// GUILayout.Label("Cab Type");
		// Settings.cabType = (Settings.CabType) GUILayout.SelectionGrid((int) Settings.cabType, cabTypeTexts, 1, "toggle");

		GUILayout.Label("Smoke Deflector Type");
		Settings.smokeDeflectorType = (Settings.SmokeDeflectorType) GUILayout.SelectionGrid((int) Settings.smokeDeflectorType, smokeDeflectorTypeTexts, 1, "toggle");

		GUILayout.Label("Boiler Type");
		Settings.boilerType = (Settings.BoilerType) GUILayout.SelectionGrid((int) Settings.boilerType, boilerTypeTexts, 1, "toggle");

		GUILayout.EndVertical();
	}
	static void OnSaveGUI(UnityModManager.ModEntry modEntry) {
		Settings.Save(modEntry);
	}

	// Logger Commands
	public static void Log(string message) {
		Instance.Logger.Log(message);
	}
	public static void Warning(string message) {
		Instance.Logger.Warning(message);
	}
	public static void Error(string message) {
		Instance.Logger.Error(message);
	}
}
