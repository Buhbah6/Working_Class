using Godot;
using System;

public partial class MainMenu : Control
{
	private Sprite2D _childSprite;
	private RandomNumberGenerator _rng = new RandomNumberGenerator();
	private Vector2 _origin;
	private float _maxOffset;
	private Window _popup;
	
	public override void _Ready()
	{
		_popup = GetNode<Window>("OptionsWindow");
		_popup.Hide();
		_childSprite = GetNode<Sprite2D>("MenuWallpaper");

		_origin = _childSprite.Position;
		_maxOffset = Math.Min(Size.X, Size.Y) * 0.10f;

		translateBackground();
	}
	
	public void onPlayPressed() {
		GetTree().ChangeSceneToFile("res://Scenes/test.tscn");
	}
	public void onOptionsPressed() {
		_popup.PopupCentered();
	}
	public void onOptionsClosed() {
		_popup.Hide();
	}
	public void onSaveAndClosePressed() {
		_popup.Hide();
	}
	public void onExitPressed() {
		GetTree().Quit();
	}
	public void onCreditsPressed() {
		GetTree().ChangeSceneToFile("res://Scenes/credits.tscn");
	}
	public void onShopPressed() {
		
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
}
