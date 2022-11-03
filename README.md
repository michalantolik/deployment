# Deploying ASP.NET Applications - Personal Learning Notes

## Resources
- https://www.linkedin.com/learning/deploying-asp-dot-net-applications/

## Demo Apps
- [Demo app from ASP.NET MVC: Building for Productivity and Maintainability - *Jess Chadwick*](https://www.linkedin.com/learning/asp-dot-net-mvc-building-for-productivity-and-maintainability/improve-the-design-of-your-asp-dot-net-mvc-applications?autoplay=true)
- [Demo app from Deploying ASP.NET Applications - *Tiberiu Covaci*](https://www.linkedin.com/learning/deploying-asp-dot-net-applications/)
  - In case of <code>Server Error in '/' Application</code> like this one below
    - <code>Could not find a part of the path</code> ...
    - ... <code>'C:\Users\mant\Downloads\Ex_Files_Deploying_ASP_NET_Apps\Exercise* Files\DemoApp\DemoApp\Website\bin\roslyn\csc.exe'.*</code>
  - Run this in Package Manager Console: <code>Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r</code>
    - Details: https://stackoverflow.com/questions/32780315/could-not-find-a-part-of-the-path-bin-roslyn-csc-exe

## Deployment strategies
- IIS
- Azure
- Docker

## Environments:
- **Development** - *e.g. Windows 10 + Vistual Studio + Docker for Windows CE*
- **Testing** - *e.g. Windows Server 2012 or newer, IIS 7.5 or newer, .NET Framework 4.5 or newer*
- **Deployment**
