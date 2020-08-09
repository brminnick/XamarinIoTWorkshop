# Xamarin IoT Workshop

|CI Tool                    |Build Status|
|---------------------------|---|
| App Center, iOS | [![Build status](https://build.appcenter.ms/v0.1/apps/186c8afc-b757-4b79-aca3-3fe0711b7f64/branches/master/badge)](https://appcenter.ms) |
| App Center, Android | [![Build status](https://build.appcenter.ms/v0.1/apps/e149e8f1-80de-4084-953b-83a482e5abd8/branches/master/badge)](https://appcenter.ms) |

# About

This workshop will connect your mobile device to an [Azure IoT Hub](https://azure.microsoft.com/services/iot-hub?WT.mc_id=xamariniotworkshop-github-bramin) backend using a mobile app created in [Xamarin](https://visualstudio.microsoft.com/xamarin?WT.mc_id=xamariniotworkshop-github-bramin) and [Azure IoT Central](https://azure.microsoft.com/services/iot-central?WT.mc_id=xamariniotworkshop-github-bramin).

The app uses [Xamarin Essentials](https://docs.microsoft.com/xamarin/essentials?WT.mc_id=xamariniotworkshop-github-bramin) to gather sensor data from the device's Accelerometer & Gyroscope, displays the data in the app using [Syncfusion's Circular Gauge control](https://www.syncfusion.com/products/xamarin/circular-gauge), and then sends the sensor data to the IoT Central Portal where we can view the data.

| Mobile App              | Sending Data to IoT Central Dashboard |
:-------------------------|:-------------------------:
![Xamarin iOS App](https://user-images.githubusercontent.com/13558917/42401809-41173f26-812c-11e8-98f4-4703ccc062c3.gif) | ![IoT Central Dashboard](https://user-images.githubusercontent.com/13558917/42401851-6ceeae54-812c-11e8-9296-b3ddbf5e8249.png)

# Walkthrough

## 1. Install Xamarin App

**Note:** If this is workshop is being done in-person, led by Brandon Minnick, follow the [App Center Installation steps](./AppCenterInstallation.md).

1. Clone/Download this git repo

![Clone](https://user-images.githubusercontent.com/13558917/45580869-83982380-b8d9-11e8-8ed2-8415ac6480b3.png)

2. After downloading/cloning the repo, locate `XamarinIoTWorkshop.sln`
3. Launch `XamarinIoTWorkshop.sln` in [Visual Studio on PC](https://visualstudio.microsoft.com/vs/?WT.mc_id=xamariniotworkshop-github-bramin) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/?WT.mc_id=xamariniotworkshop-github-bramin)
4. Build & Deploy XamarinIoTWorkshop.iOS or XamarinIoTWorkshop.Android to your local iOS or Android device

## 2. Create IoT Central Device

1. On your computer, open a browser and navigate to [Azure IoT Central](https://apps.azureiotcentral.com/myapps?WT.mc_id=XamarinIoTWorkshop-github-bramin)
2. If requested, sign in with your Microsoft account
    - You can use any Microsoft-connected account, e.g. Azure account, Live account, Hotmail account, etc.
    - If you do not have a Microsoft-connected account, now is a great time to create one!
3. On the Azure IoT Central Portal, select **New Application**

![IoT Central, New Application](https://user-images.githubusercontent.com/13558917/42413959-63a4ed0e-81e0-11e8-9343-6b3cbc987a8a.png)

4. On the Create Application page, make the following selections:
    - **Choose payment plan**: Free
    - **Select an application template**: Custom Application
    - **Application Name**: Xamarin IoT Workshop [Last Name]
        -Note: Replace "[Last Name]" with your last name to create a unique Application Name
    - **Url**: xamarin-iot-workshop-[Last Name]
         -Note: Replace "[Last Name]" with your last name to create a unique Url

5. On the Create Application page, select **Create**

![IoT Central, Create Application](https://user-images.githubusercontent.com/13558917/42413957-6377635c-81e0-11e8-9eac-1dcf04527600.png)

6. On the Homepage screen, select **Create Device Templates**

![IoT Central, Create Device Templates](https://user-images.githubusercontent.com/13558917/42413958-638d2e76-81e0-11e8-808a-ac0707dcfea8.png)

7. On the **New device template** page, enter "Mobile Device"
8. On the **New device template** page, select **Create**

![IoT Central, New device template](https://user-images.githubusercontent.com/13558917/42414015-7ceccfb0-81e1-11e8-8866-bbde4ad17e06.png)

9. On the **Mobile Device 1** page, select "Delete"
    - **Mobile Device 1** is a simulated device created by Azure IoT Central
    - We are deleting the simulated device and connecting a real device

![IoT Central, Delete Simulated Device](https://user-images.githubusercontent.com/13558917/42413956-6362175e-81e0-11e8-8965-d7a2943678d5.png)

10. On the confirmation popup, select **Delete**

![IoT Central, Confirm Delete](https://user-images.githubusercontent.com/13558917/42414016-7d032f9e-81e1-11e8-83e5-535935fcc2c4.png)

11. On the **Explorer** page, select **New** -> **Real**

![IoT Central, New Real Device](https://user-images.githubusercontent.com/13558917/42413954-6333f036-81e0-11e8-9014-c07a466c2db7.png)

## 5. Add Measurements to IoT Central Device

1. On your computer, open a browser and navigate to [Azure IoT Central](https://apps.azureiotcentral.comhttps://apps.azureiotcentral.com?WT.mc_id=XamarinIoTWorkshop-github-bramin)
2. If requested, sign in with your Microsoft account
3. On the **Mobile Device** page, select **New Measurement** -> **Telemetry**

![IoT Central, New Measurement](https://user-images.githubusercontent.com/13558917/42414489-ed7153f6-81ea-11e8-8e30-587903a620fe.png)

![IoT Central, Telemetry](https://user-images.githubusercontent.com/13558917/42414488-ed58e78a-81ea-11e8-895c-f8f60e04913c.png)

4. In the **Create Telemetry** pane, enter the following information:
    - **Display Name**: AccelerometerX
    - **Field Name**: AccelerometerX
    - **Units**: [Leave Blank]
    - **Minimum Value**: -1
    - **Maximium Value**: 1
    - **Decimal Places**: 5
5. In the **Create Telemetry** pane, select **Save**

![IoT Central, AccelerometerX](https://user-images.githubusercontent.com/13558917/42414487-ed4355c8-81ea-11e8-8eb0-9033961de83c.png)

6. On the **Mobile Device** page, select **New Measurement** -> **Telemetry**
7. In the **Create Telemetry** pane, enter the following information:
    - **Display Name**: AccelerometerY
    - **Field Name**: AccelerometerY
    - **Units**: [Leave Blank]
    - **Minimum Value**: -1
    - **Maximium Value**: 1
    - **Decimal Places**: 5
8. In the **Create Telemetry** pane, select **Save**

![IoT Central, AccelerometerY](https://user-images.githubusercontent.com/13558917/42414511-47cc8294-81eb-11e8-97e3-224f625f9cc4.png)

9. On the **Mobile Device** page, select **New Measurement** -> **Telemetry**
10. In the **Create Telemetry** pane, enter the following information:
    - **Display Name**: AccelerometerZ
    - **Field Name**: AccelerometerZ
    - **Units**: [Leave Blank]
    - **Minimum Value**: -10
    - **Maximium Value**: 10
    - **Decimal Places**: 5
11. In the **Create Telemetry** pane, select **Save**

![IoT Central, AccelerometerZ](https://user-images.githubusercontent.com/13558917/42414484-ed1517b2-81ea-11e8-923f-2711b1c20830.png)

12. On the **Mobile Device** page, select **New Measurement** -> **Telemetry**
13. In the **Create Telemetry** pane, enter the following information:
    - **Display Name**: GyroscopeX
    - **Field Name**: GyroscopeX
    - **Units**: [Leave Blank]
    - **Minimum Value**: -1
    - **Maximium Value**: 1
    - **Decimal Places**: 5
14. In the **Create Telemetry** pane, select **Save**

![IoT Central, GyroscopeX](https://user-images.githubusercontent.com/13558917/42414483-ecfeaf18-81ea-11e8-99dc-6a0faf3cab79.png)

15. On the **Mobile Device** page, select **New Measurement** -> **Telemetry**
16. In the **Create Telemetry** pane, enter the following information:
    - **Display Name**: GyroscopeY
    - **Field Name**: GyroscopeY
    - **Units**: [Leave Blank]
    - **Minimum Value**: -1
    - **Maximium Value**: 1
    - **Decimal Places**: 5
17. In the **Create Telemetry** pane, select **Save**

![IoT Central, GyroscopeY](https://user-images.githubusercontent.com/13558917/42414482-ece9b2ca-81ea-11e8-810a-044894100872.png)

18. On the **Mobile Device** page, select **New Measurement** -> **Telemetry**
19. In the **Create Telemetry** pane, enter the following information:
    - **Display Name**: GyroscopeZ
    - **Field Name**: GyroscopeZ
    - **Units**: [Leave Blank]
    - **Minimum Value**: -5
    - **Maximium Value**: 5
    - **Decimal Places**: 5
20. In the **Create Telemetry** pane, select **Save**

![IoT Central, GyroscopeZ](https://user-images.githubusercontent.com/13558917/42414481-ecd293d8-81ea-11e8-81a9-5d162b25ef90.png)

## 3. Get Device Connection String

1. In **IoT Central** on the **Mobile Device** page, select **Connect this device**

![Iot Central, Connect This Device](https://user-images.githubusercontent.com/13558917/42414606-339e1588-81ed-11e8-9c75-106a2238c092.png)

2. In the **Connect this device** popup, select the **Copy** button adjacent to **Primary connection string**

![IoT Central, Copy Primary Connection String](https://user-images.githubusercontent.com/13558917/42414605-3385ca0a-81ed-11e8-8f97-3e43d86134dd.png)

3. On your computer, open a text editor, e.g. Notepad, TextEdit, Visual Studio Code, etc.
4. In the text editor, paste the **Primary connection string** value

![Primary Connection String](https://user-images.githubusercontent.com/13558917/42414604-336c6164-81ed-11e8-8dc4-575e100d253c.png)

## 7. Install iOS App (iOS only)

We will now install the iOS app from App Center. If you have an Android Device and have already installed the Android app, skip to **Step 8**.

1. On your iOS device, open Safari
2. In Safari on your iOS device, navigate to the App Center installation page:[https://install.appcenter.ms](https://install.appcenter.ms)
3. On the installation page, if prompted, sign in

<img alt="App Center Installation Page" src="https://user-images.githubusercontent.com/13558917/42402802-ec844b24-8131-11e8-8f5a-affb75895f0b.png" width="200px">

4. On the installation page in Safari, tap on **Xamarin IoT Workshop, iOS**

<img alt="Xamarin IoT Workshop, iOS" src="https://user-images.githubusercontent.com/13558917/42534694-3d5bff02-8442-11e8-8f7f-111c0fdac6d1.png" width="200px">

5. On the **Xamarin IoT Workshop, iOS** page, select **Install**

<img alt="iOS Installation Page" src="https://user-images.githubusercontent.com/13558917/42414397-65a0babc-81e9-11e8-8a14-a47aaad93aa8.png" width="200px">

6. On the confirmation popup, select **Install**

<img alt="iOS Installation Confirmation" src="https://user-images.githubusercontent.com/13558917/42414396-65878768-81e9-11e8-8438-2f5050f8b5e9.png" width="200px">

7. On the iOS device, navigate to the iOS Home Screen
8. On the iOS Home Screen, confirm that the app is installing

<img alt="iOS Home Screen Installation" src="https://user-images.githubusercontent.com/13558917/42414398-693b41ec-81e9-11e8-8d27-b0d18764692c.PNG" width="200px">

## 8. Connect App to IoT Central

1. On your mobile device, launch the XamarinIoTWorkshop app
2. In the XamarinIoTWorkshop app, tap the **Settings** tab

<img alt="XamarinIoTWorkshop App, Accelerometer Page" src="https://user-images.githubusercontent.com/13558917/42414665-c45f520c-81ee-11e8-88a4-416d8499063f.png" width="200px">

<img alt="XamarinIoTWorkshop App, Accelerometer Page" src="https://user-images.githubusercontent.com/13558917/42414666-c47590f8-81ee-11e8-910a-f7a9103fc150.png" width="200px">

3. On **Settings** page, enter the **Primary Connection String** into the text box
    - To avoid typos, I recommend emailing yourself the connection string, then copy/pasting the connection string into this text box
4. On the **Settings** page, toggle the **Send Data to Azure** switch to **On**

<img alt="XamarinIoTWorkshop App, Accelerometer Page" src="https://user-images.githubusercontent.com/13558917/42414667-c48b71f2-81ee-11e8-8872-25e4076a04d2.png" width="200px">

5. Keep the app running in the foreground on your mobile device
    - The app will only collect data and send it to IoT Central while it is running in the foreground

## 3. View Data On IoT Central Dashboard

As you rotate your phone, the guages in the app will update and the data will appear on the IoT Central Dashboard in near-realtime

![IoT Central Dashboard](https://user-images.githubusercontent.com/13558917/42401851-6ceeae54-812c-11e8-9296-b3ddbf5e8249.png)

# Resources

- [App Center](https://appcenter.ms?WT.mc_id=XamarinIoTWorkshop-github-bramin)
- [Azure IoT Central](https://azure.microsoft.com/services/iot-central?WT.mc_id=xamariniotworkshop-github-bramin)
- [Azure IoT Hub](https://azure.microsoft.com/services/iot-hub?WT.mc_id=xamariniotworkshop-github-bramin)
- [Syncfusion's Circular Gauge control](https://www.syncfusion.com/products/xamarin/circular-gauge)
- [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac?WT.mc_id=xamariniotworkshop-github-bramin)
- [Visual Studio on PC](https://visualstudio.microsoft.com/vs?WT.mc_id=xamariniotworkshop-github-bramin)
- [Xamarin](https://visualstudio.microsoft.com/xamarin?WT.mc_id=xamariniotworkshop-github-bramin)
- [Xamarin Essentials](https://docs.microsoft.com/xamarin/essentials?WT.mc_id=xamariniotworkshop-github-bramin)
