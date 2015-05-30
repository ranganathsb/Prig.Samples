$env:URASANDESU_PRIG_CURRENT_DIRECTORY = (Resolve-Path .\ProfilersChainTest\bin\Debug).Path
$env:URASANDESU_PRIG_TARGET_PROCESS_NAME = "nunit-agent"
& .\packages\OpenCover.4.5.3723\OpenCover.Console.exe -target:runtests.bat -filter:+[ProfilersChain]*
& .\packages\ReportGenerator.2.1.4.0\ReportGenerator.exe  -reports:results.xml -targetdir:coverage
