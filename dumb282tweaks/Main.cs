using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityModManagerNet;
using UnityEngine;

namespace dumb282tweaks
{
	[EnableReloading]
	public static class Main
	{
		public static UnityModManager.ModEntry MyModEntry { get; private set; }
		public static Settings MySettings { get; private set; }

		public static GameObject wagnerSmokeDeflectorsPrefab;
		public static GameObject witteSmokeDeflectorsPrefab;
		public static GameObject chineseSmokeDeflectorsPrefab;

		public static GameObject streamlinedBoilerPrefab;

		private static bool Load(UnityModManager.ModEntry modEntry)
		{
			Harmony harmony = null;

			try
			{
				MyModEntry = modEntry;
				MySettings = UnityModManager.ModSettings.Load<Settings>(MyModEntry);

				MyModEntry.OnGUI = entry => MySettings.Draw(entry);
				MyModEntry.OnSaveGUI = entry => MySettings.Save(entry);

				string assetPath = Path.Combine(MyModEntry.Path, "assetbundles\\");

				wagnerSmokeDeflectorsPrefab = AssetBundle.LoadFromFile(Path.Combine(assetPath, "wagnersmokedeflectors"))
					.LoadAsset<GameObject>("Assets/WagnerSmokeDeflectors.prefab");
				witteSmokeDeflectorsPrefab = AssetBundle.LoadFromFile(Path.Combine(assetPath, "wittesmokedeflectors"))
					.LoadAsset<GameObject>("Assets/WitteSmokeDeflectors.prefab");
				chineseSmokeDeflectorsPrefab = AssetBundle.LoadFromFile(Path.Combine(assetPath, "chinesesmokedeflectors"))
					.LoadAsset<GameObject>("Assets/Prefab/LocoS282A_Smokebox.prefab");

				streamlinedBoilerPrefab = AssetBundle.LoadFromFile(Path.Combine(assetPath, "streamline"))
					.LoadAsset<GameObject>("Assets/Streamline.prefab");

				harmony = new Harmony(MyModEntry.Info.Id);
				harmony.PatchAll(Assembly.GetExecutingAssembly());
			}
			catch (Exception ex)
			{
				MyModEntry.Logger.LogException($"Failed to load {MyModEntry.Info.DisplayName}:", ex);
				harmony?.UnpatchAll(MyModEntry.Info.Id);
				return false;
			}

			return true;
		}

		// Logger Commands
		public static void Log(string message)
		{
			MyModEntry.Logger.Log(message);
		}

		public static void Warning(string message)
		{
			MyModEntry.Logger.Warning(message);
		}

		public static void Error(string message)
		{
			MyModEntry.Logger.Error(message);
		}
	}
}
