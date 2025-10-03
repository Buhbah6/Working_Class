using Godot;
using System;

public partial class PlayerThing : CharacterBody2D, IPlayer
{
	private Sprite2D playerTexture;
	private Texture2D idleHand; 
	private Texture2D activeHand;
	private Texture2D interactHand;
	private Texture2D secondaryHand;
	
	private bool _bCanInteract = false;
	
	public override void _Process(double delta)
	{
		base._Process(delta);
		Position = GetViewport().GetMousePosition();
	}
	
	public override void _Ready(){
		idleHand = GD.Load<Texture2D>("res://Assets/Materials/Idle.png"); 
		activeHand = GD.Load<Texture2D>("res://Assets/Materials/Active.png");
		interactHand = GD.Load<Texture2D>("res://Assets/Materials/CanInteract.png");
		secondaryHand = GD.Load<Texture2D>("res://Assets/Materials/Secondary.png");
		playerTexture = GetNode<Sprite2D>("Sprite2D");
	}
	
	public override void _Input(InputEvent @event)
	{
	
		if (@event.IsActionPressed("Interact"))
		{
			playerTexture.Texture = activeHand;
		}
		if (@event.IsActionReleased("Interact"))
		{
			playerTexture.Texture = idleHand;
		}
		
		if (@event.IsActionPressed("Secondary"))
		{
			playerTexture.Texture = secondaryHand;
		}
		if (@event.IsActionReleased("Secondary"))
		{
			playerTexture.Texture = idleHand;
		}
		
		
	}
	
	public void SetInteractState(bool state){
		_bCanInteract = state;
		if(_bCanInteract){
			playerTexture.Texture = interactHand;
		}
		else{
			playerTexture.Texture = idleHand;
		}
	}
}
