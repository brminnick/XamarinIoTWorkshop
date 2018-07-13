#!/usr/bin/env bash
SyncfusionConstantsFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name SyncfusionConstants.cs | head -1`
PostBuildScriptFile=`find "$APPCENTER_SOURCE_DIRECTORY" -name appcenter-post-build.sh | grep Android | head -1`

echo SyncfusionConstantsFile = $SyncfusionConstantsFile
echo PostBuildScriptFile = $PostBuildScriptFile

sed -i '' "s/Your License Key Here/$SyncFusionLicenseKey/g" "$SyncfusionConstantsFile"
sed -i '' "s/#warning/\/\/#warning/g" "$SyncfusionConstantsFile"

echo "Finished Injecting SyncFusion License Key"

sed -i '' "s/--token token/--token $AppCenterLoginToken/g" "$PostBuildScriptFile"

echo "Finished Injecting App Center Login Token"