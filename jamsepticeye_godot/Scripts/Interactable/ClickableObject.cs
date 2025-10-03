using Godot;
using System;

public partial class ClickableObject : TaskItem
{
	
	public override void Interact(){
		base.Interact();
	}
	
	public override void StopInteract(){
		base.StopInteract();
	}
	
	public bool CanInteract()
	{
		return !base.CompletionState; 
	}
	
	public void Complete(){
		base.Complete();
	}
}
