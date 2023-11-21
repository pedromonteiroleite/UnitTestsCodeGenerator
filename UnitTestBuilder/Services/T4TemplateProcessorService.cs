namespace UnitTestBuilder.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using UnitTestBuilder.Domain.Entities;
    using UnitTestBuilder.Interfaces.Services;

    public class T4TemplateProcessor: IT4TemplateProcessor
    {
        public string ApplyMethodParametersToTemplateSection(
            string templateFilePath,
            string sectionName,
            List<MethodParameter> methodParameters)
        {
            try
            {
                // Read the contents of the T4 template file
                string templateContent = File.ReadAllText(templateFilePath);

                // Define the section marker in the T4 template
                string sectionMarker = $"<!-- {sectionName} -->";

                // Find the start and end positions of the section
                int sectionStart = templateContent.IndexOf(sectionMarker);
                if (sectionStart == -1)
                {
                    // Section not found, return the original content
                    return templateContent;
                }

                int sectionEnd = templateContent.IndexOf("-->", sectionStart) + 3;

                // Extract the content before and after the section
                string contentBefore = templateContent.Substring(0, sectionStart);
                string contentAfter = templateContent.Substring(sectionEnd);

                // Build the updated content by applying methodParameters
                StringBuilder updatedContent = new StringBuilder();
                updatedContent.Append(contentBefore);
                updatedContent.AppendLine(sectionMarker);

                foreach (var parameter in methodParameters)
                {
                    // Add each method parameter to the section
                    updatedContent.AppendLine($"// Parameter: {parameter.Name} ({parameter.Type})");
                }

                updatedContent.Append(contentAfter);

                // Return the updated content as a string
                return updatedContent.ToString();
            }
            catch (Exception ex)
            {
                // Handle any exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return string.Empty; // Return an empty string on error
            }
        }
    }

}
