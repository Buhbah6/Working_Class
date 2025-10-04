using Godot;
using System;

public partial class IntroCutscene : Control
{
	private Camera2D _cam1;
	private Camera2D _cam2;
	private Camera2D _cam3;
	private Camera2D _cam4;
	private Camera2D _cam5;
	private Camera2D _cam6;
	private Camera2D _cam7;
	private Camera2D _currentCam;
	private int _camTracker;
	private Tween _tween;
	
	public override void _Ready() {
		_cam1 = GetNode<Camera2D>("Scene1/Cam1");
		_cam2 = GetNode<Camera2D>("Scene2/Cam2");
		_cam3 = GetNode<Camera2D>("Scene3/Cam3");
		_cam4 = GetNode<Camera2D>("Scene4/Cam4");
		_cam5 = GetNode<Camera2D>("Scene5/Cam5");
		_cam6 = GetNode<Camera2D>("Scene6/Cam6");
		_cam7 = GetNode<Camera2D>("Scene7/Cam7");
		_currentCam = _cam1;
		_camTracker = 1;
		startZoomLoop(1.15f, 0.90f);
	}
	
	private void changeCam() {
		_camTracker++;
		switch (_camTracker) {
			case 1:
				_cam1.MakeCurrent();
				_currentCam = _cam1;
				break;
			case 2:
				_cam2.MakeCurrent();
				_currentCam = _cam2;
				startZoomLoop(1.15f, 0.90f);
				break;
			case 3:
				_cam3.MakeCurrent();
				_currentCam = _cam3;
				startZoomLoop(0.90f, 1.15f);
				break;
			case 4:
				_cam4.MakeCurrent();
				_currentCam = _cam4;
				startZoomLoop(0.90f, 1.15f);
				break;
			case 5:
				_cam5.MakeCurrent();
				_currentCam = _cam5;
				startZoomLoop(1.15f, 0.90f);
				break;
			case 6:
				_cam6.MakeCurrent();
				_currentCam = _cam6;
				startZoomLoop(1.15f, 0.90f);
				break;
			case 7:
				_cam7.MakeCurrent();
				_currentCam = _cam7;
				startZoomLoop(1.15f, 0.90f);
				break;
			case 8:
				GetTree().ChangeSceneToFile("res://Scenes/test.tscn");
				break;
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) {
			if (eventKey.Pressed && eventKey.Keycode == Key.Enter) {
				changeCam();
			}
		}
		else if (@event is InputEventMouseButton eventMouseButton) {
			if (eventMouseButton.Pressed) {
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
