#!/usr/bin/env bash
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

APKFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name *.apk | head -1`

DSYMFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name *.dsym | head -1`
DSYMDirectory=`dirname $DSYMFile`

npm install -g appcenter-cli

appcenter login --token token

appcenter test run uitest --app "Xamarin-IoT-Workshop/XamarinIoTWorkshop-Android" --devices "Xamarin-IoT-Workshop/all-android-os" --app-path $APKFile --test-series "master" --locale "en_US" --build-dir $UITestBuildDir --dsym-dir $DSYMDirectory --async