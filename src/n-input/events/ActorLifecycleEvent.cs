namespace N.Package.Input.Events
{
  public enum ActorLifecycle
  {
    Created,
    Destroyed
  }

  public class ActorLifecycleEvent
  {
    public Actor Actor { get; set; }
    public ActorLifecycle Status { get; set; }
  }
}