using Godot;
using System;
using System.Collections.Generic;

public partial class Quest : Node
{
	[Signal]
	public delegate void CompletedQuestEventHandler(Quest quest);
	
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
	}
	
	private void OnTaskCompleted(){
		GD.Print("Task Completed");
		CompletedTasks++;
		if(CompletedTasks >= TaskCount){
			EmitSignal(SignalName.CompletedQuest, this);
		}
	}	
}
