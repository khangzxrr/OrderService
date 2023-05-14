namespace OrderService.Web.Utils;

public class Utils
{
  public static int getPageCount(int totalCount, int pageSize)
  {
    if (pageSize == 0)
    {
      return totalCount > 0 ? 1 : 0;
    }

    return (int)Math.Ceiling((decimal)totalCount / pageSize);
  }
}
