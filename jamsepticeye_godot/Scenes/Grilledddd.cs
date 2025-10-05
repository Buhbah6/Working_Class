using Godot;
using System;

public partial class Grilledddd : Area2D
{
	
	public override void _Ready()
	{
		BodyShapeEntered += OnBodyShapeEntered;
		BodyShapeExited += OnBodyShapeExited;
	}
	
	public override void OnBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex){
		if (body is IPatty) {
			IPatty patty = body as IPatty;
			patty.StartGrilling();
		}
	}
	
	public override void OnBodyShapeExited(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex){
		if (body is IPatty) {
			IPatty patty = body as IPatty;
			patty.StopGrilling();
		}
	}
}
