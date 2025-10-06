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
	
	private CanvasLayer _cl1;
	private CanvasLayer _cl2;
	private CanvasLayer _cl3;
	private CanvasLayer _cl4;
	private CanvasLayer _cl5;
	private CanvasLayer _cl6;
	private CanvasLayer _cl7;
	
	private AudioStreamPlayer _asp1;
	private AudioStreamPlayer _asp2;
	private AudioStreamPlayer _asp4;
	private AudioStreamPlayer _asp5;
	private AudioStreamPlayer _asp6;
	private AudioStreamPlayer _asp7;
	
	private Camera2D _currentCam;
	private Button _backButton;
	private int _camTracker;
	private Tween _tween;
	
	public override void _Ready() {
		
		Input.MouseMode = Input.MouseModeEnum.Visible;
		_cam1 = GetNode<Camera2D>("Scene1/Cam1");
		_cam2 = GetNode<Camera2D>("Scene2/Cam2");
		_cam3 = GetNode<Camera2D>("Scene3/Cam3");
		_cam4 = GetNode<Camera2D>("Scene4/Cam4");
		_cam5 = GetNode<Camera2D>("Scene5/Cam5");
		_cam6 = GetNode<Camera2D>("Scene6/Cam6");
		_cam7 = GetNode<Camera2D>("Scene7/Cam7");
		
		_cl1 = GetNode<CanvasLayer>("Scene1/CL1");
		_cl2 = GetNode<CanvasLayer>("Scene2/CL2");
		_cl3 = GetNode<CanvasLayer>("Scene3/CL3");
		_cl4 = GetNode<CanvasLayer>("Scene4/CL4");
		_cl5 = GetNode<CanvasLayer>("Scene5/CL5");
		_cl6 = GetNode<CanvasLayer>("Scene6/CL6");
		_cl7 = GetNode<CanvasLayer>("Scene7/CL7");
		
		_asp1 = GetNode<AudioStreamPlayer>("Scene1/ASP1");
		_asp2 = GetNode<AudioStreamPlayer>("Scene2/ASP2");
		_asp4 = GetNode<AudioStreamPlayer>("Scene4/ASP4");
		_asp5 = GetNode<AudioStreamPlayer>("Scene5/ASP5");
		_asp6 = GetNode<AudioStreamPlayer>("Scene6/ASP6");
		_asp7 = GetNode<AudioStreamPlayer>("Scene7/ASP7");
		
		_cl1.Visible = true;
		_cl2.Visible = false;
		_cl3.Visible = false;
		_cl4.Visible = false;
		_cl5.Visible = false;
		_cl6.Visible = false;
		_cl7.Visible = false;
		
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
				startZoomLoop(0.90f, 1.15f);
				break;
			case 4:
				_cam4.MakeCurrent();
				_currentCam = _cam4;
				_backButton = GetNode<Button>("Scene4/CL4/Return");
				connectBackButton();
				_cl3.Visible = false;
				_cl4.Visible = true;
				_asp2.Stop();
				_asp4.Play();
				startZoomLoop(0.90f, 1.15f);
				break;
			case 5:
				_cam5.MakeCurrent();
				_currentCam = _cam5;
				_backButton = GetNode<Button>("Scene5/CL5/Return");
				connectBackButton();
				_cl4.Visible = false;
				_cl5.Visible = true;
				_asp4.Stop();
				_asp5.Play();
				startZoomLoop(1.15f, 0.90f);
				break;
			case 6:
				_cam6.MakeCurrent();
				_currentCam = _cam6;
				_backButton = GetNode<Button>("Scene6/CL6/Return");
				connectBackButton();
				_cl5.Visible = false;
				_cl6.Visible = true;
				_asp5.Stop();
				_asp6.Play();
				startZoomLoop(1.15f, 0.90f);
				break;
			case 7:
				_cam7.MakeCurrent();
				_currentCam = _cam7;
				_backButton = GetNode<Button>("Scene7/CL7/Return");
				connectBackButton();
				_cl6.Visible = false;
				_cl7.Visible = true;
				_asp6.Stop();
				_asp7.Play();
				startZoomLoop(1.15f, 0.90f);
				break;
			case 8:
				GetTree().ChangeSceneToFile("res://Scenes/day_transition.tscn");
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
				startZoomLoop(1.15f, 0.90f);
				break;
			case 3:
				_cam3.MakeCurrent();
				_currentCam = _cam3;
				_backButton = GetNode<Button>("Scene3/CL3/Return");
				connectBackButton();
				_cl3.Visible = true;
				_cl4.Visible = false;
				_asp2.Play();
				_asp4.Stop();
				startZoomLoop(0.90f, 1.15f);
				break;
			case 4:
				_cam4.MakeCurrent();
				_currentCam = _cam4;
				_backButton = GetNode<Button>("Scene4/CL4/Return");
				connectBackButton();
				_cl4.Visible = true;
				_cl5.Visible = false;
				_asp4.Play();
				_asp5.Stop();
				startZoomLoop(0.90f, 1.15f);
				break;
			case 5:
				_cam5.MakeCurrent();
				_currentCam = _cam5;
				_backButton = GetNode<Button>("Scene5/CL5/Return");
				connectBackButton();
				_cl5.Visible = true;
				_cl6.Visible = false;
				_asp5.Play();
				_asp6.Stop();
				startZoomLoop(1.15f, 0.90f);
				break;
			case 6:
				_cam6.MakeCurrent();
				_currentCam = _cam6;
				_backButton = GetNode<Button>("Scene6/CL6/Return");
				connectBackButton();
				_cl6.Visible = true;
				_cl7.Visible = false;
				_asp6.Play();
				_asp7.Stop();
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
