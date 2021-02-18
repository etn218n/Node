using Node;

public class MockedNode : EmptyNode
{
    private int id;
    public  int ID => id;

    public MockedNode(int id) => this.id = id;
    public bool EqualByID(MockedNode other) => this.id == other.id;
    public static bool EqualByID(INode a, INode b) => (a as MockedNode).ID == (b as MockedNode).ID;
}