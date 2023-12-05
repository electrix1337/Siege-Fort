using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    private int currentChildID = 0;

    public Sequence(string tag) : base(tag) { }
    public Sequence(string tag, IEnumerable<Node> children)
        : base(tag, children) { }
    protected override NodeState InnerEvaluate()
    {
        var currentChild = Children[currentChildID];

        NodeState childState = currentChild.Evaluate();
        switch (childState)
        {
            case NodeState.Failure:
                State = NodeState.Failure;
                break;
            case NodeState.Running:
                State = NodeState.Running;
                break;
            case NodeState.Success:
                if (currentChildID == Children.Count - 1)
                    State = NodeState.Success;
                else
                    State = NodeState.Running;

                currentChildID = (currentChildID + 1) % Children.Count;
                break;
        }
        return State;
    }
}