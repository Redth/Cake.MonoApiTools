using NUnit.Framework;
using System;
using Cake.Xamarin.Tests.Fakes;
using Cake.Core.IO;
using Cake.MonoApiTools;
using System.Linq;

namespace Cake.FileHelpers.Tests
{
    [TestFixture]
    public class FileHelperTests
    {
        FakeCakeContext context;

        [SetUp]
        public void Setup ()
        {
            context = new FakeCakeContext ();

            var dp = new DirectoryPath ("./testdata");
            var d = context.CakeContext.FileSystem.GetDirectory (dp);

            if (d.Exists)
                d.Delete (true);

            d.Create ();
        }

        //[Test]
        public void HtmlDiff ()
        {
            var path = context.CakeContext.Globber.GetFiles ("../../../**/mono-api-html.exe").FirstOrDefault ();

            var prev = "/Users/redth/xamarin/GooglePlayServices/output/GooglePlayServices.api-info.previous.xml";
            var next = "/Users/redth/xamarin/GooglePlayServices/output/GooglePlayServices.api-info.xml";

            context.CakeContext.MonoApiHtml (prev, next, "/Users/redth/Desktop/test.html", new MonoApiHtmlToolSettings {
                ToolPath = path
            });
        }

        [TearDown]
        public void Teardown ()
        {
            context.DumpLogs ();
        }
    }
}

