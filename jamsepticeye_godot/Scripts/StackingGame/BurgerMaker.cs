using Godot;
using System;

public partial class BurgerMaker : TaskItem
{
	public void Complete(){
		base.CompletionState = false;
		base.Complete();
	}
}
