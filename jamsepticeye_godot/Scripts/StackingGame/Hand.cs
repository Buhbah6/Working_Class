using Godot;
using System.Collections.Generic;

public partial class Hand : CharacterBody2D
{
	public float MoveSpeed = 10f;
	public float YPosition = 800f;
	private List<RigidBody2D> _stack = new();
	[Export] public TextureRect x1;
	[Export] public TextureRect x2;
	[Export] public TextureRect x3;
	[Export] public TextureRect x4;
	[Export] public TextureRect x5;
	[Export] public TextureRect x6;
	[Export] public Quest quest;
	[Export] public int BurgerNum;
	[Export] public Label task;
	[Export] public Label progress;
	[Export] public AudioStreamPlayer sfx;
	private Texture2D textureA;
	private Texture2D textureB;
	private bool x1a = true;
	private bool x2a = true;
	private bool x3a = true;
	private bool x4a = true;
	private bool x5a = true;
	private bool x6a = true;

	public override void _Ready()
	{
		
		PauseManager _pauseManager = GetNode<PauseManager>("/root/PauseManager");
		_pauseManager.UpdatePath("/root/FoodStackingGame/OptionsWindow");
		GetNode<AudioStreamPlayer>("/root/BgMusic").Stop();
		textureA = GD.Load<Texture2D>("res://Assets/StackingGame/BigX.png");
		textureB = GD.Load<Texture2D>("res://Assets/StackingGame/CheckMark.png");
		task.Text = "Task: Assemble " + BurgerNum + " Burgers";
		
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		var mat = new PhysicsMaterial {
			Friction = 1000.0f,
			Bounce = 0.0f,
			Rough = true
		};
		GetNode<CollisionShape2D>("CollisionShape2D").Set("material", mat);
		for (int i = 0; i < BurgerNum; i++) {
			quest.AddTask(new BurgerMaker());
		}
		progress.Text = "Burgers made: " + (BurgerNum - quest.Tasks.Count);
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
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsAction("Secondary")) {
			checkForWin();
		}
	}
	
	public void onAreaEntered(Node2D body)
	{
		if (body is RigidBody2D rb && !_stack.Contains(rb))
		{
			_stack.Add(rb);
			GD.Print($"Added to stack: {rb.Name}");
			ToggleTexture(rb);
		}
	}

	public void onAreaExited(Node2D body)
	{
		if (body is RigidBody2D rb && _stack.Contains(rb))
		{
			_stack.Remove(rb);
			GD.Print($"Removed from stack: {rb.Name}");
			ToggleTexture(rb);
		}
	}
	
	private void ToggleTexture(RigidBody2D rb)
	{
		switch (rb.Name) {
			case "TopBun":
				x1a = !x1a;
				x1.Texture = x1a ? textureA : textureB;
				break;
			case "Lettuce":
				x2a = !x2a;
				x2.Texture = x2a ? textureA : textureB;
				break;
			case "Tomato":
				x3a = !x3a;
				x3.Texture = x3a ? textureA : textureB;
				break;
			case "Cheese":
				x4a = !x4a;
				x4.Texture = x4a ? textureA : textureB;
				break;
			case "Patty":
				x5a = !x5a;
				x5.Texture = x5a ? textureA : textureB;
				break;
			case "BottomBun":
				x6a = !x6a;
				x6.Texture = x6a ? textureA : textureB;
				break;
		}
	}
	private void checkForWin() {
		RigidBody2D rb;
		if (!x1a && !x2a && !x3a && !x4a && !x5a && !x6a) {
			x1a = x2a = x3a = x4a = x5a = x6a = true;
			x1.Texture = x2.Texture = x3.Texture = x4.Texture = x5.Texture = x6.Texture = textureA;
			quest.Tasks[quest.CompletedTasks].Complete();
			sfx.Play();
			progress.Text = "Burgers made: " + (BurgerNum - quest.CompletedTasks);
			while (_stack.Count > 0) {
				rb = _stack[0];
				_stack.Remove(rb);
				rb.Free();
			}
		}	
	}
}
