# poc-vs Outline
A project sample that uses Selenium Webdriver, Extent Reports and Specflow. 
Project scope is to create a project skeleton that bind the three technologies into a coherent solution. 

Project also encompasses features like parallel feature run and console run, having in mind the capability to run the solution with other tools like Jenkins (CI - continuous integration) or similar tools. 

# Pre-requisites
In Visual Studio go to:
  Tools > Extensions and Updates > in the Online section search for: Specflow and install.
  
  
# Project Structure

|-Configs <br />
|--  BaseBinding.cs    - Acts as the point of webdriver injection into other step classes. It also contains the before/after actions that configure and quits the driver; and the logging of steps and report creation. <br />
|--  CWebDriver.cs     - Is an abstraction over WebDriver and ExtentReports. Driver initialization, Screenshot handling and report logging are described in this class. <br />
|-DataModels <br />
|--  HeatmapCellModel.cs - Is used during test data extraction. <br />
|-Sites <br />
|--  Prma               - Each site or application code should be self contained, having two sections - Pages and Steps <br />
|---  Pages             - Pages will define application sections, by identifying the elements and defining <br />
|----   HeatmapListPage.cs <br />
|----   HeatmapTooltipPage.cs <br />
|----   ... <br />
|---  Steps             - Steps contain the specflow binding and implementation for that test component. Steps rely on pages to interact with the page elements. Steps also inherit the webdriver from the BaseBinding.cs (this is the reason they have a constructor) <br />
|----   NavigationSteps.cs <br />
|----   PrmaLoginSteps.cs <br />
|----   ... <br />
|-Tools <br />
|-- Constants.cs        - project wide properties and constants <br />
|-- FormatUtils.cs      - date, name and string formatting <br />
