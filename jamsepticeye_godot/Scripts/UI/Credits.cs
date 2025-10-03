using Godot;
using System;

public partial class Credits : Control
{
	private ScrollContainer _scrollContainer;
	private RichTextLabel _creditsLabel;
	private bool _autoScroll = true;
	private float _scrollSpeed = 30f; // pixels per second

	public override void _Ready()
	{
		
		_scrollContainer = GetNode<ScrollContainer>("ScrollContainer");
		_creditsLabel = _scrollContainer.GetNode<RichTextLabel>("RichTextLabel");
		
		_creditsLabel.Text = "TESTING CREDITS\nLine 1\nLine 2\nLine 3...";
		// Load credits from file
		string creditsText = FileAccess.Open("res://credits.md", FileAccess.ModeFlags.Read).GetAsText();
		_creditsLabel.Text = creditsText;
		GD.Print(creditsText);

		// Reset scroll to top
		_scrollContainer.ScrollVertical = 0;
	}

	public override void _Process(double delta)
	{
		if (_autoScroll)
		{
			// Move down
			_scrollContainer.ScrollVertical += (int)(_scrollSpeed * delta);

			// Max scroll limit
			double maxScroll = _scrollContainer.GetVScrollBar().MaxValue;

			if (_scrollContainer.ScrollVertical >= maxScroll)
			{
				// Reset back to top smoothly
				_scrollContainer.ScrollVertical = 0;
			}
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.WheelUp || mouseEvent.ButtonIndex == MouseButton.WheelDown)
			{
				_autoScroll = false; // let player take control
			}
		}
		else if (@event.IsAction("UiScrollUp") || @event.IsAction("UiScrollDown"))
		{
			_autoScroll = false;
		}
	}
	
	public void onReturnPressed() {
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
}
