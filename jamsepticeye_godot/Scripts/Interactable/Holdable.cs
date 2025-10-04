using Godot;
using System;

public partial class Holdable : TaskItem
{
	private bool _holding = false;
	private ProgressBar progress;
	private float initialTime;
	private TextureRect texture;
	public bool CanInteractHold = true;
	
	[Export]
	public Texture2D mesh;
	
	[Export]
	public float TimeToHold;
	
	public override void _Ready(){
		progress =  GetNode<ProgressBar>("%DoneBar");
		initialTime = TimeToHold;
		texture = GetNode<TextureRect>("%ExtraObject");
		texture.Texture = mesh;
		base._Ready();
	}
	
	public override void Interact(){
		texture.Visible = true;
		_holding = true;
	}
	
	public override void StopInteract(){
		texture.Visible = false;
		_holding = false;
	}
	
	public bool CanInteract()
	{
		return !base.CompletionState || CanInteractHold; 
	}
	
	public override void _Process(double delta)
	{
		base._Process(delta);
		if(_holding){
			TimeToHold -= (float)delta;
			progress.Value = initialTime - TimeToHold;
		}
		
		if(!base.CompletionState && TimeToHold <= 0){
			Complete();
		}
		
		if(base.CompletionState){
			texture.Visible = true;
		}
	}
	
	public void Complete(){
		base.Complete();
	}
}
