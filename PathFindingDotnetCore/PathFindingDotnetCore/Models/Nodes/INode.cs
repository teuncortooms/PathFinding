namespace PathFindingDotnetCore.Models
{
    public interface INode
    {
        public int Id { get; }
        public bool IsWall { get; set; }
        public bool IsFinish { get; set; }
        public bool IsStart { get; set; }
    }
}