#tool "nuget:?package=Cake.CoreCLR&version=0.36.0"

string SolutionName = "MarsRover";

var TestProjectPatterns = new string[]{
  "./**/*Test.csproj",
  "./**/*Tests.csproj",
};

var BuildConfig = "Release";

string[] DirectoriesToBeRemoved  = new string[]{
  $"./**/{SolutionName}*/**/bin/**",
  $"./**/{SolutionName}*/**/obj/**",
  $"./**/{SolutionName}*/**/build/**",
};

string CheckEnvVariableStage = "Check Env Variable";
string RemoveDirectoriesStage = "Remove Directories";
string DotNetCleanStage = "DotNet Clean";
string UnitTestStage = "Unit Test";
string FinalStage = "Final";

Task(CheckEnvVariableStage)
.Does(()=>
{
});

Task(RemoveDirectoriesStage)
.DoesForEach(DirectoriesToBeRemoved  , (directoryPath)=>
{
  var directories = GetDirectories(directoryPath);
    
  foreach (var directory in directories)
  {
    if(!DirectoryExists(directory)) continue;
    
    Console.WriteLine("Directory is cleaning : " + directory.ToString());     

    var settings = new DeleteDirectorySettings
    {
      Force = true,
      Recursive  = true
    };
    DeleteDirectory(directory, settings);
  }
});

Task(DotNetCleanStage)
.IsDependentOn(CheckEnvVariableStage)
.IsDependentOn(RemoveDirectoriesStage)
.Does(()=>
{
  DotNetCoreClean($"{SolutionName}.sln");
});

Task(UnitTestStage)
.IsDependentOn(DotNetCleanStage)
.DoesForEach(TestProjectPatterns, (testProjectPattern)=>
{
  FilePathCollection testProjects = GetFiles(testProjectPattern);
  foreach (var testProject in testProjects)
  {
    Console.WriteLine($"Tests are running : {testProject.ToString()}" );
    var testSettings = new DotNetCoreTestSettings{Configuration = BuildConfig};
    DotNetCoreTest(testProject.FullPath, testSettings);
  }
});

Task(FinalStage)
.IsDependentOn(UnitTestStage)
.Does(() =>
{
  Console.WriteLine("Operation is completed succesfully");
});

var target = Argument("target", FinalStage);
RunTarget(target);