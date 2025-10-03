using Godot;
using System;

public partial class Ui : CanvasLayer
{
	private Label label;
	
	public override void _Ready()
	{
		label = GetNode<Label>("%Label");
	}
}
