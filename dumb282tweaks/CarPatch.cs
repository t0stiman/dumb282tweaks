using DV.ThingTypes;
using HarmonyLib;
using UnityEngine;

using static dumb282tweaks.Main;

namespace dumb282tweaks;

[HarmonyPatch(typeof(TrainCar), "Start")]
class CarPatch {
	static void Postfix(ref TrainCar __instance)
	{
		if(__instance != null && __instance.carType == TrainCarType.LocoSteamHeavy)
		{
			string bodyPath = "LocoS282A_Body/Static_LOD0/s282_locomotive_body";
			Transform s282Body = __instance.transform.Find(bodyPath);
			if (s282Body == null)
			{
				Error($"Couldn't find S282 body on '{__instance.transform.gameObject.name}' -> {bodyPath}");
				return;
			}

			Material s282Mat = s282Body.GetComponent<MeshRenderer>().material;

			// Smoke Deflector
			switch(Main.Settings.smokeDeflectorType) {
				case Settings.SmokeDeflectorType.Witte:
					Log("witte");

					GameObject witteSmokeDeflector = Object.Instantiate(witteSmokeDeflectorsLoad, __instance.transform);
					witteSmokeDeflector.transform.localPosition = new Vector3(0.0f, 2.1f, 5f);
					witteSmokeDeflector.transform.localRotation = Quaternion.identity;

					foreach (var aMeshRenderer in witteSmokeDeflector.GetComponentsInChildren<MeshRenderer>())
					{
						aMeshRenderer.material = s282Mat;
					}

					break;
				case Settings.SmokeDeflectorType.Wagner:
					Log("witten't");

					GameObject wagnerSmokeDeflector = Object.Instantiate(wagnerSmokeDeflectorsLoad, __instance.transform);
					wagnerSmokeDeflector.transform.localPosition = new Vector3(0.0f, 2.1f, 5f);
					wagnerSmokeDeflector.transform.localRotation = Quaternion.identity;

					foreach (var aMeshRenderer in wagnerSmokeDeflector.GetComponentsInChildren<MeshRenderer>())
					{
						aMeshRenderer.material = s282Mat;
					}

					break;
			}

			// Boiler
			switch (Main.Settings.boilerType)
			{
				case Settings.BoilerType.Streamlined:
					GameObject streamlinedBoiler = Object.Instantiate(streamlinedBoilerLoad, __instance.transform);
					streamlinedBoiler.transform.localPosition = new Vector3(0.0f, 2.15f, 5.1f);
					streamlinedBoiler.transform.localRotation = Quaternion.identity;

					foreach (var aMeshRenderer in streamlinedBoiler.GetComponentsInChildren<MeshRenderer>())
					{
						aMeshRenderer.material = s282Mat;
					}

					break;
			}
		}
	}
}
