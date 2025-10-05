using Godot;
using System.Collections.Generic;

public partial class Hand : CharacterBody2D
{
	public float MoveSpeed = 10f;
	public float YPosition = 750f;
	private List<RigidBody2D> _stack = new();

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		var mat = new PhysicsMaterial {
			Friction = 1000.0f,
			Bounce = 0.0f,
			Rough = true
		};
		GetNode<CollisionShape2D>("CollisionShape2D").Set("material", mat);
	}

	public override void _PhysicsProcess(double delta)
	{
		// Get clamped mouse X, keep constant Y
		Vector2 mousePos = GetGlobalMousePosition();
		float clampedX = Mathf.Clamp(mousePos.X, 50, 1870);
		Vector2 targetPos = new(clampedX, YPosition);

		// ✅ Don't multiply by delta here – MoveAndSlide already handles delta internally
		Vector2 direction = targetPos - GlobalPosition;
		Velocity = Velocity.Lerp((targetPos - GlobalPosition) * MoveSpeed, 0.2f);
		Velocity = new Vector2(Velocity.X, 0);
		MoveAndSlide();
		GlobalPosition = new Vector2(GlobalPosition.X, YPosition);

	}
}
