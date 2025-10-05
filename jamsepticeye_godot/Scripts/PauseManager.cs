using Godot;
using System;

public partial class PauseManager : Node
{
	public NodePath OptionsWindowPath;
	private Window optionsWindow;

	public override void _Ready()
	{
		this.ProcessMode = Node.ProcessModeEnum.Always; // must always process
		//if (!string.IsNullOrEmpty(OptionsWindowPath.ToString()))
			//optionsWindow = GetNode<Window>(OptionsWindowPath);
	}
	
	public void UpdatePath(NodePath path) {
		if (!string.IsNullOrEmpty(path.ToString()))
			optionsWindow = GetNode<Window>(path);
		if (optionsWindow != null)
			optionsWindow.ProcessMode = Node.ProcessModeEnum.Always;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Pause"))
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{
		bool paused = !GetTree().Paused;
		GetTree().Paused = paused;

		if (paused)
		{
			optionsWindow?.PopupCentered();
			optionsWindow?.GrabFocus();
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else
		{
			optionsWindow?.Hide();
			//Input.MouseMode = Input.MouseModeEnum.Captured;
		}
	}
}
