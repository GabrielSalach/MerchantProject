public abstract class Command
{
    public abstract void Execute();
    public delegate void OnDone();
    protected OnDone onDone;
    public abstract void SetDoneCallback(OnDone onDoneDelegate);
    public bool Done {get; set;}
}
