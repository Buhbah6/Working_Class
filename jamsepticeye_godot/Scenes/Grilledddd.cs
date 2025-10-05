using Godot;
using System;

public partial class Grilledddd : Area2D
{
	
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		AreaExited += OnAreaExited;
	}
	
	
	public void OnAreaEntered(Area2D body){
		if (body is Patty) {
			Patty patty = body as Patty;
			patty.StartGrilling();
		}
	}
	
	public void OnAreaExited(Area2D body){
		if (body is Patty) {
			Patty patty = body as Patty;
			patty.StopGrilling();
		}
	}
}
