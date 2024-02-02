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
    public class SyntaxTreeHelper
    {
        /// <summary>
        /// 动态编译
        /// </summary>
        /// <param name="classString">编译内容字符串</param>
        /// <param name="typeNames">类列表</param>
        /// <returns>类型列表</returns>
        /// <exception cref="Exception"></exception>
        public static List<Type> GetModelTypeByClass(string classString, List<string> typeNames)
        {
            //Write("Parsing the code into the SyntaxTree");
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(classString);

            string assemblyName = Path.GetRandomFileName();
            //命名空间
            string[] refPaths = {
                typeof(Object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location
            };
            MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using MemoryStream ms = new MemoryStream();
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

            //Write("Compilation successful! Now instantiating and executing the code ...");
            ms.Seek(0, SeekOrigin.Begin);

            Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
            List<Type> types = new List<Type>();
            foreach (var typeName in typeNames)
            {
                types.Add(assembly.GetType("RoslynCompileEasyPlcEntities." + typeName)); 
            }
                    
            //Console.WriteLine(type.Name);
            return types;
        }
    }
}