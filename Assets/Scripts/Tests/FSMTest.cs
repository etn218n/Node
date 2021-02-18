using Node;
using NUnit.Framework;

public class FSMTest
{
    [Test]
    public void FSM_CanNotAddNode_BelongToSubFSM()
    {
        var fsm = new FSM();
        var sub = new SubFSM(fsm);
        
        var a = new MockedNode(0);

        sub.AddNode(a);
        fsm.AddNode(a);

        fsm.Start();

        Assert.IsTrue(!fsm.Contain(a));
    }
    
    [Test]
    public void FSM_SelectorNode_PickQualifiedNode_AsEntryNode_InStart()
    {
        var fsm = new FSM();
        var a = new MockedNode(0);

        fsm.AddTransitionFromSelectorNode(a, () => true);
        fsm.Start();

        Assert.IsTrue(MockedNode.EqualByID(a, fsm.CurrentNode));
    }
    
    [Test]
    public void FSM_SetEntryNode_InStart()
    {
        var fsm = new FSM();
        var a = new MockedNode(0);
        var b = new MockedNode(1);
        
        fsm.AddNode(a);
        fsm.AddNode(b);
        fsm.SetEntry(b);
        fsm.Start();
        
        Assert.IsTrue(MockedNode.EqualByID(b, fsm.CurrentNode));
    }
    
    [Test]
    public void FSM_SetFirstAddedNode_AsEntryNode()
    {
        var fsm = new FSM();
        var a = new MockedNode(0);

        fsm.AddNode(a);
        fsm.Start();

        Assert.IsTrue(MockedNode.EqualByID(a, fsm.CurrentNode));
    }
    
    [Test]
    public void FSM_TransitionBack_OnePreviousNode_InOneUpdate()
    {
        var fsm = new FSM();
        var a = new MockedNode(0);
        var b = new MockedNode(1);
        
        fsm.AddTransition(a, b, () => true);
        fsm.AddTransitionToPreviousNode(b, () => true);

        fsm.Start();
        fsm.Update();
        fsm.Update();
        
        Assert.IsTrue(MockedNode.EqualByID(a, fsm.CurrentNode));
    }
    
    [Test]
    public void FSM_TransitionBack_TwoPreviousNode_InTwoUpdate()
    {
        var fsm = new FSM();
        var a = new MockedNode(0);
        var b = new MockedNode(1);
        var c = new MockedNode(2);
        var d = new MockedNode(3);
        
        fsm.AddTransition(a, b, () => true);
        fsm.AddTransition(b, c, () => true);
        fsm.AddTransition(c, d, () => true);
        
        fsm.Start();  // node a
        fsm.Update(); // node b
        fsm.Update(); // node c
        fsm.Update(); // node d
        
        fsm.RemoveAllTransitionFrom(c);

        fsm.AddTransitionToPreviousNode(d, () => true);
        fsm.AddTransitionToPreviousNode(c, () => true);
        
        fsm.Update(); // back to c
        fsm.Update(); // back to b
        
        Assert.IsTrue(MockedNode.EqualByID(b, fsm.CurrentNode));
    }
    
    [Test]
    public void FSM_TransitionOneNode_InOneUpdate()
    {
        var fsm = new FSM();
        var a = new MockedNode(0);
        var b = new MockedNode(1);
        
        fsm.AddTransition(a, b, () => true);
        
        fsm.Start();
        fsm.Update();
        
        Assert.IsTrue(MockedNode.EqualByID(b, fsm.CurrentNode));
    }
    
    [Test]
    public void FSM_TransitionTwoNode_InTwoUpdate()
    {
        var fsm = new FSM();
        var a = new MockedNode(0);
        var b = new MockedNode(1);
        var c = new MockedNode(2);
        
        fsm.AddTransition(a, b, () => true);
        fsm.AddTransition(b, c, () => true);
        
        fsm.Start();
        fsm.Update();
        fsm.Update();
        
        Assert.IsTrue(MockedNode.EqualByID(c, fsm.CurrentNode));
    }
}
