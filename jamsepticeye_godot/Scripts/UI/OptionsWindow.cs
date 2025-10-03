using Godot;
using System;

public partial class OptionsWindow : Window
{
	private HSlider masterSlider;
	private HSlider sfxSlider;
	private HSlider voiceSlider;
	private HSlider musicSlider;

	// Static so values persist across scenes
	public static double masterVol = -1;
	public static double sfxVol = -1;
	public static double voiceVol = -1;
	public static double musicVol = -1;

	public override void _Ready()
	{

		// Get sliders
		masterSlider = GetNode<HSlider>("Inputs/VBoxContainer/MasterVol");
		sfxSlider = GetNode<HSlider>("Inputs/VBoxContainer/SFXVol");
		voiceSlider = GetNode<HSlider>("Inputs/VBoxContainer/VoiceVol");
		musicSlider = GetNode<HSlider>("Inputs/VBoxContainer/MusicVol");

		// Connect signals
		masterSlider.ValueChanged += OnMasterVolumeChanged;
		sfxSlider.ValueChanged += OnSfxVolumeChanged;
		voiceSlider.ValueChanged += OnVoiceVolumeChanged;
		musicSlider.ValueChanged += OnMusicVolumeChanged;
	}
	
	public void ApplyLoadedValues()
	{
		if (masterVol != -1)
		{
			masterSlider.Value = masterVol;
			sfxSlider.Value = sfxVol;
			voiceSlider.Value = voiceVol;
			musicSlider.Value = musicVol;

			SetBusVolume("Master", (float)masterVol);
			SetBusVolume("SFX", (float)sfxVol);
			SetBusVolume("Voice", (float)voiceVol);
			SetBusVolume("Music", (float)musicVol);
		}
		else
		{
			masterSlider.Value = 1.0;
			sfxSlider.Value = 1.0;
			voiceSlider.Value = 1.0;
			musicSlider.Value = 1.0;

			SetBusVolume("Master", 1.0f);
			SetBusVolume("SFX", 1.0f);
			SetBusVolume("Voice", 1.0f);
			SetBusVolume("Music", 1.0f);
		}
	}


	private void SetBusVolume(string busName, float value)
	{
		int index = AudioServer.GetBusIndex(busName);
		if (index == -1)
		{
			GD.PrintErr($"Bus '{busName}' not found!");
			return;
		}

		AudioServer.SetBusVolumeDb(index, LinearToDb(value));
		//GD.Print($"Set {busName} bus to {value:0.00} ({LinearToDb(value)} dB)");
	}

	private void OnMasterVolumeChanged(double value)
	{
		masterVol = value;
		SetBusVolume("Master", (float)value);
	}

	private void OnSfxVolumeChanged(double value)
	{
		sfxVol = value;
		SetBusVolume("SFX", (float)value);
	}

	private void OnVoiceVolumeChanged(double value)
	{
		voiceVol = value;
		SetBusVolume("Voice", (float)value);
	}

	private void OnMusicVolumeChanged(double value)
	{
		musicVol = value;
		SetBusVolume("Music", (float)value);
	}

	private float LinearToDb(float value)
	{
		if (value <= 0f)
			return -80f; // effectively mute
		return 20f * (float)Math.Log10(value);
	}
}
