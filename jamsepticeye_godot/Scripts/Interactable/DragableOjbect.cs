using Godot;
using System;

public partial class DragableOjbect : TaskItem
{
	private bool _dragged = false;
	
	[Export]
	public Area2D CompletionArea;
	
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
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
	
	public void OnAreaEntered(Area2D body){
		if(body == CompletionArea){
			Complete();
		}
	}
	
	public void Complete(){
		StopInteract();
		base.Complete();
	}
}
