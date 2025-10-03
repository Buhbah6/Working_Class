using Godot;
using System;

public partial class Shop : Control
{
	private AudioStreamPlayer _shopMusic;
	
	public override void _Ready() {
		_shopMusic = GetNode<AudioStreamPlayer>("MusicPlayer");
	}
	
	public void onHover() {
		GetNode<AudioStreamPlayer>("/root/ButtonHover").Play();
	}
	
	public void onReturnPressed() {
		_shopMusic.Stop();
		GetNode<AudioStreamPlayer>("/root/ButtonClick").Play();
		GetNode<AudioStreamPlayer>("/root/BgMusic").Play();
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
}
