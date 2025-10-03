using Godot;
using System;

public partial class MainMenu : Control
{
	private Sprite2D _childSprite;
	private RandomNumberGenerator _rng = new RandomNumberGenerator();
	private Vector2 _origin;
	private float _maxOffset;
	private Window _popup;
	private AudioStreamPlayer _musicPlayer;
	private AudioStreamPlayer _hoverSoundPlayer;
	private AudioStreamPlayer _clickSoundPlayer;
	
	
	public override void _Ready()
	{
		_musicPlayer = GetNode<AudioStreamPlayer>("/root/BgMusic");
		_hoverSoundPlayer = GetNode<AudioStreamPlayer>("/root/ButtonHover");
		_clickSoundPlayer = GetNode<AudioStreamPlayer>("/root/ButtonClick");
		_popup = GetNode<Window>("OptionsWindow");
		_childSprite = GetNode<Sprite2D>("MenuWallpaper");
		
		_popup.Hide();
		

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
	
	public void onPlayPressed() {
		_musicPlayer.Stop();
		_clickSoundPlayer.Play();
		GetTree().ChangeSceneToFile("res://Scenes/test.tscn");
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
