using Node;
using NUnit.Framework;
using UnityEngine;

public class SubFSMTest
{
        
    [Test]
    public void FSM_TransitionTwoNode_InTwoUpdate()
    {
        var fsm = new FSM();
        var sub = new SubFSM(fsm);
        
        var a = new MockedNode(0);
        var b = new MockedNode(1);
        var c = new MockedNode(2);

        sub.AddTransition(a, b, () => true);
        sub.AddTransition(b, c, () => true);

        fsm.Start();  // a
        fsm.Update(); // b
        fsm.Update(); // c

        Assert.IsTrue(MockedNode.EqualByID(c, sub.CurrentNode));
    }
    
    [Test]
    public void SubFSM_CanNotAddNode_BelongToOwnerFSM()
    {
        var fsm = new FSM();
        var sub = new SubFSM(fsm);
        
        var a = new MockedNode(0);
        
        fsm.AddNode(a);
        sub.AddNode(a);
        
        fsm.Start();

        Assert.IsTrue(!sub.Contain(a));
    }

    [Test]
    public void SubFSM_CanInteruptAndTransitionToFSM_EvenNotFinished()
    {
        var fsm = new FSM();
        var sub = new SubFSM(fsm);

        var a = new MockedNode(0);
        var b = new MockedNode(1);
        var c = new MockedNode(2);
        
        fsm.AddTransition(sub, a, () => true);
        
        sub.AddTransition(b, c, () => true);
        
        fsm.Start();
        fsm.Update();

        Assert.IsTrue(MockedNode.EqualByID(a, fsm.CurrentNode));
    }
    
    [Test]
    public void SubFSM_CanTransitionBackToFSM_WhenFinished()
    {
        var fsm = new FSM();
        var sub = new SubFSM(fsm);

        var a = new MockedNode(0);
        var b = new MockedNode(1);
        
        fsm.AddTransition(sub, a, () => sub.IsFinished);
        
        sub.AddTransitionToExitNode(b, () => true);
        
        fsm.Start();
        fsm.Update();
        fsm.Update();

        Assert.IsTrue(MockedNode.EqualByID(a, fsm.CurrentNode));
    }
    
    [Test]
    public void SubFSM_ExitNode_MarkSubFSM_AsFinished()
    {
        var fsm = new FSM();
        var sub = new SubFSM(fsm);

        var a = new MockedNode(0);
        
        sub.AddTransitionToExitNode(a, () => true);
        
        fsm.Start();
        fsm.Update();

        Assert.IsTrue(sub.IsFinished);
    }

    [Test]
    public void SubFSM_SelectorNode_PickQualifiedNode_AsEntryNode_InStart()
    {
        var fsm = new FSM();
        var sub = new SubFSM(fsm);
        
        var a = new MockedNode(0);

        sub.AddTransitionFromSelectorNode(a, () => true);
        
        fsm.Start();

        Assert.IsTrue(MockedNode.EqualByID(a, sub.CurrentNode));
    }

    [Test]
    public void SubFSM_SetEntryNode_InStart()
    {
        var fsm = new FSM();
        var sub = new SubFSM(fsm);
        
        var a = new MockedNode(0);
        var b = new MockedNode(1);
        
        sub.AddNode(a);
        sub.AddNode(b);
        sub.SetEntry(b);
        fsm.Start();
        
        Assert.IsTrue(MockedNode.EqualByID(b, sub.CurrentNode));
    }
}
