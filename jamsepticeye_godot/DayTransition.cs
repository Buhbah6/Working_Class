using Godot;
using System;

public partial class DayTransition : Control
{
	private Label label;
	public static int day = 1;
	public override void _Ready()
	{
		GetNode<GlobalManager>("/root/GlobalManager").CurrentDay ++;
		day = GetNode<GlobalManager>("/root/GlobalManager").CurrentDay;
		label = GetNode<Label>("Label");
		updateLabel();
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed) {
			if(day <= 3){
				GetTree().ChangeSceneToFile("res://Scenes/shopping.tscn");
			}
			else{
				GetTree().ChangeSceneToFile("res://Scenes/mid_cutscene.tscn");
			}
		}
	}
	
	public void updateLabel() {
		label.Text = "Day " + day;
		GD.Print("Day " + day);
	}
}
