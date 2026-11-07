namespace lab2.Models;

public class RouteInfo
{
    public String Pattern { get; set; }
    public String Controller { get; set; }
    public String Action { get; set; }
    public String? Area  { get; set; }
    public String Methods { get; set; }
}