using Godot;
using System;

public partial class DayTransition : Control
{
	private Label label;
	public static int day = 1;
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
		updateLabel();
	}
	
	public void updateLabel() {
		label.Text = "Day " + day;
		GD.Print("Day " + day);
	}
}
