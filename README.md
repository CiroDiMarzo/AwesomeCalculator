# AwesomeCalculator
### Installation guide

The solution has been developed under SharePoint 2013, using a new Azure virtual farm created with the “SharePoint 2013 non-HA Farm” template.

- Unzip the provided “Ciro_di_Marzo_Calculator.zip” file and copy its content to the SharePoint front and server. The zip file contains two folders:
  - Solution
  - Solution
- In the Scripts folder, open the Variables.ps1 file and update the following variables with the correct values for your environment:
  - $webApp: the web application that will contain the site collection to be created
  - $literalPath: the absolute physical path of the AwesomeCalculator.wsp solution file on the file system
  - $site: the address of the site collection to be created
  - $owner: the domain\username of the primary site collection administrator
- Run the PowerShell script
  - Deploy.ps1
- After the script has run successfully, open the browser and navigate to the new site collection; navigate to Site Settings/Manage Site Features and activate the web-scoped feature:
  - Awesome Calculator List Instances
  
  Navigate to the HomePage and you should see the web part Calculator that implements the given requirements.
  
  If you don’t see the web part, you can manually insert it into the page from the Awesome Calculator group.
