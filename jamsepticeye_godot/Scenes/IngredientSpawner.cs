using Godot;
using System;
using System.Collections.Generic;

public partial class IngredientSpawner : Node2D
{
	public float SpawnInterval = 1.5f;
	private float SpawnXInit = 100f;
	private float SpawnXFin = 1820f;
	private Timer _timer;

	// Array of PackedScenes for all ingredient templates
	private PackedScene[] _ingredientScenes;
	private Queue<PackedScene> _spawnQueue = new();

	public override void _Ready()
	{
		// Load all ingredient scenes
		_ingredientScenes = new PackedScene[]
		{
			GD.Load<PackedScene>("res://Assets/Prefabs/IngredientsEvil/BottomBun.tscn"),
			GD.Load<PackedScene>("res://Assets/Prefabs/IngredientsEvil/TopBun.tscn"),
			GD.Load<PackedScene>("res://Assets/Prefabs/IngredientsEvil/Blood.tscn"),
			GD.Load<PackedScene>("res://Assets/Prefabs/IngredientsEvil/Ear.tscn"),
			GD.Load<PackedScene>("res://Assets/Prefabs/IngredientsEvil/Muscle.tscn"),
			GD.Load<PackedScene>("res://Assets/Prefabs/IngredientsEvil/RazorBlade.tscn")
		};

		// Fill queue with initial shuffled order
		RefillQueue();

		// Set up timer
		_timer = new Timer();
		AddChild(_timer);
		_timer.WaitTime = SpawnInterval;
		_timer.Timeout += SpawnIngredient;
		_timer.Start();
	}

	private void RefillQueue()
	{
		List<PackedScene> shuffled = new List<PackedScene>(_ingredientScenes);
		// Fisher–Yates shuffle
		Random rng = new Random();
		int n = shuffled.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			var value = shuffled[k];
			shuffled[k] = shuffled[n];
			shuffled[n] = value;
		}
		_spawnQueue = new Queue<PackedScene>(shuffled);
	}


	private void SpawnIngredient()
	{
		if (_ingredientScenes.Length == 0)
			return;

		// If queue is empty, refill with a new shuffled batch
		if (_spawnQueue.Count == 0)
			RefillQueue();

		// Get the next ingredient
		var scene = _spawnQueue.Dequeue();
		var ingredient = scene.Instantiate<Node2D>();

		// Set random X position (above screen)
		ingredient.GlobalPosition = new Vector2(
			(float)GD.RandRange(SpawnXInit, SpawnXFin),
			-50f
		);

		// Add ingredient to scene
		GetParent().AddChild(ingredient);
	}
}
