using Godot;
using System;

public partial class OptionsWindow : Window
{
	 private HSlider masterSlider;
	private HSlider sfxSlider;
	private HSlider voiceSlider;
	private HSlider musicSlider;

	public override void _Ready()
	{
		// Get sliders (adjust paths if different)
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
	
	private void SetBusVolume(string busName, float value)
	{
		int index = AudioServer.GetBusIndex(busName);
		if (index == -1)
		{
			GD.PrintErr($"Bus '{busName}' not found!");
			return;
		}
		AudioServer.SetBusVolumeDb(index, LinearToDb(value));
	}

	
	private void OnMasterVolumeChanged(double value) => SetBusVolume("Master", (float)value);
	private void OnSfxVolumeChanged(double value) => SetBusVolume("SFX", (float)value);
	private void OnVoiceVolumeChanged(double value) => SetBusVolume("Voice", (float)value);
	private void OnMusicVolumeChanged(double value) => SetBusVolume("Music", (float)value);

	private float LinearToDb(float value)
	{
		if (value <= 0f)
			return -80f; // effectively mute
		return 20f * (float)Math.Log10(value);
	}
}
