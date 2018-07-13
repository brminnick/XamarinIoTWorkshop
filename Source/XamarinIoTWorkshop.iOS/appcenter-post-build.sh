#!/usr/bin/env bash
if [ "$APPCENTER_XAMARIN_CONFIGURATION" == "Debug" ];then

    echo "Post Build Script Started"
    
    SolutionFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name XamarinIoTWorkshop.sln`
    SolutionFileFolder=`dirname $SolutionFile`

    UITestProject=`find "$APPCENTER_SOURCE_DIRECTORY" -name XamarinIoTWorkshop.UITests.csproj`

    echo SolutionFile: $SolutionFile
    echo SolutionFileFolder: $SolutionFileFolder
    echo UITestProject: $UITestProject

    chmod -R 777 $SolutionFileFolder

    msbuild "$UITestProject" /property:Configuration=$APPCENTER_XAMARIN_CONFIGURATION

    UITestDLL=`find "$APPCENTER_SOURCE_DIRECTORY" -name "XamarinIoTWorkshop.UITests.dll" | grep bin`
    UITestBuildDir=`dirname $UITestDLL`

    IPAFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name *.ipa | head -1`

    DSYMFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name *.dsym | head -1`
    DSYMDirectory=`dirname $DSYMFile`

    npm install -g appcenter-cli

    appcenter login --token token

    appcenter test run uitest --app "Xamarin-IoT-Workshop/Xamarin-IoT-Workshop-iOS" --devices "Xamarin-IoT-Workshop/my-device-set" --app-path $IPAFile --test-series "master" --locale "en_US" --build-dir $UITestBuildDir --dsym-dir $DSYMDirectory --async
fi