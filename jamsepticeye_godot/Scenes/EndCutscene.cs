using Godot;
using System;

public partial class EndCutscene : Control
{
	private Camera2D _cam1;
	private Camera2D _cam2;
	private Camera2D _cam3;
	
	private CanvasLayer _cl1;
	private CanvasLayer _cl2;
	private CanvasLayer _cl3;
	
	private AudioStreamPlayer _asp1;
	private AudioStreamPlayer _asp2;
	private AudioStreamPlayer _asp3;
	
	private Camera2D _currentCam;
	private Button _backButton;
	private int _camTracker;
	private Tween _tween;
	
	public override void _Ready() {
		GetNode<AudioStreamPlayer>("/root/BgMusic").Stop();
		_cam1 = GetNode<Camera2D>("Scene1/Cam1");
		_cam2 = GetNode<Camera2D>("Scene2/Cam2");
		_cam3 = GetNode<Camera2D>("Scene3/Cam3");
		
		_cl1 = GetNode<CanvasLayer>("Scene1/CL1");
		_cl2 = GetNode<CanvasLayer>("Scene2/CL2");
		_cl3 = GetNode<CanvasLayer>("Scene3/CL3");
		
		_asp1 = GetNode<AudioStreamPlayer>("Scene1/ASP1");
		_asp2 = GetNode<AudioStreamPlayer>("Scene2/ASP2");
		_asp3 = GetNode<AudioStreamPlayer>("Scene3/ASP3");
		
		_cl1.Visible = true;
		_cl2.Visible = false;
		_cl3.Visible = false;
		
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
				_cl2.Visible = false;
				_asp1.Play();
				break;
			case 2:
				_cam2.MakeCurrent();
				_currentCam = _cam2;
				_backButton = GetNode<Button>("Scene2/CL2/Return");
				connectBackButton();
				_cl1.Visible = false;
				_cl2.Visible = true;
				_asp1.Stop();
				_asp2.Play();
				startZoomLoop(1.15f, 0.90f);
				break;
			case 3:
				_cam3.MakeCurrent();
				_currentCam = _cam3;
				_backButton = GetNode<Button>("Scene3/CL3/Return");
				connectBackButton();
				_cl2.Visible = false;
				_cl3.Visible = true;
				_asp2.Stop();
				_asp3.Play();
				startZoomLoop(0.90f, 1.15f);
				break;
			case 4:
				GetTree().ChangeSceneToFile("res://Scenes/credits.tscn");
				break;
		}
	}
	
	private void changeCamBack() {
		_camTracker--;
		switch (_camTracker) {
			case 1:
				_cam1.MakeCurrent();
				_currentCam = _cam1;
				_cl1.Visible = true;
				_cl2.Visible = false;
				_asp1.Play();
				_asp2.Stop();
				break;
			case 2:
				_cam2.MakeCurrent();
				_currentCam = _cam2;
				_backButton = GetNode<Button>("Scene2/CL2/Return");
				connectBackButton();
				_cl2.Visible = true;
				_cl3.Visible = false;
				_asp2.Play();
				_asp3.Stop();
				startZoomLoop(1.15f, 0.90f);
				break;
		}
	}
	
	public void onBackPressed() {
		changeCamBack();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed) {
			if (_backButton == null || !_backButton.GetGlobalRect().HasPoint(mouseEvent.Position)) {
				changeCam();
			}
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
	
	private void connectBackButton() {
		if (_backButton == null)
			return;

		if (_backButton.IsConnected("pressed", new Callable(this, nameof(onBackPressed))))
			_backButton.Disconnect("pressed", new Callable(this, nameof(onBackPressed)));

		_backButton.Pressed += onBackPressed;
	}
}
