// Import types from System and System.Reflection
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;

//[assembly: AssemblyTitle("Title")]

/// <summary>
/// Summary
/// </summary>
public class FindAttributes
{
    static void Main(string[] args)
    {
        // Output usage information if necessary
        if (args.Length == 0)
            Usage();
        else if ((args.Length == 1) && (args[0] == "/?"))
            Usage();
        else
        {
            // Load the assembly
            string assemblyName = args.First();
            string attribute = args.Skip(1).FirstOrDefault();

            try
            {
                // Attempt to load the named assembly
                Assembly assembly = Assembly.LoadFrom(assemblyName);

                Console.WriteLine("Assembly attributes for '{0}'...", assembly.GetName().Name);
                var attributes = ListAttributes(assembly);

                if (attribute == null)
                    attributes.ToList().ForEach(Console.WriteLine);
                else if (attribute.StartsWith("/i"))
                    PrintProperties(assembly);
                else if (!attributes.Any())
                    Console.WriteLine("...no attributes");
                else
                    PrintAttributeInfo(assembly, attribute);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown loading assembly {0}...", assemblyName);
                Console.WriteLine();
                Console.WriteLine(ex.ToString());
            }
        }
    }

    private static void PrintAttributeInfo(Assembly assembly, string substring)
    {
        var matches = assembly.GetCustomAttributes(true)
            .Where(asm => asm.ToString().IndexOf(substring,
                StringComparison.InvariantCultureIgnoreCase) >= 0)
            .ToList();

        switch (matches.Count)
        {
            case 0:
                Console.WriteLine("No attribute containing '{0}'.", substring);
                break;
            case 1:
                PrintProperties(matches.Single());
                break;
            default:
                Console.WriteLine("You need to be more specific, matches:");
                matches.ForEach(m => Console.WriteLine("\t* {0}", m.ToString()));
                break;
        }    
    }

    private static void PrintProperties(object instance)
    {
        if (instance == null)
            throw new ArgumentNullException(nameof(instance));

        Console.WriteLine("Listing properties of {0}", instance.ToString());
        Dictionary<string, string> properties =
            (from x in instance.GetType().GetProperties() select x)
                .ToDictionary(x => x.Name,
                    x => (x.GetGetMethod().Invoke(instance, null) == null
                        ? string.Empty
                        : x.GetGetMethod().Invoke(instance, null).ToString())
                );
        foreach (var property in properties)
        {
            Console.WriteLine($"{property.Key,15} : {property.Value}");
        }
    }

    private static IEnumerable<string> ListAttributes(Assembly assembly)
    {
        object[] attributes = assembly.GetCustomAttributes(true);

        return attributes
            .Select(attr => attr.ToString())
            .ToList();
    }

    static void Usage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  FindAttr <Assembly> <Attribute|/info>");
    }
}