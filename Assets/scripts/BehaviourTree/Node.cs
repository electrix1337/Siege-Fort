using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState { Resting, Running, Success, Failure }
public abstract class Node
{
    public string Tag { get; private set; }
    public bool IsLoggingTag { get; private set; } = false;
    public Node Parent { get; private set; }
    protected List<Node> Children { get; private set; } = new();
    public NodeState State { get; protected set; } = NodeState.Resting;

    public Node(string tag) => Tag = tag;
    public Node(string tag, IEnumerable<Node> children)
        : this(tag)
    {
        foreach (var child in children)
            Attach(child);
    }

    private void Attach(Node child)
    {
        Children.Add(child);
        child.Parent = this;
    }

    public NodeState Evaluate()
    {
        if (IsLoggingTag)
            Debug.Log(Tag);
        return InnerEvaluate();
    }
    protected abstract NodeState InnerEvaluate();
}