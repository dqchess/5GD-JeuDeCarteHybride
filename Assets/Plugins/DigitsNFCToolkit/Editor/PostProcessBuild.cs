#if UNITY_IOS
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
#endif

namespace DigitsNFCToolkit.Editor
{
	public class PostProcessBuild
	{
#if UNITY_IOS
        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                AddNFCReaderUsageDescription(path);
                AddNFCCapability(path);
            }
        }

        private static void AddNFCReaderUsageDescription(string path)
        {
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            PlistElementDict rootDict = plist.root;
            rootDict.SetString("NFCReaderUsageDescription", "Reading NFC Tags");

            File.WriteAllText(plistPath, plist.WriteToString());
        }

        private static void AddNFCCapability(string path)
        {
            string projectPath = PBXProject.GetPBXProjectPath(path);
            //PBXProject project = new PBXProject();
            //project.ReadFromFile(projectPath);

            String packageName = UnityEngine.Application.identifier;
            String name = packageName.Substring(packageName.LastIndexOf('.') + 1);
            String entitlementFileName = name + ".entitlements";
            String entitlementPath = Path.Combine(path, entitlementFileName);
            ProjectCapabilityManager projectCapabilityManager = new ProjectCapabilityManager(projectPath, entitlementFileName, PBXProject.GetUnityTargetName());
            PlistDocument entitlementDocument = AddNFCEntitlement(projectCapabilityManager);
            entitlementDocument.WriteToFile(entitlementPath);

            var projectInfo = projectCapabilityManager.GetType().GetField("project", BindingFlags.NonPublic | BindingFlags.Instance);
            PBXProject project = (PBXProject)projectInfo.GetValue(projectCapabilityManager);

            string target = project.TargetGuidByName(PBXProject.GetUnityTargetName());
            var constructor = typeof(PBXCapabilityType).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[]{typeof(string), typeof(bool), typeof(string), typeof(bool)}, null);
            PBXCapabilityType nfcCapability = (PBXCapabilityType)constructor.Invoke(new object[] { "com.apple.NearFieldCommunicationTagReading", true, "", false });
            project.AddCapability(target, nfcCapability, entitlementFileName);

            projectCapabilityManager.WriteToFile();
        }

        private static PlistDocument AddNFCEntitlement(ProjectCapabilityManager projectCapabilityManager)
        {
            MethodInfo getMethod = projectCapabilityManager.GetType().GetMethod("GetOrCreateEntitlementDoc", BindingFlags.NonPublic | BindingFlags.Instance);
            PlistDocument entitlementDoc = (PlistDocument)getMethod.Invoke(projectCapabilityManager, new object[] { });

            PlistElementDict dictionary = entitlementDoc.root;
            PlistElementArray array = dictionary.CreateArray("com.apple.developer.nfc.readersession.formats");
            array.values.Add(new PlistElementString("NDEF"));

            return entitlementDoc;
        }
#endif
	}
}

