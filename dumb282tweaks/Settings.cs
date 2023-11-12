using System.ComponentModel;
using UnityModManagerNet;
using static dumb282tweaks.Settings;

namespace dumb282tweaks;

public static class Settings {
	public enum CabType {
		[Description("Default 282 Cab")]
		Default,
		[Description("German Cab")]
		German
	}
	public enum BoilerType {
		[Description("Default Boiler")]
		Default,
		[Description("Streamlined Boiler")]
		Streamlined,
	}
	public enum SmokeDeflectorType {
		[Description("No Smoke Deflectors")]
		None,
		[Description("Witte Smoke Deflectors")]
		Witte,
		[Description("Wagner Smoke Deflectors")]
		Wagner,
	}
}

public class dumb282tweaksSettings : UnityModManager.ModSettings {
	// public CabType cabType = CabType.Default;
	public SmokeDeflectorType smokeDeflectorType = SmokeDeflectorType.Wagner;
	public BoilerType boilerType = BoilerType.Default;

	public override void Save(UnityModManager.ModEntry modEntry) {
		Save(this, modEntry);
	}
}

