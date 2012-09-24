#This build assumes the following directory structure
#
#  \               - This is where the project build code lives
#  \build          - This folder is created if it is missing and contains output of the build
#  \src            - This folder contains the source code or solutions you want to build
#

Properties {
    $build_dir = Split-Path $psake.build_script_file    
    $build_artifacts_dir = "$build_dir\build\"
    $solution_file = "$build_dir\src\Squawkings.sln"
    $nunit_dir = "$build_dir\src\packages\NUnit.Runners.2.6.1\tools"
    $version = "0.1.0"
    $newversion = $version
}

Framework("4.0")

FormatTaskName (("-"*25) + "[{0}]" + ("-"*25))

Task Default -Depends Package

Task Package -Depends Test {
    Write-Host "Packaging" -ForegroundColor Green
}

Task Test -depends Compile {
    Write-Host "Running Tests" -ForegroundColor Green
    $testAssemblies = (Get-ChildItem "$build_artifacts_dir\dlls\" -Recurse -Include *Tests.dll)
    Exec { 
        & $nunit_dir\nunit-console.exe $testAssemblies /nodots /nologo /xml=$build_artifacts_dir\testresults.xml;
    }
}

Task Compile -Depends Clean { 
    Write-Host "Building $solution_file" -ForegroundColor Green

    New-Item -force "$build_artifacts_dir\web" -type directory | Out-Null
    Exec { msbuild "$solution_file" /t:Build /p:Configuration=Release /v:quiet /p:MvcBuildViews=true /p:OutDir="$build_artifacts_dir\dlls\" /p:WebProjectOutputDir="$build_artifacts_dir\web\" } 
}

Task Clean {
    
    Write-Host "Creating BuildArtifacts directory" -ForegroundColor Green
    if (Test-Path $build_artifacts_dir) 
    {   
        rd $build_artifacts_dir -rec -force | out-null
    }
    
    mkdir $build_artifacts_dir | out-null
    
    Write-Host "Cleaning $solution_file" -ForegroundColor Green
    Exec { msbuild "$solution_file" /t:Clean /p:Configuration=Release /v:quiet } 
}

function Generate-VersionNumber {
    $today = Get-Date
    return ( ($today.year - 2000) * 1000 + $today.DayOfYear )
}

function CopyTo-Directory($files, $dir){
    Create-Directory $dir
    cp $files $dir -recurse -container
}

function Create-Directory($dir){
    if (!(Test-Path -path $dir)) { new-item $dir -force -type directory}
}

function Update-DbVersion ($filename, $version) {
    $versionPattern = '<version>(.*)</version>'
    $versionReplace = "<version>$version</version>"
    (get-content $filename) | % {$_ -replace $versionPattern, $versionReplace } | set-content $filename
}

function Update-AssemblyInfoFiles ([string]$path, [string] $version, [string]$excludePath, [System.Array] $excludes = $null, $make_writeable = $false) {

#-------------------------------------------------------------------------------
# Update version numbers of AssemblyInfo.cs
# adapted from: http://www.luisrocha.net/2009/11/setting-assembly-version-with-windows.html
#-------------------------------------------------------------------------------

    if ($version -notmatch "[0-9]+(\.([0-9]+|\*)){1,3}") {
        Write-Error "Version number incorrect format: $version"
    }

    $versionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $versionAssembly = 'AssemblyVersion("' + $version + '")'
    $versionFilePattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $versionAssemblyFile = 'AssemblyFileVersion("' + $version + '")'

    Get-ChildItem -path "$path" -r -filter AssemblyInfo.cs | % {
        $filename = $_.fullname

        $update_assembly_and_file = $true

        # set an exclude flag where only AssemblyFileVersion is set
        if ($excludes -ne $null) { 
            $excludes | % { 
                if ($filename -match $_) { 
                    $update_assembly_and_file = $false 
                } 
            } 
        }
        
        if ($filename -notMatch $excludePath)
        {
            # see http://stackoverflow.com/questions/3057673/powershell-locking-file
            # I am getting really funky locking issues. 
            # The code block below should be:
            #     (get-content $filename) | % {$_ -replace $versionPattern, $version } | set-content $filename

            $tmp = ($file + ".tmp")
            if (test-path ($tmp)) { remove-item $tmp }

            if ($update_assembly_and_file) {
                (get-content $filename) | % {$_ -replace $versionFilePattern, $versionAssemblyFile } | % {$_ -replace $versionPattern, $versionAssembly }  > $tmp
                write-host Updating file AssemblyInfo and AssemblyFileInfo: $filename --> $versionAssembly / $versionAssemblyFile
            } else {
                (get-content $filename) | % {$_ -replace $versionFilePattern, $versionAssemblyFile } > $tmp
                write-host Updating file AssemblyInfo only: $filename --> $versionAssemblyFile
            }

            if (test-path ($filename)) { remove-item $filename }
            move-item $tmp $filename -force 
        }
    }
}
