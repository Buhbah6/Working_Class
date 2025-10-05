using Godot;
using System;

public partial class Kidnap : Node2D
{
	private int _distance = 5;
	private bool _move = false;
	private float _t = 0.0f;
	private Control Alley;
	private Vector2 targetVector;
	private Area2D person;
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("WalkForward"))
		{
			WalkForward();
		}
	}
	
	public override void _Ready(){
		GetNode<AudioStreamPlayer>("/root/BgMusic").Stop();
		Alley = GetNode<Control>("%Control");
		person = GetNode<Area2D>("%Person");
		Holdable objective = person as Holdable;
		objective.CanInteractHold = false;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if(_move){
			_t += (float)delta * 0.4f;
			Alley.Scale = Alley.Scale.Lerp(targetVector, _t);
			if(_t >= 0.4f){
				_move = false;
				_t = 0.0f;
			}
		}
	}
	
	private void WalkForward(){
		if(!_move){
			if(_distance >= 0){
				_distance --;
				targetVector = new Vector2(Alley.Scale.X + 0.5f, Alley.Scale.Y + 0.5f);
				_move = true;
			}
		}
		else if(_distance == 0 && person is Holdable){
			Holdable objective = person as Holdable;
			objective.CanInteractHold = true;
		}
	}
}
