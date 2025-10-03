using Godot;
using System;

public partial class TaskItem : Area2D, IInteractable, ITask
{
	[Signal]
	public delegate void CompletedTaskEventHandler();
	public bool CompletionState = false;
	
	public override void _Ready()
	{
		BodyShapeEntered += OnBodyShapeEntered;
		BodyShapeExited += OnBodyShapeExited;
		
	}
	
	public virtual void Interact(){
		GD.Print("Interacted");
		Complete();
	}
	
	public virtual void StopInteract(){
		GD.Print("Stop Interacted");
	}
	
	public virtual void OnBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex){
		if(body is IPlayer && CanInteract()){
			IPlayer player = body as IPlayer;
			player.SetInteractState(true);
			player.SetInteractable(this);
		}
	}
	
	public virtual void OnBodyShapeExited(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex){
		if(body is IPlayer){
			IPlayer player = body as IPlayer;
			player.SetInteractState(false);
			player.SetInteractable(null);
		}
	}
	
	public bool CanInteract()
	{
		return !CompletionState; 
	}
	
	public void Complete(){
		if(!CompletionState){
			CompletionState = true;
			EmitSignal(SignalName.CompletedTask);
		}
	}
}
