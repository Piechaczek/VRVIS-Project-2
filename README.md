# VRVIS Project - AR Graph Visualization (using Vuforia)

AR Graph Visualization for Android using Unity and Vuforia.

## Setup the repo and run the app

1. After cloning the repo, copy the .tgz file found [here](https://drive.google.com/file/d/1qXaHHxr9COLdIjZp_iGSy4vKcBtO1e2_/view?fbclid=IwAR0xImbXpm6gpqLmAGcjnzB24ujmiWob6Lj9b-5cXKZXCSDMBlpoLuPL1JA) (Google Drive) to the `/Packages` folder. Copy the whole .tgz file, don't extract the contents. \
Alternatively, you can install vuforia from scratch if the above doesn't work.

2. Add the project to UnityHub and open it in the UnityEditor.

3. Open the scene `VuforiaScene` in `/Assets/Scenes`

4. Go to `File -> Build Settings`, select `Android` and press `Switch Platform` (you may have to download the Android package for Unity if this is your first time). 

5. Connect your Android device (Oreo API 26+) and select "Use this device for file sharing" or an equivalent option when prompted. Make sure to have `USB Debugging` enabled. Still under `Build Settings` under `Run Device` press refresh and select your device from the list.

6. Press `Build and Run` in the `Build settings` or use `File -> Build and run` to run the app. \
You will be prompted to select a file for the resulting .apk file. Create a folder named `/Builds` in the top-level project folder (this folder is in .gitignore). Select it and choose a name for the resulting apk file, e.g. `build.apk`. The choice of the name is irrelevant, and you can override this file in subsequent runs. \
The resulting .apk will automatically be installed and launched on your connected device. You will be prompted to grant camera access during first launch. Make sure to grant it, as the app will not ask again on subsequent runs.

7. Open the tracker image located in the `/Assets` folder. If everything works correctly, you will see a graph appear when you point your phone camera at it.