Import-Module WebAdministration

# Load IIS management assembly explicitly (RackSpace requires this)
Add-Type -Path "C:\Windows\System32\inetsrv\Microsoft.Web.Administration.dll"

$mgr = New-Object Microsoft.Web.Administration.ServerManager

$mgr.Sites |
    Sort-Object Name |
    ForEach-Object {
        $site = $_

        foreach ($app in $site.Applications) {

            # Each application has a VirtualDirectories collection
            # The one matching the application path is the physical path shown in IIS Manager
            $vdir = $app.VirtualDirectories[$app.Path]

            [PSCustomObject]@{
                Site            = $site.Name
                ApplicationPath = $app.Path
                Alias           = $app.Path.TrimStart('/')
                ApplicationPool = $app.ApplicationPoolName
                PhysicalPath    = $vdir.PhysicalPath
            }
        }
    }
