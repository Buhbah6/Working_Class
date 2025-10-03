using Godot;
using System;

public partial class TaskItem : Area2D, IInteratable
{
	public override void _Ready()
	{
		BodyShapeEntered += OnBodyShapeEntered;
		BodyShapeExited += OnBodyShapeExited;
		
	}
	
	public void Interact(){
		GD.Print("Interacted");
	}
	
	private void OnBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex){
		if(body is IPlayer){
			IPlayer player = body as IPlayer;
			player.SetInteractState(true);
		}
	}
	
	private void OnBodyShapeExited(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex){
		if(body is IPlayer){
			IPlayer player = body as IPlayer;
			player.SetInteractState(false);
		}
	}
	
	public bool CanInteract()
	{
		return true; 
	}
}
