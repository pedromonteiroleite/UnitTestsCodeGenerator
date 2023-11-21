using System;
using System.IO;
using UnitTestBuilder.Reflection;

public class FileGenerator
{
    public void GenerateAndSaveTestClassFile(Type targetType, string methodName)
    {
        // Create an instance of the TestClassGenerator
        TestClassGenerator generator = new TestClassGenerator();

        // Create a PathGenerator instance
        PathGenerator pathGenerator = new PathGenerator();

        // Generate the test class code
        string testClassCode = generator.GenerateTestClass(targetType, methodName);

        // Construct the file path
        // Generate the path for the method
        string filePath = pathGenerator.GeneratePathForMethod(targetType, methodName);

        // Get the directory part of filePath
        string directoryPath = Path.GetDirectoryName(filePath);

        // Ensure the directory exists or create it
        Directory.CreateDirectory(directoryPath);

        // Save the generated code to the file
        File.WriteAllText(filePath, testClassCode);

        Console.WriteLine($"Test class saved to {filePath}");
    }
}