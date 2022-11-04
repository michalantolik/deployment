# Deploying ASP.NET Applications - Personal Learning Notes

## Resources
- [Deploying ASP.NET Applications - LinkedIn](https://www.linkedin.com/learning/deploying-asp-dot-net-applications/)
- [IIS Architecture](https://www.linkedin.com/learning/deploying-asp-dot-net-applications/iis-introduction)
- [IIS Installation](https://www.linkedin.com/learning/deploying-asp-dot-net-applications/iis-introduction)

## Demo Apps
- [Demo app from ASP.NET MVC: Building for Productivity and Maintainability - *Jess Chadwick*](https://www.linkedin.com/learning/asp-dot-net-mvc-building-for-productivity-and-maintainability/improve-the-design-of-your-asp-dot-net-mvc-applications?autoplay=true)
- [Demo app from Deploying ASP.NET Applications - *Tiberiu Covaci*](https://www.linkedin.com/learning/deploying-asp-dot-net-applications/)
  - In case of <code>Server Error in '/' Application</code> like this one below
    - <code>Could not find a part of the path</code> ...
    - ... <code>'C:\Users\mant\Downloads\Ex_Files_Deploying_ASP_NET_Apps\Exercise* Files\DemoApp\DemoApp\Website\bin\roslyn\csc.exe'.*</code>
  - Run this in Package Manager Console: <code>Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r</code>
    - Details: https://stackoverflow.com/questions/32780315/could-not-find-a-part-of-the-path-bin-roslyn-csc-exe

## IIS (Internet Information Server) is a web server for hosting web applications
- Including ASP.NET web apps

## Deployment strategies (each uses IIS behind the scenes)
- IIS
- Azure
- Docker

## Client OS vs Server OS
- Client OS: e.g. Windows 7 (or newer)
- Server OS: e.g. Windows Server 20112 (or newer)

## Development environment:
- **E.g. configuration:** Windows 7 + Vistual Studio + Docker for Windows CE

## **Deployment environment**
  - **E.g. configuration:** Windows Server 2012 or newer, IIS 7.5 or newer, .NET Framework 4.5 or newer
  - **Deployment options**
    - **Own data center (on-premises)**
      - a lot of manual work
    - **Azure VM**
      - still a lot of manual work
    - **Azure WebApp/AppService**
      - much less manual work
      - IIS is already preinstalled behind the scenes by Microsoft *(infrastracture abstracted away)*
      - just configure connection strings for DBs, staging areas etc.
      - avoid manual patching and updating
      - auto-scaling
    - **Azure ACI**
      - Azure Container Instances
    - **Azure AKS**
      - Azure Kubernetes Service

## IIS Installation
### Manual installation on the client OS
- Start --> "Turn Windows features on or off"
- Check "Internet Information Services" checkbox - *this will check only necessary sub-items*
- Check .e.g. "Internet Information Services" --> "Application Development Feautres" --> ASP.NET 4.8 - *if your app uses it*
- Recommendation: install all necessary features for your web app and nothing more - *to avoid security risks*
- Check installation: open web browser and navigate to `localhost` --> you should see IIS startup page (or any test page)
### Manual installation on the server OS
- Start --> "Server Manager" --> "Add roles and features"
- Choose "Installation Type" --> "Role-based or feautre-based installation" or "Remote Desktop Service installation"
- Choose "Server Selection" --> server to install IIS on
- Choose "Server Roles" --> e.g. "Web Server (IIS)"
- Choose "Feautres" --> e.g. ".NET Framework 4.5" - *if your app uses it*
- Choose "Web Server Role (IIS) - Role Services" --> e.g. "Application Development - ASP.NET 4.5"
- Confirm --> Install --> Restart server
- Check installation: open web browser and navigate to `localhost` --> you should see IIS startup page (or any test page)
### Automate IIS install with PowerShell
- https://github.com/michalantolik/powershell/blob/main/scripts/Enable-IISFeatures.ps1
- https://github.com/michalantolik/powershell/blob/main/scripts/Disable-IISFeatures.ps1
- https://github.com/michalantolik/powershell/blob/main/scripts/Get-IISFeatures.ps1
### Automate IIS install with PowerShell DSC (Desired State Configuration)
- Copy this PS script to target machine: https://github.com/michalantolik/powershell/blob/main/scripts/Install-IIS-DSC.ps1
- Navigate to folder where you it was copied with PowerShell and run the following commands:
```ps
# Load scripts
. .\IIS-Install-DSC.ps1

# Execute DSC Configuration --> this will create "DeployWebApp" folder
DeployWebApp

# Start DSC Configuration
Start-DscConfiguration -Path .\DeployWebApp -Wait -Verbose
DeployWebApp
```
- Check installation: open web browser and navigate to `localhost` --> you should see IIS startup page (or any test page)
