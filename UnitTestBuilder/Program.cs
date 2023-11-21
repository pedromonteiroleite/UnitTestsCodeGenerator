using System;
using System.Reflection;
using UnitTestBuilder.Services;
using Microsoft.CodeAnalysis.MSBuild;

internal class Program
{

    static async Task Main(string[] args)
    {
        using (var workspace = MSBuildWorkspace.Create())
        {
            var project = await workspace.OpenProjectAsync("MyProject.csproj");
            var compilation = await project.GetCompilationAsync();

            // Perform analysis...
        }
    }

    //static void Main(string[] args)
    //{
    //    Type targetType = typeof(PickLoadCorrectionFlowController);
    //    string methodName = "ChangeCarrierAsync";
    //    var target = targetType.GetMember(methodName);

    //    if (target[0].MemberType == MemberTypes.Method)
    //        Console.WriteLine(methodName);

    //    var parameterExtractorService = new ParameterExtractorService();
    //    parameterExtractorService.ExtractInputParameters(targetType.GetMethod(methodName));


    //    //FileGenerator fileGenerator = new FileGenerator();
    //    //fileGenerator.GenerateAndSaveTestClassFile(targetType, methodName);
    //}

    //static void Main(string[] args)
    //{
    //    Type targetType = typeof(PickLoadCorrectionFlowController);
    //    string methodName = "ConfirmLowerStockItemQuantityAsync";

    //    FileGenerator fileGenerator = new FileGenerator();
    //    fileGenerator.GenerateAndSaveTestClassFile(targetType, methodName);
    //}

} 