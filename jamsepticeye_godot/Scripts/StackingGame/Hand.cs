using Godot;
using System.Collections.Generic;

public partial class Hand : RigidBody2D
{
	public float MoveSpeed = 10f;
	public float YPosition = 850f;
	private List<RigidBody2D> _stack = new();

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		ContactMonitor = true;
		MaxContactsReported = 10;
		GravityScale = 0;
		LockRotation = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 targetPos;
		if (mousePos.X <= 50) 
			targetPos = new Vector2(50, YPosition);
		else if (mousePos.X >= 1870) 
			targetPos = new Vector2(1870, YPosition);
		else 
			targetPos = new Vector2(mousePos.X, YPosition);
		
		GlobalPosition = GlobalPosition.Lerp(targetPos, (float)(MoveSpeed * delta));

		if (Input.IsActionJustPressed("ui_accept"))
			DropStack();
	}

	private void DropStack()
	{
		foreach (var ingredient in _stack) {
			if (!IsInstanceValid(ingredient))
				continue;
			ingredient.Freeze = false;
			ingredient.Reparent(GetTree().CurrentScene);
		}
		_stack.Clear();
	}

	private void OnBodyEntered(Node body) {
		if (body is RigidBody2D ingredient && !_stack.Contains(ingredient)) {
			ingredient.Freeze = true;
			ingredient.Reparent(this);
			float stackHeight = _stack.Count * 20f;
			ingredient.Position = new Vector2(0, -stackHeight);
			_stack.Add(ingredient);
		}
	}
}
