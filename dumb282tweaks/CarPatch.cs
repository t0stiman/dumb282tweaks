using DV.ThingTypes;
using HarmonyLib;
using UnityEngine;

using static dumb282tweaks.Main;

namespace dumb282tweaks;

[HarmonyPatch(typeof(TrainCar), "Start")]
class CarPatch {
	static void Postfix(ref TrainCar __instance)
	{
		if (__instance == null || __instance.carType != TrainCarType.LocoSteamHeavy)
		{
			return;
		}

		string bodyPath = "LocoS282A_Body/Static_LOD0/s282_locomotive_body";
		Transform s282Body = __instance.transform.Find(bodyPath);
		if (s282Body == null)
		{
			Error($"Couldn't find S282 body on '{__instance.transform.gameObject.name}' -> {bodyPath}");
			return;
		}

		Material s282Mat = s282Body.GetComponent<MeshRenderer>().material;

		// Smoke Deflector
		Log($"Applying {MySettings.smokeDeflectorType.ToString()}");

		switch(MySettings.smokeDeflectorType) {
			case Settings.SmokeDeflectorType.Witte:
			{
				GameObject witteSmokeDeflector = Object.Instantiate(witteSmokeDeflectorsPrefab, __instance.transform);
				witteSmokeDeflector.transform.localPosition = new Vector3(0.0f, 2.1f, 5f);
				witteSmokeDeflector.transform.localRotation = Quaternion.identity;

				foreach (var aMeshRenderer in witteSmokeDeflector.GetComponentsInChildren<MeshRenderer>())
				{
					aMeshRenderer.material = s282Mat;
				}

				break;
			}
			case Settings.SmokeDeflectorType.Wagner:
			{
				GameObject wagnerSmokeDeflector = Object.Instantiate(wagnerSmokeDeflectorsPrefab, __instance.transform);
				wagnerSmokeDeflector.transform.localPosition = new Vector3(0.0f, 2.1f, 5f);
				wagnerSmokeDeflector.transform.localRotation = Quaternion.identity;

				foreach (var aMeshRenderer in wagnerSmokeDeflector.GetComponentsInChildren<MeshRenderer>())
				{
					aMeshRenderer.material = s282Mat;
				}

				break;
			}
			case Settings.SmokeDeflectorType.Chinese:
				ApplyChineseSmokeDeflector(ref __instance, s282Body);
				break;
		}

		// Boiler
		Log($"Applying {MySettings.boilerType.ToString()}");

		switch (MySettings.boilerType)
		{
			case Settings.BoilerType.Streamlined:
				GameObject streamlinedBoiler = Object.Instantiate(streamlinedBoilerPrefab, __instance.transform);
				streamlinedBoiler.transform.localPosition = new Vector3(0.0f, 2.15f, 5.1f);
				streamlinedBoiler.transform.localRotation = Quaternion.identity;

				foreach (var aMeshRenderer in streamlinedBoiler.GetComponentsInChildren<MeshRenderer>())
				{
					aMeshRenderer.material = s282Mat;
				}

				break;
		}
	}

	private static void ApplyChineseSmokeDeflector(ref TrainCar locomotive, Transform body)
	{
		//hide smoke box door
		var smokeboxDoorPath = "LocoS282A_Body/Static_LOD0/s282_locomotive_smokebox_door";
		Transform smokeBoxDoor = locomotive.transform.Find(smokeboxDoorPath);
		if (smokeBoxDoor == null)
		{
			Error($"Couldn't find S282 smoke box door on '{locomotive.transform.gameObject.name}' -> {smokeboxDoorPath}");
			return;
		}
		smokeBoxDoor.gameObject.SetActive(false);

		//show deflector and stuff
		GameObject chineseSmokeDeflector = Object.Instantiate(chineseSmokeDeflectorsPrefab, body);
		chineseSmokeDeflector.transform.localPosition = smokeBoxDoor.localPosition;

		//todo apply S282 material?
		// foreach (var aMeshRenderer in chineseSmokeDeflector.GetComponentsInChildren<MeshRenderer>())
		// {
		// 	if (aMeshRenderer.material.name == "S282") //todo
		// 	{
		// 		aMeshRenderer.material = s282Mat;
		// 	}
		// }
	}
}
