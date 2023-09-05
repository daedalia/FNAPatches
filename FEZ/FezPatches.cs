// Decompiled with JetBrains decompiler
// Type: FezPatches
// Assembly: FezPatches, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD57995F-35FE-4F15-80D6-A0DBA8CAEA13
// Assembly location: C:\dev\fez\FezPatches.dll

using FezEngine.Tools;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;

[ModEntryPoint]
public static class FezPatches
{
  public static void Main()
  {
    Console.Out.WriteLine("Found FezPatches, running...");
    try
    {
      new Harmony("com.github.jtothebell.FezPatches").PatchAll(Assembly.GetExecutingAssembly());
    }
    catch (Exception ex)
    {
      Console.Out.WriteLine("Failed: " + ex.ToString());
      throw ex;
    }
  }

  [HarmonyPatch(typeof (SettingsManager), "InitializeCapabilities")]
  private class PatchInitializeCapabilities
  {
    private static Exception Finalizer() => (Exception) null;

    private static void Postfix()
    {
      SettingsManager.Settings.HardwareInstancing = FNA3D.FNA3D_SupportsHardwareInstancing(((GraphicsDeviceManager) SettingsManager.DeviceManager).GraphicsDevice.GLDevice) > (byte) 0;
      SettingsManager.Settings.MultiSampleCount = ((GraphicsDeviceManager) SettingsManager.DeviceManager).GraphicsDevice.PresentationParameters.MultiSampleCount;
    }
  }

  [HarmonyPatch(typeof(SettingsManager), "SetupViewport")]
  private class PatchCustomViewScale
  {
    private static Exception Finalizer() => (Exception) null;

    private static void Postfix()
    {
      SettingsManager.SetupViewport(this GraphicsDevice);
        //(this GraphicsDevice _) => (float)0.65;
    }
  }
  
}
