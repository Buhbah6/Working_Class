using Godot;
using System;

public partial class DragableOjbect : TaskItem
{
	private bool _dragged = false;
	
	[Export]
	public Node2D CompletionArea;
	
	public override void _Ready()
	{
		BodyShapeEntered += OnBodyShapeEntered;
		BodyShapeExited += OnBodyShapeExited;
		
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
	
	public override void OnBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex){
		base.OnBodyShapeEntered(bodyRid, body, bodyShapeIndex, localShapeIndex);
		GD.Print("Collided");
		if(body == CompletionArea){
			Complete();
		}
	}
	
	public override void OnBodyShapeExited(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex){
		base.OnBodyShapeExited(bodyRid, body, bodyShapeIndex, localShapeIndex);
	}
	
	public void Complete(){
		base.Complete();
	}
}
