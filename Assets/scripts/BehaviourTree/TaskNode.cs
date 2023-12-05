using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TaskState { Running, Success, Failure }
public abstract class TaskBT
{
    public abstract TaskState Execute();
}

public class TaskNode : Node
{
    protected List<TaskBT> Tasks { get; private set; } = new();
    private int CurrentTaskIndex { get; set; }

    public TaskNode(string tag, IEnumerable<TaskBT> tasks)
        : base(tag)
    {
        foreach (TaskBT task in tasks)
        {
            AddTask(task);
        }
    }

    public void AddTask(TaskBT task) => Tasks.Add(task);

    protected override NodeState InnerEvaluate()
    {
        bool executeNextTask = true;
        int taskCount = Tasks.Count;

        while (executeNextTask)
        {
            TaskBT currentTask = Tasks[CurrentTaskIndex];
            TaskState currentTaskState = currentTask.Execute();

            switch (currentTaskState)
            {
                case TaskState.Failure:
                    State = NodeState.Failure;
                    return State;
                case TaskState.Running:
                    executeNextTask = false;
                    break;
                case TaskState.Success:
                    if (CurrentTaskIndex == taskCount - 1)
                    {
                        CurrentTaskIndex = 0;
                        State = NodeState.Success;
                        return State;
                    }

                    ++CurrentTaskIndex;
                    break;
            }
        }

        State = NodeState.Running;
        return State;
    }
}

public class Wait : TaskBT
{
    private float ElapsedTime { get; set; } = 0;
    private float SecondsToWait { get; set; } = 0;

    public Wait(float secondsToWait) => SecondsToWait = secondsToWait;

    public override TaskState Execute()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime > SecondsToWait)
        {
            ElapsedTime = 0;
            return TaskState.Success;
        }
        //run fatigue
        return TaskState.Running;
    }
}

public class Patrol : TaskBT
{
    private Vector3[] Destinations { get; set; }
    private NavMeshAgent Agent { get; set; }
    private int CurrentDestinationID { get; set; }
    private float PatrolTime { get; set; } = 5;
    private float ElapsedTime { get; set; } = 0;
    private Transform Player { get; set; }

    public Patrol(Vector3[] destinations, NavMeshAgent agent, Transform player)
    {
        Destinations = destinations;
        Agent = agent;
        Player = player;
        PatrolTime = 5;
    }

    public override TaskState Execute()
    {
        ElapsedTime += Time.deltaTime;

        Vector3 currentDestination = Destinations[CurrentDestinationID];
        Agent.destination = currentDestination;

        if (Vector3.Distance(currentDestination, Agent.transform.position) < 3)
        {
            CurrentDestinationID = (CurrentDestinationID + 1) % Destinations.Length;
            Agent.isStopped = true;
            return TaskState.Success;
        }
        else if (ElapsedTime > PatrolTime)
        {
            Debug.Log("fatigué");
            Agent.isStopped = true;
            ElapsedTime = 0;
            return TaskState.Success;
        }
        else if (Vector3.Distance(Player.position, Agent.transform.position) < 10)
        {
            return TaskState.Success;
        }
        else
        {
            Agent.isStopped = false;
            return TaskState.Running;
        }

    }
}
public class ChaseBuilding : TaskBT
{
    private Transform Building { get; set; }
    private NavMeshAgent Agent { get; set; }
    private float ChaseTime { get; set; } = 5;
    private float ElapsedTime { get; set; } = 0;
    public ChaseBuilding(Transform building, NavMeshAgent agent)
    {
        Building = building;
        Agent = agent;
    }
    public override TaskState Execute()
    {
        Agent.destination = Building.position;
        if (Vector3.Distance(Building.position, Agent.transform.position) > 10)
        {
            return TaskState.Success;
        }
        else if (ElapsedTime > ChaseTime)
        {
            Debug.Log("fatigué");
            Agent.isStopped = true;
            ElapsedTime = 0;
            return TaskState.Success;
        }
        else
        {
            Agent.isStopped = false;
            return TaskState.Running;
        }

    }
}