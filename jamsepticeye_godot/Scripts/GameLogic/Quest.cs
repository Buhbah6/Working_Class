using Godot;
using System;
using System.Collections.Generic;

public partial class Quest : Node
{
	[Signal]
	public delegate void CompletedQuestEventHandler(Quest quest);
	
	private TextureRect questBanner;
	private Label questLabel;
	
	private Godot.Collections.Array<String> completionText = [
		"Wow! Good Job!",
		"Tasks Completed!",
		"Amazing!!!"
	];
	
	[Export]
	public Godot.Collections.Array<TaskItem> Tasks { get; set; }
	
	public int TaskCount { get; set; }
	public int CompletedTasks { get; set; }
	
	public override void _Ready()
	{
		foreach(TaskItem task in Tasks){
			task.CompletedTask += OnTaskCompleted;
			TaskCount ++;
		}
		CompletedTasks = 0;
	
		questBanner = GetNode<TextureRect>("%QuestBanner");
		questBanner.Visible = false;
		
		questLabel = GetNode<Label>("%Text");
		questLabel.Text = completionText.PickRandom();
	}
		
	private void OnTaskCompleted(){
		GD.Print("Task Completed");
		CompletedTasks++;
		if(CompletedTasks >= TaskCount){
			GD.Print("Quest Complete");
			questBanner.Visible = true;
			EmitSignal(SignalName.CompletedQuest, this);
		}
	}	
}
