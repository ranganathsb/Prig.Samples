# Set working directory for Prig.
$env:URASANDESU_PRIG_CURRENT_DIRECTORY = (Resolve-Path .\ProfilersChainTest\bin\Debug).Path

# Set the target process name that Prig should attach.
$env:URASANDESU_PRIG_TARGET_PROCESS_NAME = "nunit-agent"

# Run tests with OpenCover.
& .\packages\OpenCover.4.5.3723\OpenCover.Console.exe -target:runtests.bat -filter:+[ProfilersChain]*

# Format the result by ReportGenerator.
& .\packages\ReportGenerator.2.1.4.0\ReportGenerator.exe  -reports:results.xml -targetdir:coverage
