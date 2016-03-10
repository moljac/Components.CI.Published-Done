#load "./common.cake"

/*
    mono \
        ~/bin/tools/Cake/Cake.exe \
        -verbosity=diagnostic

    Docs:
    https://github.com/cake-build/cake
    http://cakebuild.net/
    
    http://cakebuild.net/docs/tutorials/getting-started
    
    http://redth.codes/would-you-like-some-csharp-in-your-cake/
    http://patriksvensson.se/2014/07/its-not-a-party-without-cake/
    
    http://cakebuild.net/addins?path=
*/

//###################################################################################
// Externals setup
//      external files archives needed to build artifacts

string folder_externals_extensive = System.IO.Path.Combine(".", "external", "binaries-xtensive");
string folder_externals_minimal   = System.IO.Path.Combine(".", "external", "binaries-minimal");

Dictionary<string, string> files_external = new Dictionary<string, string>();

Task ("externals-download-holisticware")
    .Does 
    (
        () =>
        {
            string fd = null;
            string fo = null;
            
            fd = System.IO.Path.Combine(folder_externals_extensive, "*.zip");
            //DeleteFiles(fd);
            fd = System.IO.Path.Combine(folder_externals_extensive, "*.tar.*");
            //DeleteFiles(fd);
            
            foreach(KeyValuePair<string, string> kvp in files_external)
            {
                fo = kvp.Value;
                fd = System.IO.Path.Combine(folder_externals_extensive, kvp.Key);
                Information ("    fo     = " + fo);
                Information ("        fd = " + fd);
                
                if (fo.EndsWith(".git"))
                {
                    Information ("        fo = cloning" );
                    StartProcess 
                        (
                            "git",
                                " clone --recursive " 
                                + kvp.Value + 
                                " "
                                + fd
                                );
                }
                else
                {
                    if (!FileExists(fd))
                    {
                        DownloadFile (kvp.Value, fd);
                    }
                }
            }
        }
    );
    
Task ("externals-unzip-holisticware")
    .IsDependentOn("externals-download-holisticware")
    .Does 
    (
        () =>
        {
        }
    );


//###################################################################################
// Library setup
List<string>            library_projectnames = new List<string>();
List<string> 			library_project_outputfiles = new List<string>();
List<OutputFileCopy> 	library_output_files_to_copy = new List<OutputFileCopy>();
