using Godot;
using System;

public partial class Patty : TaskItem, IPatty
{
	public void StartGrilling(){
		GD.Print("Started Grilling your mom");
		
	}
	
	public void StopGrilling(){
		GD.Print("Stopped grilling ;)");
	}
	
	private bool _dragged = false;
	
	[Export]
	public Node2D CompletionArea;
	
	public override void _Ready()
	{
		base._Ready();
	}
	
	public override void Interact(){
		_dragged = true;
		GD.Print("Dragging");
	}
	
	public override void StopInteract(){
		_dragged = false;
		GD.Print("Stopped Dragging");
	}
	
	public bool CanInteract()
	{
		return !base.CompletionState; 
	}
	
	public override void _Process(double delta)
	{
		base._Process(delta);
		if(_dragged){
			Position = GetViewport().GetMousePosition();
		}
	}
	
	public void Complete(){
		base.Complete();
	}
}
