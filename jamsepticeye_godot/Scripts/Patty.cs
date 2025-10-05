using Godot;
using System;

public partial class Patty : TaskItem, IPatty
{
	private bool isGrilling = false;
	private bool isGrilled = false;
	private float timeToGrill;
	private float initialTime;
	
	[Export]
	public Texture2D finishedProduct;
	
	[Export]
	public ProgressBar itCooking;
	
	[Export]
	public AudioStreamPlayer audioObj;
	
	public void StartGrilling(){
		GD.Print("Started Grilling your mom");
		audioObj.Play();
		isGrilling = true;
		itCooking.Visible = true;
	}
	
	public void StopGrilling(){
		GD.Print("Stopped grilling ;)");
		audioObj.Stop();
		isGrilling = false;
		itCooking.Visible = false;
	}
	
	private bool _dragged = false;
	
	[Export]
	public Node2D CompletionArea;
	
	public override void _Ready()
	{
		itCooking.Visible = false;
		timeToGrill = (GD.Randi() % 21 + 5);
		initialTime = timeToGrill;
		AreaEntered += OnAreaEntered;
		itCooking.MaxValue = initialTime;
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
		if (isGrilling) {
			timeToGrill -= (float)delta;
			itCooking.Value = initialTime - timeToGrill;
		}
		if (timeToGrill <= 0) {
			CompleteGrill();
		}
		if(_dragged){
			Position = GetViewport().GetMousePosition();
		}
	}
	private void CompleteGrill() {
		isGrilling = false;
		isGrilled = true;
		TextureRect texture = GetNode<TextureRect>("%TextureRect");
		texture.Texture = finishedProduct;
	}
	public void OnAreaEntered(Area2D body){
		if(body == CompletionArea && isGrilled) {
			Complete();
		}
	}
	public void Complete(){
		StopInteract();
		base.Complete();
	}
}
