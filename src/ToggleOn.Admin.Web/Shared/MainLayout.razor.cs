namespace ToggleOn.Admin.Web.Shared;

public partial class MainLayout
{
    private bool _drawerOpen = true;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
}
