using Godot;
using System;

public partial class MainMenu : Control
{
	// Background elements
	private Sprite2D _childSprite;
	private RandomNumberGenerator _rng = new RandomNumberGenerator();
	private Vector2 _origin;
	private float _maxOffset;
	
	// Popup Window (Options)
	private Window _popup;
	
	// Audio Streams
	private AudioStreamPlayer _musicPlayer;
	private AudioStreamPlayer _hoverSoundPlayer;
	private AudioStreamPlayer _clickSoundPlayer;
	
	private string _savePath = "user://data.save";
	private static bool _hasLoaded = false;
	
	
	public override void _Ready()
	{
		_musicPlayer = GetNode<AudioStreamPlayer>("/root/BgMusic");
		_hoverSoundPlayer = GetNode<AudioStreamPlayer>("/root/ButtonHover");
		_clickSoundPlayer = GetNode<AudioStreamPlayer>("/root/ButtonClick");
		_popup = GetNode<Window>("OptionsWindow");
		_childSprite = GetNode<Sprite2D>("MenuWallpaper");
		
		_popup.Hide();
		if (!_hasLoaded) {
			load();
			_hasLoaded = true;  // mark it as run
		}

		_origin = _childSprite.Position;
		_maxOffset = Math.Min(Size.X, Size.Y) * 0.10f;

		translateBackground();
	}
	
	private async void translateBackground() {
		while (true)
		{
			float angle = _rng.RandfRange(0, Mathf.Tau);
			float radius = _rng.RandfRange(0, _maxOffset);
			Vector2 randomOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

			Vector2 targetPos = _origin + randomOffset;

			var tween = GetTree().CreateTween();
			tween.TweenProperty(_childSprite, "position", targetPos, 7f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);

			await ToSignal(tween, "finished");
		}
	}
	
	public void save()
	{
		var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Write);

		var data = new Godot.Collections.Dictionary
		{
			{"masterVol", OptionsWindow.masterVol},
			{"sfxVol", OptionsWindow.sfxVol},
			{"voiceVol", OptionsWindow.voiceVol},
			{"musicVol", OptionsWindow.musicVol}
		};

		file.StoreVar(data);
		GD.Print("Saved volumes:", data);
	}

	
	public void load() {
		if (!FileAccess.FileExists(_savePath)) {
			GD.Print("No save file found, using defaults.");
			OptionsWindow.masterVol = 1.0;
			OptionsWindow.sfxVol    = 1.0;
			OptionsWindow.voiceVol  = 1.0;
			OptionsWindow.musicVol  = 1.0;
			return;
		}

		using var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Read);
		try {
			var data = (Godot.Collections.Dictionary)file.GetVar();

			OptionsWindow.masterVol = data.ContainsKey("masterVol") ? (float)data["masterVol"] : 1.0f;
			OptionsWindow.sfxVol    = data.ContainsKey("sfxVol")    ? (float)data["sfxVol"]    : 1.0f;
			OptionsWindow.voiceVol  = data.ContainsKey("voiceVol")  ? (float)data["voiceVol"]  : 1.0f;
			OptionsWindow.musicVol  = data.ContainsKey("musicVol")  ? (float)data["musicVol"]  : 1.0f;

			GD.Print($"Loaded volumes: master={OptionsWindow.masterVol}, sfx={OptionsWindow.sfxVol}, voice={OptionsWindow.voiceVol}, music={OptionsWindow.musicVol}");
		}
		catch (Exception e) {
			GD.PrintErr("Failed to load save file, using defaults. Error: " + e.Message);
			OptionsWindow.masterVol = 1.0;
			OptionsWindow.sfxVol    = 1.0;
			OptionsWindow.voiceVol  = 1.0;
			OptionsWindow.musicVol  = 1.0;
		}

		(_popup as OptionsWindow)?.ApplyLoadedValues();
	}


	
	public void onPlayPressed() {
		_musicPlayer.Stop();
		_clickSoundPlayer.Play();
		GetTree().ChangeSceneToFile("res://Scenes/intro_cutscene.tscn");
	}
	
	public void onOptionsPressed() {
		_clickSoundPlayer.Play();
		_popup.PopupCentered();
	}
	
	public void onOptionsClose() {
		_clickSoundPlayer.Play();
		_popup.Hide();
	}
	
	public void onOptionsSaveAndClose() {
		save();
		_clickSoundPlayer.Play();
		_popup.Hide();
	}
	
	public void onExitPressed() {
		_clickSoundPlayer.Play();
		GetTree().Quit();
	}
	
	public void onCreditsPressed() {
		_clickSoundPlayer.Play();
		GetTree().ChangeSceneToFile("res://Scenes/credits.tscn");
	}
	
	public void onShopPressed() {
		_musicPlayer.Stop();
		_clickSoundPlayer.Play();
		GetTree().ChangeSceneToFile("res://Scenes/shop.tscn");
	}
	
	public void onFilterPressed() {
		_clickSoundPlayer.Play();	
	}

	public void onHover() {
		_hoverSoundPlayer.Play();
	}
	
}
