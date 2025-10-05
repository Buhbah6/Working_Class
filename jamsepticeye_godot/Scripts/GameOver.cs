using Godot;
using System;

public partial class GameOver : Control
{
	private Camera2D _cam1;

	private CanvasLayer _cl1;
	
	private AudioStreamPlayer _asp1;
	
	private Camera2D _currentCam;
	private int _camTracker;
	private Tween _tween;
	
	public override void _Ready() {
		GetNode<AudioStreamPlayer>("/root/BgMusic").Stop();
		_cam1 = GetNode<Camera2D>("Scene1/Cam1");
		
		_cl1 = GetNode<CanvasLayer>("Scene1/CL1");
		
		_asp1 = GetNode<AudioStreamPlayer>("Scene1/ASP1");
		
		_cl1.Visible = true;
		
		_currentCam = _cam1;
		_camTracker = 1;
		startZoomLoop(1.15f, 0.90f);
		_asp1.Play();
	}
	
	private void changeCam() {
		_camTracker++;
		switch (_camTracker) {
			case 1:
				_cam1.MakeCurrent();
				_currentCam = _cam1;
				_cl1.Visible = true;
				_asp1.Play();
				break;
			case 2:
				GetTree().ChangeSceneToFile("res://Scenes/credits.tscn");
				break;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed) {
			changeCam();
		}

		if (@event is InputEventKey eventKey) {
			if (eventKey.Pressed && eventKey.Keycode == Key.Enter) {
				changeCam();
			}
		}
	}

	
	private void startZoomLoop(float first, float second)
	{
		_tween = GetTree().CreateTween().BindNode(this);
		_tween.SetTrans(Tween.TransitionType.Sine);
		_tween.SetEase(Tween.EaseType.InOut);

		// Zoom in
		_tween.TweenProperty(_currentCam, "zoom", new Vector2(first, first), 20f);

		// Zoom out
		_tween.TweenProperty(_currentCam, "zoom", new Vector2(second, second), 25f);

		// When done, restart the loop
		_tween.Finished += () => startZoomLoop(first, second);
	}
}
