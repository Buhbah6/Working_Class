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
		
		// Load credits from file
		string creditsText = FileAccess.Open("res://credits.md", FileAccess.ModeFlags.Read).GetAsText();
		_creditsLabel.Text = creditsText;

		// Reset scroll to top
		_scrollContainer.ScrollVertical = 0;
	}

	public override void _Process(double delta)
	{
		if (_autoScroll)
		{
			var vScrollBar = _scrollContainer.GetVScrollBar();
			float newScroll = (float)vScrollBar.Value + (_scrollSpeed * (float)delta);
			vScrollBar.Value = Mathf.Min(newScroll, (float)vScrollBar.MaxValue);

			if (_scrollContainer.ScrollVertical >= (float)vScrollBar.MaxValue)
			{
				// Reset back to top
				_scrollContainer.ScrollVertical = 0;
			}
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsAction("UiScrollUp") || @event.IsAction("UiScrollDown"))
		{
			_autoScroll = false;
		}
	}
	
	public void onHover() {
		GetNode<AudioStreamPlayer>("/root/ButtonHover").Play();
	}
	
	public void onReturnPressed() {
		GetNode<AudioStreamPlayer>("/root/ButtonClick").Play();
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
	public void onAutoPressed() {
		_autoScroll = true;
	}
}
