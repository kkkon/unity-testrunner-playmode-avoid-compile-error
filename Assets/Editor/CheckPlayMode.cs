/*
 * The MIT License
 *
 * Copyright 2018 kkkon.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CheckPlayMode
{
    public static bool USE_TEST_PLAYMODE = true;

    public static void PlayModeBuild()
    {
#if ENABLE_PLAYMODE_TESTS_RUNNER && USE_PLAYMODE_TESTS_RUNNER
        if ( !CheckPlayMode.USE_TEST_PLAYMODE )
        {
            // https://feedback.unity3d.com/suggestions/stop-slash-cancel-build-from-code-postprocessbuild-buildpipeline-dot-cancelbuild-1
            string message = "";
            Debug.LogError(message);
            UnityEngine.Assertions.Assert.IsTrue(false,message);
            throw new System.Exception(message);
        }
#endif
    }

}



#if UNITY_5_6_OR_NEWER && !UNITY_2018_1_OR_NEWER
public class CheckPlayModePreprocessBuild
  : UnityEditor.Build.IPreprocessBuild // 2018.1 obsolate
{
  public int callbackOrder { get { return 0; } }
  public void OnPreprocessBuild(BuildTarget target, string path )
  {
      Debug.Log("OnPreprocessBuild" + target + " path=" + path );
      CheckPlayMode.PlayModeBuild();
  }
}
#elif UNITY_2018_1_OR_NEWER
public class CheckPlayModePreprocessBuild
  : UnityEditor.Build.IPreprocessBuildWithReport
{
  public int callbackOrder { get { return 0; } }
  public void OnPreprocessBuild(UnityEditor.Build.Reporting.BuildReport report )
  {
      Debug.Log("OnPreprocessBuild" + report.summary.platform + " path=" + report.summary.outputPath );
      CheckPlayMode.PlayModeBuild();
  }
}
#else
#error
#endif
