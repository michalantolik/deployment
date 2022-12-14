# Deploying ASP.NET Applications - Personal Learning Notes

## Resources
- [Deploying ASP.NET Applications - LinkedIn](https://www.linkedin.com/learning/deploying-asp-dot-net-applications/)
- [IIS Architecture](https://www.linkedin.com/learning/deploying-asp-dot-net-applications/iis-introduction)
- [IIS Installation](https://www.linkedin.com/learning/deploying-asp-dot-net-applications/iis-introduction)
- [What Is an IIS Application Pool - GREAT ONE!](https://stackify.com/what-is-an-iis-application-pool/)
- [Publish WebAPI from Visual Studio to IIS using Web Deploy](https://www.linkedin.com/learning/advanced-asp-dot-net-web-api-2-2/iis-hosting?autoplay=true)

## Course Environment
### Machine Configuration
- **E.g. configuration:** Windows 7 + Vistual Studio + Docker for Windows CE
### Demo Apps
- [Demo app from ASP.NET MVC: Building for Productivity and Maintainability - *Jess Chadwick*](https://www.linkedin.com/learning/asp-dot-net-mvc-building-for-productivity-and-maintainability/improve-the-design-of-your-asp-dot-net-mvc-applications?autoplay=true)
- [Demo app from Deploying ASP.NET Applications - *Tiberiu Covaci*](https://www.linkedin.com/learning/deploying-asp-dot-net-applications/)
  - In case of <code>Server Error in '/' Application</code> like this one below
    - <code>Could not find a part of the path</code> ...
    - ... <code>'C:\Users\mant\Downloads\Ex_Files_Deploying_ASP_NET_Apps\Exercise* Files\DemoApp\DemoApp\Website\bin\roslyn\csc.exe'.*</code>
  - Run this in Package Manager Console: <code>Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r</code>
    - Details: https://stackoverflow.com/questions/32780315/could-not-find-a-part-of-the-path-bin-roslyn-csc-exe

## IIS (Internet Information Server) is a web server for hosting web applications
- Including ASP.NET web apps

## IIS - Terminology
### Application Pool
- Collection of websites running as a single process, with a single identity ...
- ... each application pool consists of a process called `w3wp.exe` that runs on the server machine
- https://stackify.com/what-is-an-iis-application-pool/

## Application Pool - Benefits
- **Splitting websites over application pools allows ...**
  - ... more rigid security between the websites
  - ... as well as preventing one website from crashing if another one does *(better reliability/availability)*
  - Application pools are used to separate sets of IIS worker processes ...
  - ... that share the same configuration and application boundaries
- **Combining multiple sites into a single application pool ...**
  - ... is to either share resources
  - ...or to leave a smaller server footprint on the server

## Deployment Strategies (each uses IIS behind the scenes)
### Operating Systems
- Client OS: e.g. Windows 7 (or newer)
- Server OS: e.g. Windows Server 2012 (or newer)
### Deployment Strategies
- **IIS --> on client OS**
  - a lot of manual work
  - need to install `IIS` and `Web Deploy`
- **IIS --> on server OS** (on-premises server or Azure VM)
  - still a lot of manual work
- **Azure WebApp/AppService**
  - much less manual work
  - IIS is already preinstalled behind the scenes by Microsoft *(infrastracture abstracted away)*
  - just configure connection strings for DBs, staging areas etc.
  - avoid manual patching and updating
- **Docker**
  - **Azure ACI**
    - Azure Container Instances
  - **Azure AKS**
    - Azure Kubernetes Service

## IIS Installation
### Manual installation on the client OS
- Start --> "Turn Windows features on or off"
- Check "Internet Information Services" checkbox - *this will check only necessary sub-items*
- Check .e.g. "Internet Information Services" --> "Application Development Feautres" --> ASP.NET 4.8 - *if your app uses it*
- Recommendation: install only necessary features for your web app and nothing more - *to avoid security risks*
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
# Load the script
. .\IIS-Install-DSC.ps1

# Compile given DSC Configuration --> this will create "DeployWebApp" folder
DeployWebApp

# Start DSC Configuration from folder where it was compiled
Start-DscConfiguration -Path .\DeployWebApp -Wait -Verbose
DeployWebApp
```
- Check installation: open web browser and navigate to `localhost` --> you should see IIS startup page (or any test page)

## Deployment using "Web Deploy"
### Web Deploy - what is it 
- Client-server application that works together with IIS ...
- ... and help us with the publishing of applications

### Web Deploy - deployment stratagies
- Manual
- Automatic

### Web Deploy - how to install - manually
- **Enable feature called "Management Service"** in "Server Manager"
  - Server Manager --> Server Roles --> Web Server (IIS) --> Management Tools --> Management Service --> ...
  - ... Next --> Install
  - ... to allow IIS web server to be managed remotely from another computer using IIS Manager
- **Install Web Deploy application**
  - Go to `iis.net` webpage --> Downloads --> Deploy & Migrate --> Web Deploy 3.6 --> Install this extension
  
### Web Deploy - how to install - using PowerShell DSC
- Copy this PS script to target machine: https://github.com/michalantolik/powershell/blob/main/scripts/WebDeploy-DSC.ps1
- Navigate to folder where you it was copied with PowerShell and run the following commands:
```ps
# Load the script
. .\WebDeploy-DSC.ps1

# Compile given DSC Configuration --> this will create "WebDeployOnly" folder
WebDeployOnly

# Start DSC Configuration from folder where it was compiled
Start-DscConfiguration -Path .\WebDeployOnly -Wait -Verbose
```

## IIS Deployment - Strategies
- ### Publish to Folder deployment
  - Publish to local folder from VS --> e.g. to `C:\Deploy\FolderDeployment`
  - Edit `web.config` file in `C:\Deploy\FolderDeployment` - *e.g. replace local DB connection strings with production DB*
  - Copy content of `C:\Deploy\FolderDeployment` folder to `C:\inetpub\wwwroot`
  - Browse to `localhost` to test the website
- ### Publish to Web Server (IIS) - Web Deploy Package
  - **Package the application and deploy manually (copy)**
    - Publish to Web Server (IIS) from VS
    - Web Deploy Package
    - Enter Package location
    - Enter Site name - *"Default Web Site" by default in IIS*
    - Enter production DB connection strings
    - Publish
    - Go to Package location and make sure that (zip + cmd + readme) + other files are there
  - **Deploy the pacakged application to IIS manually**
    - Open IIS Manager --> Deploy --> Import Application *(google for it if missing)* --> Select published ZIP file
  - **Deploy the packaged application to IIS using script (cmd)**
    - Open commandline in admin mode
    - Go to Package location
    - Run `Website.deploy.cmd /T` - to test - "what if"
    - Run `Website.deploy.cmd /Y` - to deploy - to local IIS
    - Run `Website.deploy.cmd /Y /M:remote_machine_name /U:username_and_password` - to deploy - to remote IIS
- ### Publish to Web Server (IIS) - Web Deploy
  - Publish to Web Server (IIS) from VS
  - Web Deploy
  - Enter Server: "localhost" or some remote server
  - Enter Site name - *"Default Web Site" by default in IIS*
  - Enter Destination URL: "http://localhost
  - Enter DB connection strings
  - Save & Publish
  - Destination URL should be open automatically to make sure that the process went fine


## Azure Deployment - Strategies
- ### Publish to Azure App Service
  - [Quickstart: Deploy an ASP.NET web app](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net60&pivots=development-environment-vs)
    - Kudu ZIP Push Deployment
      - Manual, Azure Portal Kudu GUI
