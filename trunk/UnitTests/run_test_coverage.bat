cd C:\Projects\SqlConfidence\SqlConfidence\packages\OpenCover.4.5.3522

OpenCover.Console.exe -register:user -target:"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\MSTest.exe" -targetargs:"/testcontainer:C:\Projects\SqlConfidence\SqlConfidence\UnitTests\bin\Debug\UnitTests.dll" -output:C:\Projects\SqlConfidence\SqlConfidence\UnitTests\unit_test_report.xml -filter:"+[*]*"

cd C:\Projects\SqlConfidence\SqlConfidence\packages\ReportGenerator.2.0.4.0

ReportGenerator.exe -reports:C:\Projects\SqlConfidence\SqlConfidence\UnitTests\unit_test_report.xml -targetdir:C:\Projects\SqlConfidence\SqlConfidence\UnitTests\reports

pause