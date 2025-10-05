using Godot;
using System;

public partial class Patty : Node2D, DragableObject, IPatty
{
	public override void StartGrilling(){
		GD.Print("Started Grilling your mom");
	}
	public override void StopGrilling(){
		GD.Print("Stopped grilling ;)");
	}
}
