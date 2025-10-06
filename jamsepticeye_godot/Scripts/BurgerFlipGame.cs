using Godot;
using System;

public partial class BurgerFlipGame : Node2D
{
	public override void _Ready(){
		GetNode<AudioStreamPlayer>("/root/BgMusic").Stop();
		PauseManager _pauseManager = GetNode<PauseManager>("/root/PauseManager");
		_pauseManager.UpdatePath("/root/BurgerFlipGame/OptionsWindow");
	}
}
