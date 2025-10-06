using Godot;
using System;

public partial class Shopping : Node2D
{
	private Control Store;
	private float _t = 0.0f;
	private Vector2 targetVector;
	private bool isPlaying = true;
	
	[Export]
	public Quest quest;
	
	[Export]
	public Area2D completionZone;
	
	public override void _Ready(){
		PauseManager _pauseManager = GetNode<PauseManager>("/root/PauseManager");
		_pauseManager.UpdatePath("/root/Shopping/OptionsWindow");
		Store = GetNode<Control>("%Control");
		targetVector = new Vector2(3f, 3f);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if(isPlaying){
			_t += (float)delta * 0.001f;
			Store.Scale = Store.Scale.Lerp(targetVector, _t);
			if(_t >= 4f){
				isPlaying = false;
			}
		}
	}
}
