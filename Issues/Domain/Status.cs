namespace Issues.Domain
{
  internal class Status
  {
    public static Status OPEN = new OpenStatus();

    internal class OpenStatus : Status
    {
    }
  }
}