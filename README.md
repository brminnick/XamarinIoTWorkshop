# Xamarin IoT Workshop

|CI Tool                    |Build Status|
|---------------------------|---|
| App Center, iOS | [![Build status](https://build.appcenter.ms/v0.1/apps/186c8afc-b757-4b79-aca3-3fe0711b7f64/branches/master/badge)](https://appcenter.ms) |
| App Center, Android | [![Build status](https://build.appcenter.ms/v0.1/apps/e149e8f1-80de-4084-953b-83a482e5abd8/branches/master/badge)](https://appcenter.ms) |

# About

This workshop will connect your mobile device to an [Azure IoT Hub](https://azure.microsoft.com/services/iot-hub/?WT.mc_id=XamarinIoTWorkshop-github-bramin) backend using a mobile app created in [Xamarin](https://visualstudio.microsoft.com/xamarin/?WT.mc_id=XamarinIoTWorkshop-github-bramin) and [Azure IoT Central](https://azure.microsoft.com/services/iot-central/?WT.mc_id=XamarinIoTWorkshop-github-bramin).

The app uses [Xamarin Essentials](https://docs.microsoft.com/xamarin/essentials/) to gather sensor data from the device's Accelerometer & Gyroscope, displays the data in the app using [Syncfusion's Circular Gauge control](https://www.syncfusion.com/products/xamarin/circular-gauge), and then sends the sensor data to the IoT Central Portal where we can view the data.

## Xamarin App

![Xamarin iOS App](https://user-images.githubusercontent.com/13558917/42401809-41173f26-812c-11e8-98f4-4703ccc062c3.gif)

## IoT Central Dashboard

![IoT Central Dashboard](https://user-images.githubusercontent.com/13558917/42401851-6ceeae54-812c-11e8-9296-b3ddbf5e8249.png)

# Walkthrough

## 1. Create an App Center account

We will use [App Center](https://appcenter.ms/?WT.mc_id=XamarinIoTWorkshop-github-bramin) to download the app to our mobile device. Users familiar with Xamarin are welcome to download the source code in this GitHub repo and manually build/deploy the iOS/Android app using [Visual Studio on PC](https://visualstudio.microsoft.com/vs/?WT.mc_id=XamarinIoTWorkshop-github-bramin) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/?WT.mc_id=XamarinIoTWorkshop-github-bramin)

1. Navigate to [https://appcenter.ms](https://appcenter.ms)
2. Choose **Continue with GitHub**, **Continue with Microsoft**, **Facebook**, **Google**, or **Create a new account** to create a new account

![App Center Registration](https://user-images.githubusercontent.com/13558917/42402275-94801690-812e-11e8-8000-655b7f2f7ae7.png)

## 2. Register for the Xamarin IoT Workshop App

Each attendee will need to register

1. [Send me an email](mailto:brandon.minnick@microsoft.com?subject=Xamarin%20IoT%20Workshop%20App%20Center%20Registration)
    - To: Brandon.Minnick@Microsoft.com
    - Subject: Xamarin IoT Workshop App Center Registration
2. Standby as I manually add each participant to the App Center distribution group
3. Once I have registered you, you will receive an email from App Center
4. In the email from App Center, tap **Accept Invitation**

![App Center Invitation Email](https://user-images.githubusercontent.com/13558917/42404139-957c6f64-813b-11e8-8460-bf8ba956b7bc.png)

## 3a. Download App (Android)

If you have an iOS Device, continue to step **3b. Register Device (iOS)**

1. On your Android device, open Chrome
2. In Chrome on your Android device, navigate to the App Center installation page: [https://install.appcenter.ms](https://install.appcenter.ms)
3. On the installation page, if prompted, sign in

![App Center Install Sign In Page](https://user-images.githubusercontent.com/13558917/42402802-ec844b24-8131-11e8-8f5a-affb75895f0b.png)
4. On the App Center Installation Page, scroll down and locate Xamarin IoT Workshop, Android
5. On the App Center Installation Page, tap Xamarin IoT Workshop, Android
![Android Install Xamarin IoT Workshop](https://user-images.githubusercontent.com/13558917/42402804-ecb23b88-8131-11e8-8e06-ced1bb30c6d6.png)
6. On the Android download page, select Download
![Android Download](https://user-images.githubusercontent.com/13558917/42402790-dcba12c8-8131-11e8-9b94-2c83dcf5751c.png)
7. On the **Downloading...** page, select **Download**
![Android Downloading Page](https://user-images.githubusercontent.com/13558917/42402791-dcd78cd6-8131-11e8-99b3-c45a15da4846.png)
8. Stand by until the download has completed
![Android Downloading](https://user-images.githubusercontent.com/13558917/42402792-dcf56ec2-8131-11e8-9906-31d689f6a5c2.png)
9. After the download has completed, select **Open**
![Android Download Completed](https://user-images.githubusercontent.com/13558917/42402808-ed20e876-8131-11e8-9527-e8539cfe4e75.png)
10. On the Android Install page, select **Install**
![Android Install Page](https://user-images.githubusercontent.com/13558917/42402809-ed3a9ab4-8131-11e8-82f5-e3c41db7b03c.png)
11. Standby until the app has finished installing
![Android App Installing](https://user-images.githubusercontent.com/13558917/42402810-ed5b219e-8131-11e8-845f-e172c40d28de.png)
12. Once the installation has finished, select **OPEN** 
![Android Installation Finished](https://user-images.githubusercontent.com/13558917/42402811-ed7e4f84-8131-11e8-95f4-e77d67888754.png)

## 3b. Register Devie (iOS)

If you have an Android Device, continue to step **4**.

Apple does not allow users to download iOS apps that have not been previously registered. In this step, we will register for the iOS app, then download it later in the workshop after each device has been added to the app's provisioning profile.

1. On your iOS device, open Safari
2. In Safari on your iOS device, navigate to the App Center installation page: [https://install.appcenter.ms](https://install.appcenter.ms)
3. On the installation page, if prompted, sign in
4. Once signed-in, on the installation page, select **+ Add Device**

![Add iOS Device](https://user-images.githubusercontent.com/13558917/42399795-0cbd3152-8124-11e8-857a-d7db238acc17.PNG)
5. On the iOS prompt, select "Allow" which will open the **Settings** app
![iOS Prompt](https://user-images.githubusercontent.com/13558917/42399794-0c95e9ee-8124-11e8-88bf-aaa2634ffe77.PNG)
6. In the Settings app, select **Install**
![Install Profile](https://user-images.githubusercontent.com/13558917/42399793-0c7ae32e-8124-11e8-87f1-3c12d29cfd5f.PNG)
7. If prompted, enter your device's lock-screen passcode
![Lock Screen Passcode](https://user-images.githubusercontent.com/13558917/42399791-0c63d6c0-8124-11e8-87d7-ecf964c5b955.PNG)
8. On the popup confirmation, select **Install**
![Popup dialog](https://user-images.githubusercontent.com/13558917/42399790-0c33a5fe-8124-11e8-8da9-912b31aa7007.PNG)
9. Once the installation has completed, the device will return you back to the install page in Safari
10. On the installation page in Safari, tap on **Xamarin IoT Workshop, iOS**
![Xamarin IoT Workshop, iOS](https://user-images.githubusercontent.com/13558917/42399787-0bf6669e-8124-11e8-9e1c-fe9dcc7c01d5.PNG)
11. On the installation page, confirm that it says **No releases for app**. Once I've added you to the app's Provisioning Profile, iOS will allow you the download the app here.
![No releases for app](https://user-images.githubusercontent.com/13558917/42399786-0bd82986-8124-11e8-8320-66384fbfb5a5.PNG)

## 4. Create IoT Central Device

1. On your computer, open a browser and navigate to [Azure IoT Central](https://apps.azureiotcentral.com/)
2. If requested, sign in with your Microsoft account
    - You can use any Microsoft-connected account, e.g. Azure account, Live account, Hotmail account (does anyone still use Hotmail?)
    - If you do not have a Microsoft-connected, create one!
3. On the Azure IoT Central Portal, select **New Application**

![IoT Central, New Application](https://user-images.githubusercontent.com/13558917/42413959-63a4ed0e-81e0-11e8-9343-6b3cbc987a8a.png)

4. On the Create Application page, make the following selections:
    - **Choose payment plan**: Free
    - **Select an appliation template**: Custom Application
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

![IoT Centra, New Real Device](https://user-images.githubusercontent.com/13558917/42413954-6333f036-81e0-11e8-9014-c07a466c2db7.png)

## 5. Add Measurements to IoT Central Device

# Resources

- [App Center](https://appcenter.ms)
- [Azure IoT Central](https://azure.microsoft.com/services/iot-central/?WT.mc_id=XamarinIoTWorkshop-github-bramin)
- [Azure IoT Hub](https://azure.microsoft.com/services/iot-hub/?WT.mc_id=XamarinIoTWorkshop-github-bramin)
- [Syncfusion's Circular Gauge control](https://www.syncfusion.com/products/xamarin/circular-gauge)
- [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/?WT.mc_id=XamarinIoTWorkshop-github-bramin)
- [Visual Studio on PC](https://visualstudio.microsoft.com/vs/?WT.mc_id=XamarinIoTWorkshop-github-bramin)
- [Xamarin](https://visualstudio.microsoft.com/xamarin/?WT.mc_id=XamarinIoTWorkshop-github-bramin)
- [Xamarin Essentials](https://docs.microsoft.com/xamarin/essentials/)
