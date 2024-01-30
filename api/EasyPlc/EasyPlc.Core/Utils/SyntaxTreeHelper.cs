/*=============================================================================================
*
*      *******    *******         **    **
*      **         **              **    **
*      **         **              **    **
*      *******    *******   **    ********
*           **    **              **    **
*           **    **              **    **
*      *******    **              **    **
*
* 创建者：韩顺发
* CLR版本：4.0.30319.42000
* 电子邮箱：shunfa.han@kstopa.com.cn
* 创建时间：2024/1/29 16:43:30
* 版本：v1.0.0
* 描述：
*
* ==============================================================================================
* 修改人：
* 修改时间：
* 修改说明：
* 版本：
*
===============================================================================================*/

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlc.Core.Utils
{
    internal class SyntaxTreeHelper
    {
        public static Type GetModelTypeByClass(string classString, string typeName)
        {
            //Write("Parsing the code into the SyntaxTree");
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(classString);

            string assemblyName = Path.GetRandomFileName();
            //命名空间
            string[] refPaths = new[] {
                //typeof(AccessViolationException).Assembly.Location,
                typeof(System.Object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll"),
            };
            MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

            //Write("Adding the following references");
            //foreach (var r in refPaths)
            // Write(r);

            //Write("Compiling ...");
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (MemoryStream ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    //Write("Compilation failed!");
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    string message = "";
                    foreach (Diagnostic diagnostic in failures)
                    {
                        message += diagnostic.GetMessage();
                    }
                    throw new Exception("解析实体类出错，请检查命名" + message + " \r\n " + classString);
                }
                else
                {
                    //Write("Compilation successful! Now instantiating and executing the code ...");
                    ms.Seek(0, SeekOrigin.Begin);

                    Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    var type = assembly.GetType("RoslynCompileSampleDemo." + typeName);
                    //Console.WriteLine(type.Name);
                    return type;
                }
            }
        }
    }
}